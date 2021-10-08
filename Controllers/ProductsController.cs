namespace VanillaArtStore.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Collections.Generic;
    using System.Linq;
    using VanillaArtStore.Models.Products;
    using VanillaArtStore.Services.Products;
    using VanillaArtStore.Services.Products.Models;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly IMapper mapper;

        public ProductsController(IProductService products, IMapper mapper)
        {
            this.products = products;
            this.mapper = mapper;
        }

        [Authorize(Roles = WebConstants.AdministratorRoleName)]
        public IActionResult All([FromQuery] AllProductsQueryModel query)
         {
            var queryResult = this.products.All(query.Category, query.SearchTerm, query.Sorting, query.CurrentPage);

            query.Products = queryResult.Products.Select(p => new ProductListingViewModel 
            {
                Id = p.Id,
                Price = p.Price,
                Description = p.Description,
                Images = p.Images,
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

            var productsFromCategory = this.products.GetAllProductFromSAmeCategory(product.CategoryId);

            MultipleClassForDetailsAndReview multiModel = new();

            var products = new ProductListingViewModel

            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Images = product.Images,
                InStockQuantity = product.InStockQuantity,
                Category = product.Category,
                ProductsFromCategory = productsFromCategory,
                Reviews = product.Reviews
            };

            var reviews = new ProductReviewFormModel { };

            multiModel.Products = products;
            multiModel.Review = reviews;

            return View(multiModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(MultipleClassForDetailsAndReview multiView)
        {
            var review = multiView.Review;

            return RedirectToAction(nameof(Details));
        }

        [Authorize(Roles = WebConstants.AdministratorRoleName)]
        public IActionResult Add()
            => View(new ProductFormModel
            {
                Categories = this.products.GetAllProductCategories()
            });

        [HttpPost]
        [Authorize(Roles = WebConstants.AdministratorRoleName)]
        public IActionResult Add(ProductFormModel product)
        {
            //Image should be saved explained at -32min at lecture working with data.

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (product.Images == null || product.Images.Count < 3)
            {
                this.ModelState.AddModelError(nameof(product.Images), "Images should be mode than 3");
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
                product.Images,
                product.InStockQuantity,
                product.CategoryId);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = WebConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var product = this.products.Details(id);

            var productForm = new ProductFormModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                InStockQuantity = product.InStockQuantity,
                CategoryId = product.CategoryId,
                Images = product.Images
                .Select(i => new Models.Images.ImageInputModel
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl
                })
                .ToList()
            };

            productForm.Categories = this.products.GetAllProductCategories();

            return View(productForm);
        }

        [Authorize(Roles = WebConstants.AdministratorRoleName)]
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
              product.Images,
              product.InStockQuantity,
              product.CategoryId);

           if (productEdited)
           {
               return RedirectToAction(nameof(All));
           }

            return BadRequest();
        }

        [Authorize(Roles = WebConstants.AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var deleted = this.products.Delete(id);
            if (!deleted)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
