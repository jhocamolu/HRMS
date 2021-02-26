using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Beneficios.Comandos.Parcial
{
    public class ParcialBeneficioHandler : IRequestHandler<ParcialBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Beneficio beneficio = await this.contexto.Beneficios.FindAsync(request.Id);

                if (request.ValorSolicitud != null) beneficio.ValorSolicitud = request.ValorSolicitud;
                if (request.PlazoMaximo != null) beneficio.PlazoMaximo = request.PlazoMaximo;
                if (request.TipoPeriodoId != null) beneficio.TipoPeriodoId = request.TipoPeriodoId;
                if (request.OpcionAuxilioEducativo != null) beneficio.OpcionAuxilioEducativo = request.OpcionAuxilioEducativo;
                if (request.CantidadHoraSemana != null) beneficio.CantidadHoraSemana = request.CantidadHoraSemana;
                if (request.FechaInicioEstudio != null) beneficio.FechaInicioEstudio = request.FechaInicioEstudio;
                if (request.FechaFinalizacionEstudio != null) beneficio.FechaFinalizacionEstudio = request.FechaFinalizacionEstudio;
                if (request.Observacion != null) beneficio.Observacion = request.Observacion;
                if (request.Estado != null) beneficio.Estado = (EstadoBeneficiosCorportativos)request.Estado;
                if (request.ObservacionAprobacion != null) beneficio.ObservacionAprobacion = request.ObservacionAprobacion;
                if (request.ObservacionAutorizacion != null) beneficio.ObservacionAutorizacion = request.ObservacionAutorizacion;
                if (request.ValorAutorizado != null) beneficio.ValorAutorizado = request.ValorAutorizado;
                if (request.ValorCobrar != null) beneficio.ValorCobrar = request.ValorCobrar;
                if (request.NotaAcademica != null) beneficio.NotaAcademica = request.NotaAcademica;
                if (request.ObservacionNotaAcademica != null) beneficio.ObservacionNotaAcademica = request.ObservacionNotaAcademica;
                if (beneficio.EstadoRegistro != null)
                {
                    beneficio.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false)
                    {
                        beneficio.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                this.contexto.Beneficios.Update(beneficio);
                await this.contexto.SaveChangesAsync();



                #region Eliminamos y Creamos los Subperiodos
                if (request.BeneficiosSubperiodos.Count() > 0)
                {
                    //Eliminamos los Subperiodos Existentes para ese Embargo
                    string tabla = typeof(BeneficioSubperiodo).Name;
                    this.contexto.Database
                                 .ExecuteSqlRaw($"DELETE FROM {tabla} WHERE BeneficioId ={ request.Id}");

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
                #endregion

                #region Eliminamos y Creamos los Adjuntos
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
                #endregion

                return CommandResult.Success(beneficio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
