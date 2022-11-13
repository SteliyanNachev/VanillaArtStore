namespace VanillaArtStore.Services.Messages
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Services.Messages.Models;
    using static VanillaArtStore.Data.DataConstants;

    public class MessageService : IMessageService
    {
        private readonly VanillaArtDbContext data;
        private readonly UserManager<Data.Models.User> user;
        private readonly IMapper mapper;

        public MessageService(VanillaArtDbContext data, UserManager<Data.Models.User> user, IMapper mapper)
        {
            this.data = data;
            this.user = user;
            this.mapper = mapper;
        }

        bool IMessageService.SendMessage(string userName, string userEmail, string subject, string message)
        {
            var existingUser = this.user.FindByEmailAsync(userEmail);

            if (existingUser.Result == null)
            {
                var messageData = new Message
                {
                    UserName = userName,
                    UserEmail = userEmail,
                    Subject = subject,
                    MessageText = message
                };

                this.data.Messages.Add(messageData);
                this.data.SaveChanges();

                return true;
            }
            else
            {
                var messageData = new Message
                {
                    UserName = userName,
                    UserEmail = userEmail,
                    Subject = subject,
                    MessageText = message,
                    UserId = existingUser.Result.Id
                };

                this.data.Messages.Add(messageData);
                this.data.SaveChanges();

                return true;
            }


            return false;
        }
    }
}
