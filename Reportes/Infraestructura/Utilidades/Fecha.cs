namespace Reportes.Infraestructura.Utilidades
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


    }
}
