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
using TextParser.Models.Interfaces;
using TextParser.Service;
using TextParser.Service.Interfaces;
using TextParser.Models;
using System.Text;
using Task2.Models;
using AutoMapper;

namespace Text_Analyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IParser parser = new Parser();
        IFileService fileService = new FileService();
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile uploadedFile)
        {
            IText text = new Text();
            if (uploadedFile != null)
            {
                // путь к папке Files
                var result = new StringBuilder();
                using (var reader = new StreamReader(uploadedFile.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(await reader.ReadLineAsync());
                }
                string path = "wwwroot/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                switch (uploadedFile.ContentType)
                {
                    case "application/pdf":
                        {
                            ICollection<string> s = fileService.GetReaderPDF(path);
                            text = parser.ParseText(s);
                            var x = text.Sentences.SelectMany(sentence => sentence.Words)
                                .GroupBy(word => word.ToString().ToLower())
                                .Select(item => new ConcordanceItem { Word = new Word(item.Key), Count = item.ToList().Count })
                                .Where(item => item.Word.Count > 0)
                                .OrderBy(x => x.Word.FirstChar);
                            //.GroupBy(item => item.Word.FirstChar);
                            var items = _mapper.Map<IEnumerable<ConcordanceItem>, IEnumerable<ConcordanceItemViewModel>>(x);
                            return View("List", items);
                            break;
                        }
                    case "text/plain":
                        {                           
                            ICollection<string> s = fileService.GetTXT(path);
                            text = parser.ParseText(s);
                            var x = text.Sentences.SelectMany(sentence => sentence.Words)
                               .GroupBy(word => word.ToString().ToLower())
                               .Select(item => new ConcordanceItem { Word = new Word(item.Key), Count = item.ToList().Count })
                               .Where(item => item.Word.Count > 0)
                               .OrderBy(x => x.Word.FirstChar);
                            break;
                        }
                    case "application/msword":
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                        {
                            ICollection<string> s = fileService.GetReader(path);
                            text = parser.ParseText(s);
                            var x = text.Sentences.SelectMany(sentence => sentence.Words)
                                .GroupBy(word => word.ToString().ToLower())
                                .Select(item => new ConcordanceItem { Word = new Word(item.Key), Count = item.ToList().Count })
                                .Where(item => item.Word.Count > 0)
                                .OrderBy(x => x.Word.FirstChar);
                            break;
                        }
                    default: { break; }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
