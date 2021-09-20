namespace VanillaArtStore.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VanillaArtStore.Services.Products;
    using static Data.DataConstants;
    public class ProductFormModel
    {
        [Required]
        [StringLength(ProductNameMaxLenght,
            MinimumLength = ProductNameMinLenght,
            ErrorMessage ="Name should be between {2} and {1} characters long !")]
        [MinLength(ProductNameMinLenght)]
        public string Name { get; init; }

        [Required]
        [Range(ProductPriceMinQuantity,ProductPriceMaxQuantity)]
        public decimal Price { get; init; }

        [Required]
        [MaxLength(ProductDescriptionMaxLenght)]
        [MinLength(ProductDescriptionMinLenght)]
        public string Description { get; init; }

        [Required]
        [Range(ProductInSctockMinQuantity,ProductInSctockMaxQuantity)]
        public int InStockQuantity { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; }
    }
}
