using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Libranzas.Comandos.Parcial
{
    public class ParcialLibranzaHandler : IRequestHandler<ParcialLibranzaRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ParcialLibranzaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialLibranzaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Libranza libranza = this.contexto.Libranzas.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        libranza.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        libranza.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.FuncionarioId != null)
                {
                    libranza.FuncionarioId = (int)request.FuncionarioId;
                }
                if (request.EntidadFinancieraId != null)
                {
                    libranza.EntidadFinancieraId = (int)request.EntidadFinancieraId;
                }
                if (request.FechaInicio != null)
                {
                    libranza.FechaInicio = (DateTime)request.FechaInicio;
                }
                if (request.ValorPrestamo != null)
                {
                    libranza.ValorPrestamo = (double)request.ValorPrestamo;
                }
                if (request.FechaInicio > DateTime.Today)
                {
                    libranza.Estado = EstadoLibranza.Pendiente;
                }
                else
                {
                    libranza.Estado = EstadoLibranza.Vigente;
                }
                if (request.NumeroCuotas != null)
                {
                    libranza.NumeroCuotas = (int)request.NumeroCuotas;
                }
                if (request.Observacion != null)
                {
                    libranza.Observacion = request.Observacion;
                }
                if (request.ValorCuota != null)
                {
                    libranza.ValorCuota = (double)request.ValorCuota;
                }
                contexto.Libranzas.Update(libranza);
                await contexto.SaveChangesAsync();

                //Eliminamos los subperiodos para la libranza
                var libranzaSubperiodosBorrar = this.contexto.LibranzaSubperiodos.Where(x => x.LibranzaId == request.Id)
                                                                                        .ToList();
                foreach (var item in libranzaSubperiodosBorrar)
                {
                    this.contexto.LibranzaSubperiodos.Remove(item);
                    await this.contexto.SaveChangesAsync();
                }

                // Creamos los subperíodos para la libranza
                foreach (var item in request.LibranzasSubperiodo)
                {
                    LibranzaSubperiodo libranzaSubperiodo = new LibranzaSubperiodo();
                    libranzaSubperiodo.LibranzaId = libranza.Id;
                    libranzaSubperiodo.SubPeriodoId = (int)item.SubperiodoId;

                    this.contexto.LibranzaSubperiodos.Add(libranzaSubperiodo);
                    await this.contexto.SaveChangesAsync();
                }
                libranza.LibranzaSubperiodos = null;
                return CommandResult.Success(libranza);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);

            }
        }
    }
}
