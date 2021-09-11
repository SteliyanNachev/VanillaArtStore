namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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

        public IActionResult All()
        {
            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
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

            return View(products);
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
