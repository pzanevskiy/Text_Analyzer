using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Text_Analyzer.Models;
using AutoMapper;
using Text_Analyzer.Utility.Service.Interfaces;
using Text_Analyzer.Utility.Service;
using Text_Analyzer.Utility.Models.Interfaces;
using Text_Analyzer.Utility.Models;
using Text_Analyzer.Utility.DataTransferObject;

namespace Text_Analyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParser _parser = new Parser();
        private readonly IFileService _fileService = new FileService();
        private readonly ITextService _textService = new TextService();
        private readonly IMapper _mapper;
        private ApplicationContext _app;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, ApplicationContext applicationContext)
        {
            _logger = logger;
            _mapper = mapper;
            _app = applicationContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile uploadedFile)
        {
            IText text;
            string[] file = uploadedFile.FileName.Split('.');
            if (uploadedFile != null)
            {
                string path = "wwwroot/Files/" + file[0] + DateTime.Now.ToString("yyyyMMdd_HHmmssff") + "." + file[1];
                string xslsPath = path.Replace($".{file[1]}", ".xlsx");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var uploaded = new UploadedFile() { Filename = path.Replace("wwwroot/Files/",""), Extension = uploadedFile.ContentType };
                _app.UploadedFiles.Add(uploaded);
                ICollection<string> strings = _fileService.GetData(path, uploadedFile.ContentType);
                text = _parser.ParseText(strings);
                IEnumerable<ConcordanceItem> x = _textService.Concordance(text);
                var morph = _textService.ConcordanceMorphy(x);
                var items = _mapper.Map<IEnumerable<ConcordanceItemsDTO>, IEnumerable<ConcordanceItemViewModel>>(morph);
                _fileService.WriteData(morph, xslsPath);
                var toDownload = new FileToDownload() { Filename = xslsPath.Replace("wwwroot/Files/", "") };
                _app.FileToDownloads.Add(toDownload);
                _app.FileLinks.Add(new FileLinks() { UploadedFile = uploaded, FileToDownload = toDownload });
                await _app.SaveChangesAsync();
                var fileViewModel = new FileViewModel() { FileInfo = xslsPath, Items = items };
                return View("List", fileViewModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Download(string filename)
        {
            return File("~/Files/" + filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"answer_{filename.Replace("~/Files/", "")}.xlsx");
        }

        public IActionResult DownloadSource(string filename, string extension)
        {
            return File("~/Files/" + filename, extension, filename.Replace("~/Files/", ""));
        }

        public IActionResult History()
        {
            var history = _app.FileLinks.ToList()
                .Select(x => new HistoryItemViewModel()
                {
                    Uploaded = _app.UploadedFiles.Where(upl => upl.Id == x.UploadedFileId).FirstOrDefault().Filename,
                    UploadedExt = _app.UploadedFiles.Where(upl => upl.Id == x.UploadedFileId).FirstOrDefault().Extension,
                    Downloaded = _app.FileToDownloads.Where(upl => upl.Id == x.FileToDownloadId).FirstOrDefault().Filename
                });
            return View(history);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
