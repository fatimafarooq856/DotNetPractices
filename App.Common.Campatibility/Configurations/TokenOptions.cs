using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.Common.Campatibility.Configurations
{
    public class TokenOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; } = "App";

        private SymmetricSecurityKey _tokenSymetricSecurityKey;
        public SymmetricSecurityKey TokenSymetricSecurityKey
        {
            get
            {
                if (_tokenSymetricSecurityKey == null)
                    _tokenSymetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));

                return _tokenSymetricSecurityKey;
            }
        }

        private SigningCredentials _tokenSigningCredentials;
        public SigningCredentials TokenSigningCredentials
        {
            get
            {
                if (_tokenSigningCredentials == null)
                    _tokenSigningCredentials = new SigningCredentials(TokenSymetricSecurityKey, SecurityAlgorithms.HmacSha256);

                return _tokenSigningCredentials;
            }
        }
    }
}
