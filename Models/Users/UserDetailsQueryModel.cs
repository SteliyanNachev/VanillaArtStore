namespace VanillaArtStore.Models.Users
{
    using System.Collections.Generic;
    using VanillaArtStore.Data.Models;

    public class UserDetailsQueryModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public bool HasDiscount { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
