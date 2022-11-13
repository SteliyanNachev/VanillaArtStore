namespace VanillaArtStore.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using VanillaArtStore.Models.Messages;
    using VanillaArtStore.Services.Messages;

    public class ContactController : Controller
    {
        private readonly IMessageService messages;
        private readonly IMapper mapper;

        public ContactController(IMessageService messages, IMapper mapper)
        {
            this.messages = messages;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SendMessageFormModel model)
        {
            var sendMessageResult = this.messages.SendMessage(model.UserName,model.UserEmail,model.Subject,model.Message);

            return RedirectToAction("Index");
        }
    }
}
