namespace VanillaArtStore.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddProductFormModel
    {
        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Description { get; init; }

        public int InStockQuantity { get; init; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}
