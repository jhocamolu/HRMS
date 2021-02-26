
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using Reclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Dominio.Autenticacion.RecordarClave
{
    public class RecordarClaveHandler : GenerarToken, IRequestHandler<RecordarClaveRequest, CommandResult>
    {
        protected IConfiguration configuration;
        private readonly UserManager<UsuarioAplicacion> userManager;
        private readonly ReclutamientoDbContext contexto;

        public RecordarClaveHandler(IConfiguration configuration, UserManager<UsuarioAplicacion> userManager, ReclutamientoDbContext contexto)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(RecordarClaveRequest request, CancellationToken cancellationToken)
        {
            try
            {
                
                var clave = GenerarClaveAleatoria();
                var user = await userManager.FindByNameAsync(request.CorreoElectronicoPersonal);
                if (user != null)
                {
                    await userManager.RemovePasswordAsync(user);
                    await userManager.AddPasswordAsync(user, clave);
                    await userManager.UpdateAsync(user);

                    // Crea notificación vía email.
                    string alias = "NotificacionRecuperacionContraseña";

                    var informacionHojaDeVida = contexto.HojaDeVidas.FirstOrDefault(x=> x.CorreoElectronicoPersonal == request.CorreoElectronicoPersonal);

                    // Llamar al metodo encargado de construir las notificaciones
                    var crearNotificacion = NotificacionesPortal.CrearNotificacionPortal(informacionHojaDeVida.Id, alias, clave, contexto, null);

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
                        notificacionDestinatario.CorreoElectronico = request.CorreoElectronicoPersonal;
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
  
                    Dictionary<string,string> mensaje = new Dictionary<string, string>();
                    mensaje.Add("message", "Se ha enviado al correo electrónico suministrado la contraseña.");

                    return CommandResult.Success(mensaje);
                }
                else
                {
                    return CommandResult.Fail("El correo electrónico que ingresaste no está registrado.",404);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
       
    }
}
