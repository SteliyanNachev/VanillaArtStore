namespace VanillaArtStore.Services.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data.Models;

    public interface IShoppingCartService
    {
        public void AddToCart(Product product, int quantity);

        public int RemoveFromCart(Product product);

        public List<ShoppingCartItem> GetShoppingCartItems();

        public void ClearCart();

        public decimal GetShoppingCartTotal();
    }
}
