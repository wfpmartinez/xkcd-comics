using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using xkcd.Services;
using xkcd.Models;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Index()
        {
            var model = await _comicService.GetComic(null);
            model.MaxNumber = model.Number;
            HttpContext.Session.SetString("maxNumber", model.Number.ToString());
            return View(model);
        }

        [Route("comic/{comicId?}")]
        public async Task<IActionResult> Index(int? comicId, bool previous = true)
        {
            var model = await _comicService.GetComic(comicId);

            if (model == null)
            {
                model = await _comicService
                    .GetComic(_comicService
                        .GetNextComicNumber((int)comicId, previous)
                    );
            }
            model.MaxNumber = Convert.ToInt16(HttpContext.Session.GetString("maxNumber"));
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
