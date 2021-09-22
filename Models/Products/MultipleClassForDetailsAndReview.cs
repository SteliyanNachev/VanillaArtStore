namespace VanillaArtStore.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MultipleClassForDetailsAndReview
    {
        public ProductListingViewModel Products { get; set; }

        public ProductReviewFormModel Review { get; set; }
    }
}
