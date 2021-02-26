using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Reclutamiento.Infraestructura.Utilidades
{

    public class InformacionToken
    {

        public static dynamic ObtenerTokenUsuario(IHttpContextAccessor _httpContextAccessor)
        {
            try
            {
                var tokenPortal = "";
                var caracteristicas = _httpContextAccessor.HttpContext.Features.Get<IHttpRequestFeature>();
                foreach (var item in caracteristicas.Headers)
                {
                    if (item.Key == "Authorization")
                    {
                        tokenPortal = item.Value;
                    }
                }
                return tokenPortal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }


        public static dynamic ObtenerInformacionUsuario(string token,string key, UserManager<UsuarioAplicacion> userManager)
        {
            try
            {
                token = token.Replace("Bearer ", "");
                var decodedToken = new JwtSecurityToken(token);
                var claims = decodedToken.Claims.ToList();
                string valor = null;
                foreach (var claim in claims)
                {
                    if (claim.Type == key)
                    {
                        valor = claim.Value;
                    }
                }
                if (key == "unique_name")
                {
                    
                    var usuario = userManager.Users.FirstOrDefault(x=> x.UserName == valor);
                    if (usuario != null)
                    {
                        return usuario;
                    }
                    else
                    { return ""; }
                    
                }
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static dynamic ValidaUsuario(ReclutamientoDbContext contexto, int hojaDeVidaId)
        {
            try
            {
                var usuarioAutenticado = contexto.HojaDeVidas.FirstOrDefault(x=> x.Id == hojaDeVidaId);
                return usuarioAutenticado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
