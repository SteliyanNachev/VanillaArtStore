namespace VanillaArtStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data;
    using VanillaArtStore.Models.ShoppingCart;
    using VanillaArtStore.Services.Products;
    using VanillaArtStore.Services.ShoppingCart;

    public class ShoppingCartController : Controller
    {
        private readonly IProductService products;
        private readonly ShoppingCartService shoppingCart;
        private readonly VanillaArtDbContext data;

        public ShoppingCartController(IProductService products, ShoppingCartService shoppingCart, VanillaArtDbContext data )
        {
            this.products = products;
            this.shoppingCart = shoppingCart;
            this.data = data;
        }

        public IActionResult Index()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }

        public IActionResult AddToCart([FromQuery]int id, [FromQuery]int quantity)
        {
            var selectedProduct = this.data.Products.FirstOrDefault(p => p.Id == id);
            if (selectedProduct != null)
            {
                shoppingCart.AddToCart(selectedProduct, quantity);
            }

            return Redirect("/Shop");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var selectedProduct = this.data.Products.FirstOrDefault(p => p.Id == id);
            if (selectedProduct != null)
            {
                shoppingCart.RemoveFromCart(selectedProduct);
            }

            return Redirect("/Home");
        }
    }
}
