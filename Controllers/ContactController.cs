namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VanillaArtStore.Models.Messages;

    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(SendMessageFormModel model)
        {



            return RedirectToAction("Index");
        }
    }
}
