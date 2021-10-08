namespace VanillaArtStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
