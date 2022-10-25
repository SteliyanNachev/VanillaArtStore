namespace VanillaArtStore.Services.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Models.Messages;
    using VanillaArtStore.Services.Messages.Models;

    interface IMessageService
    {
        MessageServiceModel SendMessage(
            int id,
            string userName,
            string userEmail,
            string subject, 
            string message);
    }
}
