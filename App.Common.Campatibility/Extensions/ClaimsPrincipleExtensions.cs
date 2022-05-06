using System.Security.Claims;

namespace App.Common.Campatibility.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static int? GetUserIdIfExisting(this ClaimsPrincipal user)
        {
            var claimValue = user.FindFirst(ClaimTypes.NameIdentifier);
            if (claimValue != null && int.TryParse(claimValue.Value, out var userId))
                return userId;

            return null;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var claimValue = user.FindFirst(ClaimTypes.NameIdentifier);
            if (claimValue != null && int.TryParse(claimValue.Value, out var userId))
                return userId;

            throw new ApplicationException("User Id could not be found");
        }
        public static int? GetOriginalUserId(this ClaimsPrincipal user)
        {
            return user.GetNullableIntClaim("osub");
        }

        private static int? GetNullableIntClaim(this ClaimsPrincipal user, string valueName)
        {
            var userIdStr = user.Claims.FirstOrDefault(x => x.Type == valueName);

            if (userIdStr != null)
            {
                var userId = int.Parse(userIdStr.Value);
                return userId;
            }

            return null;
        }

        public static int GetOriginalOrDefaultUserId(this ClaimsPrincipal user)
        {
            var userId = user.GetOriginalUserId();
            if (userId != null) return userId.Value;
            return user.GetUserId();
        }
    }
}
