using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JarvisOmnicrypt.Models;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace JarvisOmnicrypt.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;
        private readonly ILogger<HomeController> _logger;

        public static int CodePage { get; set; } = 1251;
        public static string Key { get; set; } = "";
        public const string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public HomeController(ILogger<HomeController> logger, ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Models.File file = new Models.File { Name = uploadedFile.FileName, Path = path };
                try
                {
                    file.Value = System.IO.File.ReadAllText(_appEnvironment.WebRootPath + path, Encoding.GetEncoding(CodePage));
                    FileViewModel.Text = file.Value;
                }
                catch (Exception)
                {
                }
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public void ChangeCodePage()
        {
            CodePage = (CodePage == 1251) ? 65001 : 1251;
        }

        public IActionResult Decrypt()
        {
            int keyLenght = Key.Length;
            int keyPosition = 0;
            int textLenght = FileViewModel.Text.Length;
            char[] text = FileViewModel.Text.ToCharArray();
            if (keyLenght > 0)
            {
                for (int i = 0; i < textLenght; i++)
                {
                    if (russianAlphabet.Contains(text[i]))
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}
