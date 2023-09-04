using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PemUtils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWT
{
    public class TokenService
    {
        private string _privateFilePath;
        private string _publicFilePath;
        
        public TokenService(string privateFilePath, string publicFilePath)
        {
            _privateFilePath = privateFilePath;
            _publicFilePath = publicFilePath;
        }
        
        public string Generate(List<Claim> claims)
        {
            RsaSecurityKey rsaKey = GetRsaSecurityKey(_privateFilePath);

            var signingCredentials = new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);

            var tokenDescrpitor = new SecurityTokenDescriptor
            {
                Claims = claims.ToDictionary(c => c.Type, c => (object)c.Value),
                SigningCredentials = signingCredentials
            };

            var handler = new JsonWebTokenHandler();

            var token = handler.CreateToken(tokenDescrpitor);

            return token;
        }
        
        public string Verify(string token)
        {
            RsaSecurityKey rsaKey = GetRsaSecurityKey(_publicFilePath);

            var validationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                IssuerSigningKey = rsaKey
            };

            var handler = new JwtSecurityTokenHandler();
            
            var payload = handler.ValidateToken(token, validationParameters, out var validatedToken);

            return JsonSerializer.Serialize(payload.Claims.Select(s => s.Type + ": " + s.Value));
        }

        private RsaSecurityKey GetRsaSecurityKey(string filePath)
        {
            var fileBytes = File.ReadAllBytes(filePath);

            RsaSecurityKey rsaKey;
            using (MemoryStream mStrm = new MemoryStream(fileBytes))
            {
                using (var reader = new PemReader(mStrm))
                {
                    rsaKey = new RsaSecurityKey(reader.ReadRsaKey());
                }
            }

            return rsaKey;
        }
    }
}
