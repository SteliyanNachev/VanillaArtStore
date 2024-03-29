﻿namespace VanillaArtStore.Services.Products.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VanillaArtStore.Data.Models;

    public class ProductServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Description { get; init; }

        public int InStockQuantity { get; init; }

        public int CategoryId { get; init; }

        public string Category { get; init; }

        public IEnumerable<Review> Reviews { get; init; }

        public IEnumerable<Image> Images { get; init; }



    }
}
