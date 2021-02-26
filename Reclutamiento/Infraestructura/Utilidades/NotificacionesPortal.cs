using ApiV3.Models;
using Reclutamiento.Infraestructura.DbContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Infraestructura.Utilidades
{
    public class NotificacionesPortal
    {
        public static dynamic CrearNotificacionPortal(int hojaDeVidaId, string alias, string clave , ReclutamientoDbContext contexto, int? requisicionId)
        {
            //NotificacionRegistroPortalReclutamietno
            //Consulta información de la plantilla
            var consultaPlantillaNotificacion = contexto.NotificacionPlantillas.FirstOrDefault(x => x.Alias == alias);

            // Consulta información de la hoja de vida
            var consultaHojaDeVida = contexto.HojaDeVidas.FirstOrDefault(x=> x.Id == hojaDeVidaId);

            //Consulta información básica de la compañia
            var consultaInformacionCompañia = contexto.InformacionBasicas.FirstOrDefault();

            //Consulta infomarmación requisicion 
            var consultaRequsicion = contexto.RequisicionPersonales.FirstOrDefault(x=> x.Id == requisicionId);

            //Reemplaza valores en la plantilla.
            string mensajeNotificacion = "";
            if (consultaPlantillaNotificacion != null && consultaHojaDeVida != null && consultaInformacionCompañia != null)
            {
                mensajeNotificacion = consultaPlantillaNotificacion.Plantilla;
                mensajeNotificacion = mensajeNotificacion.Replace("|&|PRIMERNOMBREUSUARIO|&|", consultaHojaDeVida.PrimerNombre);
                mensajeNotificacion = mensajeNotificacion.Replace("|&|RAZONSOCIAL|&|", consultaInformacionCompañia.RazonSocial);
                mensajeNotificacion = mensajeNotificacion.Replace("|&|USUARIO|&|", consultaHojaDeVida.CorreoElectronicoPersonal);
                mensajeNotificacion = mensajeNotificacion.Replace("|&|CLAVE|&|", clave);
                if (consultaRequsicion != null)
                {
                    mensajeNotificacion = mensajeNotificacion.Replace("|&|CARGOSOLICITADO|&|", consultaRequsicion.CargoDependenciaSolicitado.Cargo.Nombre);
                }
            }

         
            dynamic respuesta = "";
            if (consultaPlantillaNotificacion != null)
            {
                respuesta = new
                {
                    Titulo = consultaPlantillaNotificacion.Descripcion,
                    Mensaje = mensajeNotificacion
                };
            }
            

            return respuesta;
        }
    }
}
