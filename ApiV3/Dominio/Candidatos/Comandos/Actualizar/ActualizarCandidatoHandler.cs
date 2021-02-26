using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Candidatos.Comandos.Actualizar
{
    public class ActualizarCandidatoHandler : IRequestHandler<ActualizarCandidatoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCandidatoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCandidatoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Candidato candidato = await contexto.Candidatos.FindAsync(request.Id);
                candidato.HojaDeVidaId = (int)request.HojaDeVidaId;
                candidato.RequisicionPersonalId = (int)request.RequisicionPersonalId;
                candidato.Estado = (EstadoCandidato)request.Estado;
                if (request.Justificacion != null) { candidato.Justificacion = request.Justificacion; }
                if (request.AdjuntoExamen != null) { candidato.AdjuntoExamen = request.AdjuntoExamen; }
                if (request.AdjuntoPruebas != null) { candidato.AdjuntoPruebas = request.AdjuntoPruebas; }

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