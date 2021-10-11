using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VanillaArtStore.Data.Models;

using static VanillaArtStore.Infrastructure.AdressExtentions;

namespace VanillaArtStore.Models.Users
{
    public class UserDetailsFormModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public string Adress { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<string> Countries { get; set; } = GetCountryList();

    }
}
