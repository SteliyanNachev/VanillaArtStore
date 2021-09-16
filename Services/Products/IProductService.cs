namespace VanillaArtStore.Services.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Models.Products;

    public interface IProductService
    {
        AllProductsQueryServiceModel All(
            string category,
            string searchTerm,
            ProductSorting sorting,
            int currentPage);


    }
}
