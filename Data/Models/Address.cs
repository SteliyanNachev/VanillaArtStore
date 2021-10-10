namespace VanillaArtStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    using static DataConstants.Address;
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressLineMaxLenght)]
        public string AddressLine { get; set; }

        [Required]
        [MaxLength(TownMaxLenght)]
        public string Town { get; set; }

        [Required]
        [MaxLength(ZipCodeMaxLenght)]
        public int ZipCode { get; set; }
        
        [Required]
        public string Country { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }

    }
}
