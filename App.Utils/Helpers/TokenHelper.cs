using App.Common.Abstract.Helpers;
using App.Common.Campatibility.Configurations;
using App.Utils.Extensions;
using NodaTime;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Utils.Helpers
{
    public static class TokenHelper
    {
        public static TokenResponse GetToken(
            string userId,
            //Guid userGuid,
           // IEnumerable<string> userRoles,
            TokenOptions options,
            Instant? manualExpireTime = null,
            //int? clientId = null,
            ulong[] claimValues = null,
            int? originalUserId = null,
            int? originalClientId = null,
            Guid? originalUserGuid = null
        )
        {
            var now = SystemClock.Instance.GetCurrentInstant();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            //foreach (var role in userRoles)
            //{
            //    claims.Add(new Claim("role", role));
            //}

            //if (!userGuid.Equals(Guid.Empty)) claims.Add(new Claim("guid", userGuid.ToString()));
           // if (clientId.HasValue) claims.Add(new Claim("cid", clientId.ToString()));

            if (claimValues != null)
            {
                for (int i = 0; i < claimValues.Length; i++)
                {
                    claims.Add(new Claim($"clv{i + 1}", claimValues[i].ToString()));
                }
            }

            //Add original value if not equals default
            //if (originalUserId.HasValue && originalUserId != userId) claims.Add(new Claim("osub", originalUserId.ToString()));
           // if (originalClientId.HasValue && originalClientId != clientId) claims.Add(new Claim("ocid", originalClientId.ToString()));
           // if (originalUserGuid.HasValue && !originalUserGuid.Equals(userGuid)) claims.Add(new Claim("oguid", originalUserGuid.ToString()));


            Instant expires;

            //Expires day after at 03:00
            if (!manualExpireTime.HasValue)
            {
                var localDateTime = now.InZone(NodaTimeHelper.ApplicationTimeZone).LocalDateTime;
                localDateTime = localDateTime.PlusDays(1);
                localDateTime = new LocalDateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day, 3, 0);
                expires = localDateTime.InZoneLeniently(NodaTimeHelper.ApplicationTimeZone).ToInstant();
            }
            else
            {
                expires = manualExpireTime.Value;
            }

            // WARNING DO NOT UNCOMMENT THIS LINE //
            //expires = NodaTimeHelper.Now + Duration.FromDays(2 * 365) // For debug purpose;

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: now.ToDateTimeUtc(),
                expires: expires.ToDateTimeUtc(),
                signingCredentials: options.TokenSigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponse()
            {
                Token = encodedJwt,
                Expires = expires
            };

            return response;
        }

        private static readonly Random Random = new Random();

        public static string ModifyTokenAndReturnSignature(TokenResponse token)
        {
            var tokenParts = token.Token.Split('.');
            var signature = tokenParts[2];

            var sb = new StringBuilder();
            for (int i = 0; i < signature.Length * 2; i++)
            {
                sb.Append((char)Random.Next('A', 'z'));
            }

            tokenParts[2] = sb.ToString().AsBase64().Substring(0, signature.Length);
            token.Token = string.Join(".", tokenParts);
            return signature;
        }


        public class TokenResponse
        {
            public string Token { get; set; }
            public Instant Expires { get; set; }
        }
    }
}
