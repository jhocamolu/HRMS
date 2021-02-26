using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Actualizar
{
    public class ActualizarSolicitudCesantiaHandler : IRequestHandler<ActualizarSolicitudCesantiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IReadOnlyRepository repositorio;


        public ActualizarSolicitudCesantiaHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ActualizarSolicitudCesantiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
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

                SolicitudCesantia solicitudCesantia = this.contexto.SolicitudCesantias.Find(request.Id);
                solicitudCesantia.FuncionarioId = (int)request.FuncionarioId;
                solicitudCesantia.MotivoSolicitudCesantiaId = (int)request.MotivoSolicitudCesantiaId;
                solicitudCesantia.BaseCalculoCesantia = BaseCesantias;
                solicitudCesantia.ValorSolicitado = (double)request.ValorSolicitado;
                solicitudCesantia.Soporte = request.Soporte;
                solicitudCesantia.Observacion = request.Observacion;

                contexto.SolicitudCesantias.Update(solicitudCesantia);
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
