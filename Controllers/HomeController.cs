using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using xkcd.Services;
using xkcd.Models;

namespace xkcd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComicService _comicService;

        public HomeController(ComicService comicService)
        {
            _comicService = comicService;
        }

        [Route("/")]
        [Route("comic/{comicId?}")]
        public async Task<IActionResult> Index(int? comicId)
        {
            var model = await _comicService.GetComic(comicId);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
