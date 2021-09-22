namespace VanillaArtStore.Models.Products
{
    using System.Collections.Generic;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Services.Products;

    public class ProductListingViewModel
    {
        public int Id { get; init; } 
        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Description { get; init; }

        public int InStockQuantity { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }

        public IEnumerable<ProductServiceModel> ProductsFromCategory { get; init; }

        public IEnumerable<Review> Reviews { get; init; }
    }
}
