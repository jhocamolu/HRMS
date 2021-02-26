using System;
using System.Globalization;
using System.Linq;

namespace Plantillas.Dominio.Utilidades
{
    public static class Texto
    {
        public static string LetraCapital(string oracion)
        {
            if (String.IsNullOrEmpty(oracion))
            {
                return null;
            }
            else
            {
                oracion = oracion.ToLower();
                TextInfo texto = new CultureInfo("es-ES", false).TextInfo;
                return texto.ToTitleCase(oracion);
            }
        }
        public static string TipoOracion(string oracion = null)
        {
            if (String.IsNullOrEmpty(oracion))
            {
                return null;
            }
            else
            {
                oracion = oracion.ToLower();
                return oracion.First().ToString().ToUpper() + oracion.Substring(1);
            }
        }
    }
}