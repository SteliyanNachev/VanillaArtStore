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

        ProductServiceModel Details(int id);

        IEnumerable<ProductCategoryServiceModel> GetAllProductCategories();

        bool CategoryExists(int categoryId);

        int Create(
            string name,
            decimal price,
            string description,
            string imageUrl,
            int inStockQuantity,
            int categoryId);
        
        bool Edit(
            int id,
            string name,
            decimal price,
            string description,
            string imageUrl,
            int inStockQuantity,
            int categoryId);
    }
}
