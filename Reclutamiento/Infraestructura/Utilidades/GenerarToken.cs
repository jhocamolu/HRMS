
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reclutamiento.Models;
using Reclutamiento.Support;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;

namespace Reclutamiento.Infraestructura.Utilidades
{
    public  class GenerarToken
    {

        public  UsuarioToken ConstruirToken(UsuarioPortal userInfo, string nombre, string numeroDocumento, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Usuario),
                new Claim(JwtRegisteredClaimNames.Jti, numeroDocumento),
                new Claim(JwtRegisteredClaimNames.NameId , nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>(Constants.JWT.KEY)));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UsuarioToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        //Método de ayuda al módulo de autenticación encargado de construir la contraseña.
        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public  string GenerarClaveAleatoria(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random Rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(Rand.Next(0, chars.Count),
                    randomChars[0][Rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(Rand.Next(0, chars.Count),
                    randomChars[1][Rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(Rand.Next(0, chars.Count),
                    randomChars[2][Rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(Rand.Next(0, chars.Count),
                    randomChars[3][Rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[Rand.Next(0, randomChars.Length)];
                chars.Insert(Rand.Next(0, chars.Count),
                    rcs[Rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
