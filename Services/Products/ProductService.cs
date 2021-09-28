using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using VanillaArtStore.Data;
using VanillaArtStore.Data.Models;
using VanillaArtStore.Models.Products;
using VanillaArtStore.Services.Products.Models;

namespace VanillaArtStore.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly VanillaArtDbContext data;
        private readonly UserManager<User> user;
        private readonly IMapper mapper;

        public ProductService(VanillaArtDbContext data,UserManager<User> user, IMapper mapper)
        {
            this.data = data;
            this.user = user;
            this.mapper = mapper;
        }

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
                    Images = p.Images,
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
                InStockQuantity = inStockQuantity,
                CategoryId = categoryId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public ProductServiceModel Details(int id)
        {
            var reviews = this.GetAllProductReviews(id);

            return this.data
                           .Products
                           .Where(p => p.Id == id)
                           .ProjectTo<ProductServiceModel>(this.mapper.ConfigurationProvider)
                           .FirstOrDefault();
        }

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

        public IEnumerable<ProductServiceModel> GetAllProductFromSAmeCategory(int categoryId)
        {
            var productsFromCategory = this.data
                .Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    InStockQuantity = p.InStockQuantity,
                    Category = p.Category.Name,
                    Images = p.Images
                })
                .ToList();

            return productsFromCategory;
        }

        public IEnumerable<Review> GetAllProductReviews(int productId)
        {
            var productReviews = this.data
                .Reviews
                .Where(r => r.ProductId == productId)
                .Select(r => new Review
                {
                    Id = r.Id,
                    Date = r.Date,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    ProductId = r.ProductId,
                    UserId = r.UserId,
                    User = r.User
                })
                .ToList();

            return productReviews;
        }

        public  IEnumerable<Image> GetImages(int productId)
        {
            var productImages = this.data
                .Images
                .Where(i => i.ProductId == productId)
                .Select(i => new Image
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    ProductId = i.ProductId,
                    Product = i.Product
                })
                .ToList();

            return productImages;
        }
    }
}
