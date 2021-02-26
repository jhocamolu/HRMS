using System;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Ayuda.Dominio.Utilidades
{
    public static class Texto
    {
        public static string LetraCapital(string oracion)
        {
            if (String.IsNullOrEmpty(oracion))
                throw new ArgumentException("Error");
            TextInfo texto = new CultureInfo("es-ES", false).TextInfo;
            return texto.ToTitleCase(oracion);
        }
        public static string TipoOracion(string oracion)
        {
            oracion = oracion.ToLower();
            if (String.IsNullOrEmpty(oracion))
                throw new ArgumentException("Error");
            return oracion.First().ToString().ToUpper() + oracion.Substring(1);
        }
    }
}