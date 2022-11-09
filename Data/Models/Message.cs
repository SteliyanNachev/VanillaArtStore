using static VanillaArtStore.Data.DataConstants;

namespace VanillaArtStore.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
