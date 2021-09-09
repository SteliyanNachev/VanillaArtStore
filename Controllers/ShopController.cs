using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VanillaArtStore.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.LayoutSecond = true;
            return View();
        }
    }
}
