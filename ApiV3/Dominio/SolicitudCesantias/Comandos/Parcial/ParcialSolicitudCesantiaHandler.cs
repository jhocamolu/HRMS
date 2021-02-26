using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Parcial
{
    public class ParcialSolicitudCesantiaHandler : IRequestHandler<ParcialSolicitudCesantiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IConfiguration configuration;
        private readonly IReadOnlyRepository repositorio;

        public ParcialSolicitudCesantiaHandler(NominaDbContext contexto, IConfiguration configuration, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.configuration = configuration;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ParcialSolicitudCesantiaRequest request, CancellationToken cancellationToken)
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

                if (consultarProcedure.Any())
                {
                    BaseCesantias = consultarProcedure.First().BaseCesantias;
                    totalCesantias = consultarProcedure.First().ValorIntresesCesantiasAcumuladas +
                                     consultarProcedure.First().ValorCesantiasAcumuladas -
                                     anticiposSolicitados;
                }

                if (totalCesantias < request.ValorSolicitado)
                {
                    return CommandResult.Fail("El Valor solicitado excede el valor máximo de anticipo a las cesantías.", 404);
                }


                SolicitudCesantia solicitudCesantia = this.contexto.SolicitudCesantias.Find(request.Id);

                if (request.FuncionarioId != null)
                {
                    solicitudCesantia.FuncionarioId = (int)request.FuncionarioId;
                }
                if (request.MotivoSolicitudCesantiaId != null)
                {
                    solicitudCesantia.MotivoSolicitudCesantiaId = (int)request.MotivoSolicitudCesantiaId;
                }
                if (BaseCesantias != 0)
                {
                    solicitudCesantia.BaseCalculoCesantia = BaseCesantias;
                }
                if (request.ValorSolicitado != null)
                {
                    solicitudCesantia.ValorSolicitado = (double)request.ValorSolicitado;
                }
                if (request.Soporte != null)
                {
                    solicitudCesantia.Soporte = request.Soporte;
                }
                if (request.Observacion != null)
                {
                    solicitudCesantia.Observacion = request.Observacion;
                }

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
