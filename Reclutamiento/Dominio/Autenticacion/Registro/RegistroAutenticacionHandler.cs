
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


namespace Reclutamiento.Dominio.Autenticacion.Registro
{
    public class RegistroAutenticacionHandler : GenerarToken, IRequestHandler<RegistroAutenticacionRequest, CommandResult>
    {
        private readonly UserManager<UsuarioAplicacion> userManager;
        protected IConfiguration configuration;
        private readonly ReclutamientoDbContext contexto;

        public RegistroAutenticacionHandler(UserManager<UsuarioAplicacion> userManager, IConfiguration configuration, ReclutamientoDbContext contexto)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(RegistroAutenticacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Crear registro en Hoja de vida
                HojaDeVida informacionHojaDeVida = new HojaDeVida
                {
                    #region Carga de Datos
                    PrimerNombre = Texto.LetraCapital(request.PrimerNombre),
                    SegundoNombre = request.SegundoNombre != null ? Texto.LetraCapital(request.SegundoNombre) : null,
                    PrimerApellido = Texto.LetraCapital(request.PrimerApellido),
                    SegundoApellido = request.SegundoApellido != null ? Texto.LetraCapital(request.SegundoApellido) : null,
                    SexoId = (int)request.SexoId,
                    TipoDocumentoId = (int)request.TipoDocumentoId,
                    NumeroDocumento = request.NumeroDocumento,
                    Celular = request.Celular,
                    CorreoElectronicoPersonal = request.CorreoElectronicoPersonal
                    #endregion
                };

                contexto.HojaDeVidas.Add(informacionHojaDeVida);
                await contexto.SaveChangesAsync();

                // crear método para construir clave para el registro.
                var clave = GenerarClaveAleatoria();

                var nombre = informacionHojaDeVida.PrimerNombre + ' ' + informacionHojaDeVida.PrimerApellido;
                var user = new UsuarioAplicacion { UserName = request.CorreoElectronicoPersonal, Email = request.CorreoElectronicoPersonal, HojaDeVidaId = informacionHojaDeVida.Id, TokenGhestic = "" };
                var result = await userManager.CreateAsync(user, clave);
                
                if (result.Succeeded)
                {
                    var usuarioPortal = new UsuarioPortal { Usuario = request.CorreoElectronicoPersonal, Clave = clave };
                    UsuarioToken generarToken = ConstruirToken(usuarioPortal, nombre, informacionHojaDeVida.NumeroDocumento ,configuration);

                    string alias = "NotificacionRegistroPortalReclutamiento";

                    // Llamar al metodo encargado de construir las notificaciones
                    var crearNotificacion = NotificacionesPortal.CrearNotificacionPortal(informacionHojaDeVida.Id, alias,clave,contexto, null);


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
                                Arguments = tareaProgramada.Instruccion+" "+ notificacion.Id
                            };
                            Process.Start(startInfo);
                        }
                        
                    }

                    return CommandResult.Success(generarToken);
                }else
                {
                    var errorDescripcion = "";
                    foreach (var item in result.Errors)
                    {
                        errorDescripcion += item.Description;
                    }
                    return CommandResult.Fail(errorDescripcion);
                }
                
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
