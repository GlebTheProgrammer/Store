using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Yandex.Kassa.Areas.YandexKassa.Controllers
{
    [Area("YandexKassa")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // /YandexKassa/Home/Callabck
        public IActionResult Callback()
        {
            return View();
        }
    }
}
