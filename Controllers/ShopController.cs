namespace VanillaArtStore.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using VanillaArtStore.Models.Products;
    using VanillaArtStore.Services.Products;

    public class ShopController : Controller
    {
        private readonly IProductService products;
        private readonly IMapper mapper;

        public ShopController(IProductService products, IMapper mapper)
        {
            this.products = products;
            this.mapper = mapper;
        }

        public IActionResult Index([FromQuery] AllProductsQueryModel query)
        {
            ViewBag.LayoutSecond = true;

            var queryResult = this.products.All(query.Category, query.SearchTerm, query.Sorting, query.CurrentPage);

            query.Products = queryResult.Products.Select(p => new ProductListingViewModel
            {
                Id = p.Id,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
                Name = p.Name,
                Images = p.Images,
                InStockQuantity = p.InStockQuantity
            });

            query.TotalProducts = queryResult.TotalProducts;

            query.Categories = queryResult.Categories;

            return View(query);
        }

        public IActionResult All()
        {
            ViewBag.LayoutSecond = true;

            var products = this.products
                .GetAllProducts()
                .ProjectTo<ProductListingViewModel>(this.mapper.ConfigurationProvider)
                .Take(8)
                .ToList();

            return View(products);
        }
    }
}
