namespace VanillaArtStore.Services.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Models.Images;
    using VanillaArtStore.Models.Products;
    using VanillaArtStore.Services.Products.Models;

    public interface IProductService
    {
        AllProductsQueryServiceModel All(
            string category,
            string searchTerm,
            ProductSorting sorting,
            int currentPage);

        ProductServiceModel Details(int id);

        int Create(
            string name,
            decimal price,
            string description,
            ICollection<ImageInputModel> images,
            int inStockQuantity,
            int categoryId);

        bool Edit(
           int id,
           string name,
           decimal price,
           string description,
           ICollection<ImageInputModel> images,
           int inStockQuantity,
           int categoryId);

        bool Delete(int id);

        IEnumerable<ProductCategoryServiceModel> GetAllProductCategories();
        IEnumerable<ProductServiceModel> GetAllProductFromSAmeCategory(int categoryId);

        IEnumerable<Review> GetAllProductReviews(int productId);

        bool CategoryExists(int categoryId);

        IEnumerable<Image> GetImages(int productId);

        IEnumerable<ProductServiceModel> GetLatestProducts();

        IQueryable<ProductServiceModel> GetAllProducts();
    }
}
