namespace VanillaArtStore.Models.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Services.ShoppingCart;

    public class ShoppingCartViewModel
    {
        public ShoppingCartService ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
