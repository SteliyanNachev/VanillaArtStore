using System.Collections.Generic;
using System.Linq;
using VanillaArtStore.Data;
using VanillaArtStore.Data.Models;
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
                    Description = p.Description,
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

        public bool CategoryExists(int categoryId)
            => this.data.Categories.Any(c => c.Id == categoryId);

        public int Create(string name, decimal price, string description, string imageUrl, int inStockQuantity, int categoryId)
        {
            var productData = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                InStockQuantity = inStockQuantity,
                CategoryId = categoryId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public ProductServiceModel Details(int id)
            => this.data
                .Products
                .Where(p => p.Id == id)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    InStockQuantity = p.InStockQuantity,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                })
                .FirstOrDefault();

        public bool Edit(int id, string name, decimal price, string description, string imageUrl, int inStockQuantity, int categoryId)
        {
            var productData = this.data.Products.Find(id);

            //check if car can be edited by user in future if there is more than one admin.

            if (productData == null)
            {
                return false;
            }

            productData.Name = name;
            productData.Price = price;
            productData.Description = description;
            productData.ImageUrl = imageUrl;
            productData.InStockQuantity = inStockQuantity;
            productData.CategoryId = categoryId;
            
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ProductCategoryServiceModel> GetAllProductCategories()
             => this.data
                .Categories
                .Select(p => new ProductCategoryServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        
    }
}
