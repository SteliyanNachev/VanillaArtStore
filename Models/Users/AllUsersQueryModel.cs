namespace VanillaArtStore.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AllUsersQueryModel
    {
        public List<UserDetailsQueryModel> Users { get; set; }
    }
}
