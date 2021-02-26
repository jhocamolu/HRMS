using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AnnoTrabajos.Comandos.Parcial
{
    public class ParcialAnnoVigenciaHandler : IRequestHandler<ParcialAnnoVigenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialAnnoVigenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialAnnoVigenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AnnoVigencia annoVigencia = contexto.AnnoVigencias.Find(request.Id);
                annoVigencia.Estado = (EstadoAnnoVigencia)request.Estado;
                contexto.AnnoVigencias.Update(annoVigencia);
                await contexto.SaveChangesAsync();

                if (request.Estado == EstadoAnnoVigencia.Vigente)
                {
                    //Cambia a todos los demas registros a cerrado.
                    var annoVigenciaVigente = contexto.AnnoVigencias.FirstOrDefault(x => x.Estado == EstadoAnnoVigencia.Vigente &&
                                                                                    x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                    x.Id != request.Id);
                    if (annoVigenciaVigente != null)
                    {
                        annoVigenciaVigente.Estado = EstadoAnnoVigencia.Cerrado;
                        contexto.AnnoVigencias.Update(annoVigenciaVigente);
                        await contexto.SaveChangesAsync();
                    }

                }

                return CommandResult.Success(annoVigencia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
