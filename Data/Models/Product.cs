namespace VanillaArtStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ProductNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ProductInSctockMaxQuantity)]
        public int InStockQuantity { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
