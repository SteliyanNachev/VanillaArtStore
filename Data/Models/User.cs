namespace VanillaArtStore.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.User;
    public class User : IdentityUser
    {
        [MaxLength(UserNameMaxLenght)]
        public string FirstName { get; set; }

        [MaxLength(UserNameMaxLenght)]
        public string LastName { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public  IEnumerable<Review> Reviews { get; set; }

        public bool HasDiscount { get; set; } = false;

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
