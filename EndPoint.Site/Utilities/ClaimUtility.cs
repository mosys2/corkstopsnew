using Microsoft.AspNetCore.Identity;
using Store.Domain.Entities.Users;
using System.Security.Claims;

namespace EndPoint.Site.Utilities
{

    public static class ClaimUtility
    {
        public static string? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userId;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
