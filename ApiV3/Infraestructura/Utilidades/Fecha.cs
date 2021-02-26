using System;
using System.Collections.Generic;

namespace ApiV3.Infraestructura.Utilidades
{
    public class Fecha
    {
        public static string NoPuedeSerMenor(string campoinicial, string valorConFormato, string campoFinal)
        {
            if (valorConFormato.Length > 1)
            {
                return "La " + campoinicial + " que ingresaste no puede ser menor a " + valorConFormato + " que la " + campoFinal + ".";
            }
            else
            {
                return "La " + campoinicial + " que ingresaste no puede ser menor a la " + campoFinal + ".";
            }
        }

        public static string NoPuedeSerMayor(string campoinicial, string valorConFormato, string campoFinal)
        {
            if (valorConFormato.Length > 1)
            {
                return "La " + campoinicial + " que ingresaste no puede ser mayor a " + valorConFormato + " que la " + campoFinal + ".";
            }
            else
            {
                return "La " + campoinicial + " que ingresaste no puede ser mayor  a la " + campoFinal + ".";
            }
        }

        public static Dictionary<string, string> CalculoDifencia(DateTime incial, DateTime final)
        {
            string enLetras;
            var diferencia = (final - incial).Days;
            double valor = 0;

            enLetras = "";
            if (diferencia <= 31)
            {
                valor = diferencia;
                enLetras = NumeroLetras.Enletras(valor.ToString()) + "días";
            }
            else if (diferencia >= 31 && diferencia <= 365)
            {
                valor = diferencia / 30;
                enLetras = NumeroLetras.Enletras(valor.ToString()) + "meses";
            }
            else if (diferencia > 365)
            {
                valor = diferencia / 365;
                enLetras = NumeroLetras.Enletras(valor.ToString()) + "años";
            }
            Dictionary<string, string> datos = new Dictionary<string, string>();
            datos.Add("enNumero", valor.ToString());
            datos.Add("enLetras", enLetras);

            return datos;
        }

    }
}
