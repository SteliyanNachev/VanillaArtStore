using System.ComponentModel.DataAnnotations;
using static VanillaArtStore.Data.DataConstants;

namespace VanillaArtStore.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MessageUsernameMaxLenght,
            MinimumLength = MessageUsernameMinLenght,
            ErrorMessage = "Name should be between {2} and {1} characters long !")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [MaxLength(MessageSubjectMaxLenght)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(MessageMaxLenght)]
        public string MessageText { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
