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
        private ApplicationContext _applicationContext;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, ApplicationContext applicationContext)
        {
            _logger = logger;
            _mapper = mapper;
            _applicationContext = applicationContext;
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
                string path = "wwwroot/Files/" + file[0] + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + "." + file[1];
                string xslsPath = path.Replace($".{file[1]}", ".xlsx");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var uploaded = new UploadedFile() { Filename = path };
                _applicationContext.UploadedFiles.Add(uploaded);
                ICollection<string> strings = _fileService.GetData(path, uploadedFile.ContentType);
                text = _parser.ParseText(strings);
                IEnumerable<ConcordanceItem> x = _textService.Concordance(text);
                var morph = _textService.ConcordanceMorphy(x);
                var items = _mapper.Map<IEnumerable<ConcordanceItemsDTO>, IEnumerable<ConcordanceItemViewModel>>(morph);
                _fileService.WriteData(morph, xslsPath);
                var toDownload = new FileToDownload() { Filename = xslsPath };
                _applicationContext.FileToDownloads.Add(toDownload);
                _applicationContext.FileLinks.Add(new FileLinks() { UploadedFile = uploaded, FileToDownload = toDownload });
                await _applicationContext.SaveChangesAsync();
                var fileViewModel = new FileViewModel() { FileInfo = xslsPath, Items = items };
                return View("List", fileViewModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Download(string filename)
        {
            filename = filename.Replace("wwwroot", "~");
            return File(filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "answer.xlsx");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
