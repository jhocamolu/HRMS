using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Beneficios.Comandos.Crear
{
    public class CrearBeneficiohandler : IRequestHandler<CrearBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearBeneficiohandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Beneficio beneficio = new Beneficio
                {
                    FuncionarioId = (int)request.FuncionarioId,
                    TipoBeneficioId = (int)request.TipoBeneficioId,
                    FechaSolicitud = (DateTime)request.FechaSolicitud,
                    ValorSolicitud = request.ValorSolicitud,
                    PlazoMaximo = request.PlazoMaximo,
                    TipoPeriodoId = request.TipoPeriodoId,
                    OpcionAuxilioEducativo = request.OpcionAuxilioEducativo,
                    CantidadHoraSemana = request.CantidadHoraSemana,
                    FechaInicioEstudio = request.FechaInicioEstudio,
                    FechaFinalizacionEstudio = request.FechaFinalizacionEstudio,
                    Observacion = request.Observacion,
                    Estado = EstadoBeneficiosCorportativos.EnTramite,
                    ObservacionAprobacion = request.ObservacionAprobacion,
                    ObservacionAutorizacion = request.ObservacionAutorizacion,
                    Saldo = 0
                };

                this.contexto.Beneficios.Add(beneficio);
                await this.contexto.SaveChangesAsync();

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
                return CommandResult.Success(beneficio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}