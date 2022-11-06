namespace VanillaArtStore.Services.Messages
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data;
    using VanillaArtStore.Services.Messages.Models;
    using static VanillaArtStore.Data.DataConstants;

    public class MessageService : IMessageService
    {
        private readonly VanillaArtDbContext data;
        private readonly UserManager<User> user;
        private readonly IMapper mapper;

        public MessageService(VanillaArtDbContext data, UserManager<User> user, IMapper mapper)
        {
            this.data = data;
            this.user = user;
            this.mapper = mapper;
        }

        bool IMessageService.SendMessage(string userName, string userEmail, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
