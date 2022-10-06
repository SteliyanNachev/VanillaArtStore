namespace VanillaArtStore.Models.Users
{
    using VanillaArtStore.Data.Models;

    public class UserDetailsQueryModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
