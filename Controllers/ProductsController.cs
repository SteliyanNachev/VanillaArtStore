namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using VanillaArtStore.Data;
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

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            return View();
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
