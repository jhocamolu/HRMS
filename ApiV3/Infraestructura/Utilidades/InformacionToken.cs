using ApiV3.Infraestructura.DbContexto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ApiV3.Infraestructura.Utilidades
{

    public class InformacionToken
    {

        public static string ObtenerInformacionUsuario(dynamic token, string key)
        {
            var decodedToken = new JwtSecurityToken(token);
            var claims = decodedToken.Claims.ToList();
            string identificacion = null;
            foreach (var claim in claims)
            {
                if (claim.Type == key)
                {
                    identificacion = claim.Value;
                }
            }

            return identificacion;
        }

        public static dynamic ObtenerInformacionFuncionario(dynamic token, NominaDbContext contexto)
        {
            try
            {
                string identificacionUsuario = ObtenerInformacionUsuario(token, "Identificacion");
                if (identificacionUsuario != null || identificacionUsuario != "")
                {
                    //Consulta funcionario
                    var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.NumeroDocumento == identificacionUsuario);
                    return funcionario;
                }
                return "";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
