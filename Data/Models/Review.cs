namespace VanillaArtStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Review
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(ReviewCommentMaxLenght)]
        public string Comment { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
