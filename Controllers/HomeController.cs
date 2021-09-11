namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;
    using VanillaArtStore.Data;
    using VanillaArtStore.Models;
    using VanillaArtStore.Models.Products;

    public class HomeController : Controller
    {
        private readonly VanillaArtDbContext data;

        public HomeController(VanillaArtDbContext data)
            => this.data = data;

        public IActionResult Index()
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
                    Description = p.Description,
                    Category = p.Category.Name
                })
                .Take(8)
                .ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
