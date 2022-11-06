using VanillaArtStore.Services.Messages.Models;

namespace VanillaArtStore.Services.Messages
{

    public interface IMessageService
    {
        bool SendMessage(
            string userName,
            string userEmail,
            string subject, 
            string message);
    }
}
