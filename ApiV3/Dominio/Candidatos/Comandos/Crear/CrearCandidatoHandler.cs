using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Candidatos.Comandos.Crear
{
    public class CrearCandidatoHandler : IRequestHandler<CrearCandidatoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCandidatoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCandidatoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Candidato candidato = new Candidato { };
                candidato.HojaDeVidaId = (int)request.HojaDeVidaId;
                candidato.RequisicionPersonalId = (int)request.RequisicionPersonalId;
                candidato.Estado = EstadoCandidato.Postulado;
                if (request.Justificacion != null) { candidato.Justificacion = request.Justificacion; }
                if (request.AdjuntoExamen != null) { candidato.AdjuntoExamen = request.AdjuntoExamen; }
                if (request.AdjuntoPruebas != null) { candidato.AdjuntoPruebas = request.AdjuntoPruebas; }

                // Crea notificación vía email.
                string alias = "NotificacionAplicacionConvocatoria";
                var clave = "";

                var informacionHojaDeVida = contexto.HojaDeVidas.FirstOrDefault(x => x.Id == request.HojaDeVidaId);

                // Llamar al metodo encargado de construir las notificaciones
                var crearNotificacion = NotificacionesPortal.CrearNotificacionPortal(informacionHojaDeVida.Id, alias, clave, contexto, request.RequisicionPersonalId);

                if (crearNotificacion != null)
                {
                    //Realiza los insert en las tablas notificacion, notificacion destinatario

                    Notificacion notificacion = new Notificacion();
                    notificacion.Tipo = ApiV3.Infraestructura.Enumerador.TipoNotificacion.Email;
                    notificacion.Fecha = DateTime.Now.Date;
                    notificacion.Titulo = crearNotificacion.Titulo;
                    notificacion.Mensaje = crearNotificacion.Mensaje;
                    contexto.Notificaciones.Add(notificacion);
                    await contexto.SaveChangesAsync();

                    // Realiza el insert a la tabla de notificacionDestinatario
                    NotificacionDestinatario notificacionDestinatario = new NotificacionDestinatario();
                    notificacionDestinatario.CorreoElectronico = informacionHojaDeVida.CorreoElectronicoPersonal;
                    notificacionDestinatario.NotificacionId = notificacion.Id;
                    notificacionDestinatario.Estado = ApiV3.Infraestructura.Enumerador.EstadoNotificacion.Pendiente;
                    contexto.NotificacionDestinatarios.Add(notificacionDestinatario);
                    await contexto.SaveChangesAsync();


                    var tareaProgramada = this.contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == "notificacion-email");
                    if (tareaProgramada != null)
                    {
                        if (tareaProgramada.EnEjecucion)
                        {
                            return CommandResult.Fail("Tarea en ejecucion", 400);
                        }

                        ProcessStartInfo startInfo = new ProcessStartInfo("powershell.exe")
                        {
                            WindowStyle = ProcessWindowStyle.Minimized,
                            Arguments = tareaProgramada.Instruccion + " " + notificacion.Id
                        };
                        Process.Start(startInfo);
                    }

                }

                this.contexto.Candidatos.Add(candidato);
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