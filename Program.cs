using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;

namespace JWT
{
    public class Program
    {
        static void Main(string[] args)
        {
            string basePath = Environment.CurrentDirectory + "\\..\\..\\Credentials";

            string privateFilePath = $"{basePath}\\private.pem";
            string publicFilePath = $"{basePath}\\public.pem";

            TokenService tokenService = new TokenService(privateFilePath, publicFilePath);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "me@bilaldemir.dev"),
            };
            string token = tokenService.Generate(claims);

            Console.WriteLine($"JWT: {token}");
            
            Console.WriteLine();

            string verifiedClaims = tokenService.Verify(token);
            Console.WriteLine($"Verified Claims: {verifiedClaims}");
            Console.ReadKey();
        }
    }
}
