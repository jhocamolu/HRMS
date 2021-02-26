using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Candidatos.Comandos.Estado
{
    public class EstadoCandidatoHandler : IRequestHandler<EstadoCandidatoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EstadoCandidatoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EstadoCandidatoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Candidato candidato = await contexto.Candidatos.FindAsync(request.Id);
                if (request.AdjuntoPruebas != null)
                {
                    candidato.AdjuntoPruebas = request.AdjuntoPruebas;
                }
                if (request.AdjuntoExamen != null)
                {
                    candidato.AdjuntoExamen = request.AdjuntoExamen;
                }
                if (request.Justificacion != null)
                {
                    candidato.Justificacion = request.Justificacion;
                }
                candidato.Estado = (EstadoCandidato)request.Estado;

                this.contexto.Candidatos.Update(candidato);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(candidato);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
