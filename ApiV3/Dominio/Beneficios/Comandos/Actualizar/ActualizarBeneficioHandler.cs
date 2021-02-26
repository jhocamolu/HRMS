using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Beneficios.Comandos.Actualizar
{
    public class ActualizarBeneficioHandler : IRequestHandler<ActualizarBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Beneficio beneficio = await this.contexto.Beneficios.FindAsync(request.Id);

                beneficio.ValorSolicitud = request.ValorSolicitud;
                beneficio.PlazoMaximo = request.PlazoMaximo;
                beneficio.TipoPeriodoId = request.TipoPeriodoId;
                beneficio.OpcionAuxilioEducativo = request.OpcionAuxilioEducativo;
                beneficio.CantidadHoraSemana = request.CantidadHoraSemana;
                beneficio.FechaInicioEstudio = request.FechaInicioEstudio;
                beneficio.FechaFinalizacionEstudio = request.FechaFinalizacionEstudio;
                beneficio.Observacion = request.Observacion;
                beneficio.ValorAutorizado = request.ValorAutorizado;

                this.contexto.Beneficios.Update(beneficio);
                await this.contexto.SaveChangesAsync();

                //Eliminamos los Subperiodos Existentes para ese Embargo
                if (request.BeneficiosSubperiodos.Count() > 0)
                {
                    string tabla = typeof(BeneficioSubperiodo).Name;
                    this.contexto.Database
                                 .ExecuteSqlRaw($"DELETE FROM {tabla} WHERE BeneficioId ={ request.Id}");

                    //Creamos los Subperiodos
                    int numeroItem = request.BeneficiosSubperiodos.Count();
                    int contador = 0;
                    foreach (var item in request.BeneficiosSubperiodos)
                    {
                        contador++;

                        BeneficioSubperiodo beneficiosSubperiodo = new BeneficioSubperiodo
                        {
                            BeneficioId = beneficio.Id,
                            SubPeriodoId = (int)item.SubPeriodoId
                        };

                        this.contexto.BeneficioSubperiodos.Add(beneficiosSubperiodo);
                        await this.contexto.SaveChangesAsync();
                    }
                }


                if (request.BeneficiosAdjuntos.Count() > 0)
                {
                    string tablaAdjunto = typeof(BeneficioAdjunto).Name;
                    this.contexto.Database
                                 .ExecuteSqlRaw($"DELETE FROM {tablaAdjunto} WHERE BeneficioId ={ request.Id}");
                    foreach (var adjunto in request.BeneficiosAdjuntos)
                    {

                        BeneficioAdjunto beneficioAdjunto = new BeneficioAdjunto
                        {
                            BeneficioId = beneficio.Id,
                            TipoBeneficioRequisitoId = adjunto.TipoBeneficioRequisitoId,
                            Adjunto = adjunto.AdjuntoId
                        };

                        this.contexto.BeneficioAdjuntos.Add(beneficioAdjunto);
                        await this.contexto.SaveChangesAsync();
                    }
                }
                return CommandResult.Success(beneficio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
