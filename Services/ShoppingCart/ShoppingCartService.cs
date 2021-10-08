namespace VanillaArtStore.Services.ShoppingCart
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Services.ShoppingCart.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly VanillaArtDbContext data;

        public ShoppingCartService(VanillaArtDbContext data)
        {
            this.data = data;
        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<VanillaArtDbContext>();
            string cartId = session.GetString("CartId")?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartService(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product, int quantity)
        {
            var shoppingCartItem =
                this.data.ShoppingCartItems.SingleOrDefault(
                    s => s.ProductId == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Quantity = quantity
                };

                this.data.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }

            this.data.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
                this.data.ShoppingCartItems.SingleOrDefault(
                    s => s.ProductId == product.Id && s.ShoppingCartId == ShoppingCartId);

            var localQuantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    localQuantity = shoppingCartItem.Quantity;
                }
                else
                {
                    this.data.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            this.data.SaveChanges();

            return localQuantity;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? 
                (ShoppingCartItems =
                this.data.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = this.data
                .ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId);

            this.data.ShoppingCartItems.RemoveRange(cartItems);

            this.data.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = this.data
                .ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Quantity)
                .Sum();

            return total;
        }
    }
}
