namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using VanillaArtStore.Models.Products;
    using VanillaArtStore.Services.Products;

    public class ProductsController : Controller
    {
        private readonly IProductService products;

        public ProductsController(IProductService products)
        {
            this.products = products;
        }

        public IActionResult All([FromQuery] AllProductsQueryModel query)
         {
            var queryResult = this.products.All(query.Category, query.SearchTerm, query.Sorting, query.CurrentPage);

            query.Products = queryResult.Products.Select(p => new ProductListingViewModel 
            {
                Id = p.Id,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Category = p.Category,
                Name = p.Name,
                InStockQuantity = p.InStockQuantity
            });

            query.TotalProducts = queryResult.TotalProducts;

            query.Categories = queryResult.Categories;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var product = this.products.Details(id);

            return View(new ProductListingViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                InStockQuantity = product.InStockQuantity,
                Category = product.Category
            });
        }

        [Authorize]
        public IActionResult Add()
            => View(new ProductFormModel
            {
                Categories = this.products.GetAllProductCategories()
            });

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            //Image should be saved explained at -32min at lecture working with data.

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.GetAllProductCategories();

                return View(product);
            }

            this.products.Create(
                product.Name,
                product.Price,
                product.Description,
                product.ImageUrl,
                product.InStockQuantity,
                product.CategoryId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var product = this.products.Details(id);

            return View(new ProductFormModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                InStockQuantity = product.InStockQuantity,
                CategoryId = product.CategoryId,
                Categories = this.products.GetAllProductCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.GetAllProductCategories();

                return View(product);
            }

            var productEdited = this.products.Edit(
                id,
                product.Name,
                product.Price,
                product.Description,
                product.ImageUrl,
                product.InStockQuantity,
                product.CategoryId);

            if (productEdited)
            {
                return RedirectToAction(nameof(All));
            }

            return BadRequest();
        }
    }
}
