namespace VanillaArtStore.Models.Products
{
    using System.Collections.Generic;
    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public int TotalProducts { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public string SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
