namespace VanillaArtStore.Models.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;


    public class SendMessageFormModel
    {
        [Required]
        [StringLength(MessageUsernameMaxLenght,
            MinimumLength = MessageUsernameMinLenght,
            ErrorMessage = "Name should be between {2} and {1} characters long !")]
        public string UserName { get; init; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; init; }

        [MaxLength(MessageSubjectMaxLenght)]
        public string Subject { get; init; }

        [Required]
        [MaxLength(MessageMaxLenght)]
        public string Message { get; init; }

    }
}
