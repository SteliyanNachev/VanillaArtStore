namespace VanillaArtStore.Infrastructure
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtentions
    {
        public static string GetId(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier).Value;

    }
}
