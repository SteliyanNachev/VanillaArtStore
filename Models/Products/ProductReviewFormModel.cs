namespace VanillaArtStore.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using static Data.DataConstants;

    public class ProductReviewFormModel
    {

        [Required]
        [Range(ReviewRatingMin,ReviewRatingMxn)]
        public int Rating { get; set; }
        
        [Required]
        [StringLength(ReviewCommentMaxLenght,
            MinimumLength = ReviewCommentMinLenght,
            ErrorMessage = "Review should be between {2} and {1} characters long !")]
        public string Comment { get; set; }

    }
}
