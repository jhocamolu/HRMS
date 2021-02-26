using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Libranzas.Comandos.Actualizar
{
    public class ActualizarLibranzaHandler : IRequestHandler<ActualizarLibranzaRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ActualizarLibranzaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarLibranzaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Libranza libranza = contexto.Libranzas.Find(request.Id);
                libranza.FuncionarioId = (int)request.FuncionarioId;
                libranza.EntidadFinancieraId = (int)request.EntidadFinancieraId;
                libranza.FechaInicio = (DateTime)request.FechaInicio;
                libranza.ValorPrestamo = (double)request.ValorPrestamo;
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
