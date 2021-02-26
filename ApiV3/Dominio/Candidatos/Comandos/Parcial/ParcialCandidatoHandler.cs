using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Candidatos.Comandos.Parcial
{
    public class ParcialCandidatoHandler : IRequestHandler<ParcialCandidatoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialCandidatoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialCandidatoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Candidato candidato = await contexto.Candidatos.FindAsync(request.Id);
                if (request.Activo != null)
                {
                    candidato.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false)
                    {
                        candidato.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

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