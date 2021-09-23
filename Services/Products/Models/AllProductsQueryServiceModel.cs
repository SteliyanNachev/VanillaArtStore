namespace VanillaArtStore.Services.Products.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Models.Products;

    public class AllProductsQueryServiceModel
    {
        public const int ProductsPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public int TotalProducts { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public string SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
