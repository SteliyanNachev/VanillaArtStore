namespace VanillaArtStore.Models.Products
{
    using System.Collections.Generic;
    public class AllProductsQueryModel
    {
        public IEnumerable<string> Category { get; init; }

        public IEnumerable<string> SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<ProductListingViewModel> Products { get; init; }
    }
}
