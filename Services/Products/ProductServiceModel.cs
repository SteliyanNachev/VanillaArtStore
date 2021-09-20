﻿namespace VanillaArtStore.Services.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Description { get; init; }

        public int InStockQuantity { get; init; }

        public string ImageUrl { get; init; }

        public int CategoryId { get; init; }

        public string Category { get; init; }
    }
}
