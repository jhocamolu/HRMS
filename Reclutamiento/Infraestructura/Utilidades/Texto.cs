using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Reclutamiento.Infraestructura.Utilidades
{
    public static class Texto
    {
        public static string LetraCapital(string oracion)
        {
            if (string.IsNullOrEmpty(oracion))
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
            if (string.IsNullOrEmpty(oracion))
            {
                return null;
            }
            else
            {
                oracion = oracion.ToLower();
                return oracion.First().ToString().ToUpper() + oracion.Substring(1);
            }
        }

        public static string QuitarAcentos(string inputString)
        {
            inputString = inputString.ToLower();
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            Regex replace_n_Accents = new Regex("[ñ]", RegexOptions.Compiled);
            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_u_Accents.Replace(inputString, "u");
            inputString = replace_n_Accents.Replace(inputString, "n");
            return inputString;
        }
    }
}