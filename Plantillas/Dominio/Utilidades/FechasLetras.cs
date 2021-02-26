using System;

namespace Plantillas.Dominio.Utilidades
{
    public static class FechasLetras
    {
        private static string letras = "";
        public static string ConvertirDiaSemana(DateTime fecha)
        {
            int dia = (int)fecha.DayOfWeek;

            switch (dia)
            {
                case 1:
                    letras = "Lunes";
                    break;
                case 2:
                    letras = "Martes";
                    break;
                case 3:
                    letras = "Miércoles";
                    break;
                case 4:
                    letras = "Jueves";
                    break;
                case 5:
                    letras = "Viernes";
                    break;
                case 6:
                    letras = "Sábado";
                    break;
                case 0:
                    letras = "Domingo";
                    break;
                default:
                    letras = "";
                    break;
            }
            return letras.ToUpper();
        }

        public static string ConvertirToda(string fecha)
        {
            DateTime date = Convert.ToDateTime(fecha);
            int dia = date.Day;
            int mes = date.Month;
            int anio = date.Year;
            switch (mes)
            {
                case 1:
                    letras = "Enero";
                    break;
                case 2:
                    letras = "Febrero";
                    break;
                case 3:
                    letras = "Marzo";
                    break;
                case 4:
                    letras = "Abril";
                    break;
                case 5:
                    letras = "Mayo";
                    break;
                case 6:
                    letras = "Junio";
                    break;
                case 7:
                    letras = "Julio";
                    break;
                case 8:
                    letras = "Agosto";
                    break;
                case 9:
                    letras = "Septiembre";
                    break;
                case 10:
                    letras = "Octubre";
                    break;
                case 11:
                    letras = "Noviembre";
                    break;
                case 12:
                    letras = "Diciembre";
                    break;
                default:
                    letras = "";
                    break;
            }

            return (dia + " de " + letras + " de " + anio);
        }
        public static string ConvertirMes(string fecha)
        {
            DateTime date = Convert.ToDateTime(fecha);
            int mes = date.Month;
            switch (mes)

            {
                case 1:
                    letras = "Enero";
                    break;
                case 2:
                    letras = "Febrero";
                    break;
                case 3:
                    letras = "Marzo";
                    break;
                case 4:
                    letras = "Abril";
                    break;
                case 5:
                    letras = "Mayo";
                    break;
                case 6:
                    letras = "Junio";
                    break;
                case 7:
                    letras = "Julio";
                    break;
                case 8:
                    letras = "Agosto";
                    break;
                case 9:
                    letras = "Séptiembre";
                    break;
                case 10:
                    letras = "Octubre";
                    break;
                case 11:
                    letras = "Noviembre";
                    break;
                case 12:
                    letras = "Diciembre";
                    break;
                default:
                    letras = "";
                    break;
            }

            return (letras);
        }
    }
}
