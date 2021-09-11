namespace VanillaArtStore.Models.Products
{
    public class ProductListingViewModel
    {
        public int Id { get; init; } 
        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Description { get; init; }

        public int InStockQuantity { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }
    }
}
