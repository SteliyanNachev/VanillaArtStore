namespace VanillaArtStore.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;
    using VanillaArtStore.Data;
    using VanillaArtStore.Models;
    using VanillaArtStore.Models.Products;

    public class HomeController : Controller
    {
        private readonly VanillaArtDbContext data;
        private readonly IMapper mapper;

        public HomeController(VanillaArtDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductListingViewModel>(this.mapper.ConfigurationProvider)
                .Take(8)
                .ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
