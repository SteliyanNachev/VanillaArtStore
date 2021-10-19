namespace VanillaArtStore.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;
    using VanillaArtStore.Models;
    using VanillaArtStore.Models.Products;
    using VanillaArtStore.Services.Products;

    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductService products;

        public HomeController(IMapper mapper, IProductService products)
        {
            this.mapper = mapper;
            this.products = products;
        }

        public IActionResult Index()
        {
            var products = this.products.GetLatestProducts()
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    InStockQuantity = p.InStockQuantity,
                    Category = p.Category,
                    Images = p.Images,
                    Reviews = p.Reviews
                })
                .ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
