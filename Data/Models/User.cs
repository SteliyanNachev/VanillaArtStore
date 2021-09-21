namespace VanillaArtStore.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.User;
    public class User : IdentityUser
    {
        [MaxLength(UserNameMaxLenght)]
        public string FirstName { get; set; }

        [MaxLength(UserNameMaxLenght)]
        public string LastName { get; set; }

        [MaxLength(UserAdressMaxLenght)]
        public string Adress { get; set; }
    }
}
