namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VanillaArtStore.Data;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Models.Products;

    public class ProductsController : Controller
    {
        private readonly VanillaArtDbContext data;

        public ProductsController(VanillaArtDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddProductFormModel
        {
            Categories = this.GetProductCategories()
        });

        public IActionResult All([FromQuery] AllProductsQueryModel query)
         {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(query.SearchTerm.ToLower())
                    || p.Description.ToLower().Contains(query.SearchTerm.ToLower())
                    || p.Category.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productsQuery = query.Sorting switch
            {
                ProductSorting.DateCreated => productsQuery.OrderByDescending(p => p.Id),
                ProductSorting.PriceInc => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.PriceDec => productsQuery.OrderBy(p => p.Price),
                _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((query.CurrentPage - 1) * AllProductsQueryModel.ProductsPerPage)
                .Take(AllProductsQueryModel.ProductsPerPage)
                .Select(p => new ProductListingViewModel
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

            query.Categories = categories;
            query.Products = products;
            query.TotalProducts = totalProducts;

            return View(query);
        }

        [HttpPost]
        public IActionResult Add(AddProductFormModel product, IEnumerable<IFormFile> images)
        {
            if (images.Any(i=>i.Length > 4 * 1024 * 1024))
            {
                this.ModelState.AddModelError("Images","Images should be maximum 4Mb.");
            }

            //Image should be saved explained at -32min at lecture working with data.

            if (!this.data.Categories.Any(c=>c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            }

            var productData = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                InStockQuantity = product.InStockQuantity,
                CategoryId = product.CategoryId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.data
                .Categories
                .Select(p => new ProductCategoryViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
    }
}
