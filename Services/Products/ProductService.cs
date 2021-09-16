using System.Linq;
using VanillaArtStore.Data;
using VanillaArtStore.Models.Products;

namespace VanillaArtStore.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly VanillaArtDbContext data;

        public ProductService(VanillaArtDbContext data) => this.data = data;
        public AllProductsQueryServiceModel All(
            string category,
            string searchTerm,
            ProductSorting sorting,
            int currentPage
            )
        {
           

            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower())
                    || p.Description.ToLower().Contains(searchTerm.ToLower())
                    || p.Category.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.DateCreated => productsQuery.OrderByDescending(p => p.Id),
                ProductSorting.PriceInc => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.PriceDec => productsQuery.OrderBy(p => p.Price),
                _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * AllProductsQueryServiceModel.ProductsPerPage)
                .Take(AllProductsQueryServiceModel.ProductsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    InStockQuantity = p.InStockQuantity,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                })
                .ToList();

            var categories = this.data
                .Categories
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return new AllProductsQueryServiceModel
            {
                Products = products,
                Categories = categories,
                TotalProducts = totalProducts
            };
        }
    }
}
