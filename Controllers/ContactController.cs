namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
