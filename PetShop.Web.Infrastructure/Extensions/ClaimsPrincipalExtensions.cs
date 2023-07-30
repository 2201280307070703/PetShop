namespace PetShop.Web.Infrastructure.Extensions
{
    using System.Security.Claims;
    using static PetShop.Common.GeneralApplicationConstants;
    public static  class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsUserAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
