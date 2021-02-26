
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using Reclutamiento.Models;
using Reclutamiento.Servicios.Peticion;
using Reclutamiento.Support;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Eliminar
{
    public class EliminarHojaDeVidaExperienciaLaboralHandler : Peticion, IRequestHandler<EliminarHojaDeVidaExperienciaLaboralRequest, CommandResult>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<UsuarioAplicacion> userManager;
        private readonly ReclutamientoDbContext contexto;

        public EliminarHojaDeVidaExperienciaLaboralHandler(IConfiguration configuration, IPeticionService peticion, IHttpContextAccessor httpContextAccessor, UserManager<UsuarioAplicacion> userManager, ReclutamientoDbContext contexto)
        {
            this.configuration = configuration;
            this.peticion = peticion;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarHojaDeVidaExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tokenPortal = InformacionToken.ObtenerTokenUsuario(httpContextAccessor);
                if (tokenPortal != "")
                {
                    var usuario = InformacionToken.ObtenerInformacionUsuario(tokenPortal, "unique_name", this.userManager);

                    var consulta = this.contexto.HojaDeVidaExperienciaLaborales.FirstOrDefault(x => x.Id == request.Id);
                    var consultaHojaDeVida = this.contexto.HojaDeVidas.FirstOrDefault(x => x.Id == consulta.HojaDeVidaId);

                    if (usuario.UserName != consultaHojaDeVida.CorreoElectronicoPersonal)
                    {
                        return CommandResult.Fail("La información suministrada no es correcta, para realizar la acción", 400);
                    }

                    var host = this.configuration.GetValue<string>(Constants.ServiceApi.GHESTIC);
                    var list = await this.EliminarServicios($"{host}/api/HojadevidaExperiencialaborales/" + request.Id, usuario.TokenGhestic);
                    if (list.IsSuccessStatusCode)
                    {
                        return CommandResult.Success();
                    }
                    else
                    {
                        return CommandResult.Fail(list.ReasonPhrase, (int)list.StatusCode);
                    }
                }
                else
                {
                    return CommandResult.Fail("unauthorized", 401);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
