
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Crear
{
    public class CrearSolicitudCesantiaHandler : IRequestHandler<CrearSolicitudCesantiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IReadOnlyRepository repositorio;

        public CrearSolicitudCesantiaHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(CrearSolicitudCesantiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var req = httpContextAccessor.HttpContext.Request.Headers;

                // Consulta anticipos solicitados
                var anticiposSolicitados = contexto.SolicitudCesantias.Where(x => x.FuncionarioId == request.FuncionarioId &&
                                                                                    x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                  (x.Estado == EstadoCesantia.Aprobada ||
                                                                                  x.Estado == EstadoCesantia.Finalizada))
                                                                        .GroupBy(x => x.FuncionarioId)
                                                                        .Select(x => x.Sum(i => i.ValorSolicitado))
                                                                        .FirstOrDefault();

                double BaseCesantias = 0;
                double totalCesantias = 0;
                var consultarProcedure = repositorio.Query($@"SELECT * FROM [dbo].[UFT_ObtenerDatosCesantias] ({(int)request.FuncionarioId})");
                var respuestaProcedure = consultarProcedure.First();

                if (consultarProcedure.Any())
                {
                    BaseCesantias = respuestaProcedure.BaseCesantias;
                    totalCesantias = respuestaProcedure.ValorIntresesCesantiasAcumuladas +
                                     respuestaProcedure.ValorCesantiasAcumuladas -
                                     anticiposSolicitados;
                }

                if (totalCesantias < request.ValorSolicitado)
                {
                    return CommandResult.Fail("El valor solicitado excede el valor máximo de anticipo a las cesantías.", 404);
                }

                SolicitudCesantia solicitudCesantia = new SolicitudCesantia();
                solicitudCesantia.FuncionarioId = (int)request.FuncionarioId;
                solicitudCesantia.MotivoSolicitudCesantiaId = (int)request.MotivoSolicitudCesantiaId;
                solicitudCesantia.FechaSolicitud = DateTime.Now;
                solicitudCesantia.BaseCalculoCesantia = BaseCesantias;
                solicitudCesantia.ValorSolicitado = (double)request.ValorSolicitado;
                solicitudCesantia.Soporte = request.Soporte;
                solicitudCesantia.Observacion = request.Observacion;
                if (
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() != null
                    &&
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() == this.configuration.GetValue<string>(Constants.ClienteMovil.Value))
                {
                    solicitudCesantia.Estado = Infraestructura.Enumerador.EstadoCesantia.EnTramite;
                }
                else
                {
                    solicitudCesantia.Estado = Infraestructura.Enumerador.EstadoCesantia.Aprobada;
                }
                contexto.SolicitudCesantias.Add(solicitudCesantia);

                await contexto.SaveChangesAsync();

                return CommandResult.Success(solicitudCesantia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
