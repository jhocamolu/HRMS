using ApiV3.Infraestructura.DbContexto;
using System;
using System.Linq;

namespace ApiV3.Infraestructura.Utilidades
{
    public static class DiaHabil
    {
        // Método de apoyo que permite saber si una fecha, es un día hábil.
        public static bool ValidaDiaHabil(DateTime fecha, NominaDbContext contexto)
        {
            bool respuesta = true;
            //Consulta día festivo
            var validaDiasFestivos = contexto.Calendarios.FirstOrDefault(x => x.Fecha == fecha);
            if ((fecha.DayOfWeek == DayOfWeek.Saturday) || (fecha.DayOfWeek == DayOfWeek.Sunday) || (validaDiasFestivos != null))
            {
                respuesta = false;
            }
            return respuesta;
        }
        //Métod de apoyo para el módulo de vacaciones que permite calcular la fecha final 
        public static DateTime SumaDiasHabilesFecha(DateTime fecha, int dias, NominaDbContext contexto)
        {
            bool sumaDia = true;

            DateTime fechaFinal = fecha;
            int dia = dias-1;
            int i = dia;
            // Suma días habiles para obtener fecha final
            while (sumaDia)
            {
                if (i >= 1)
                {
                    fechaFinal = fechaFinal.AddDays(1);
                    if (ValidaDiaHabil(fechaFinal, contexto) == true)
                    {
                        if (i >= 1)
                        {
                            i--;
                        }
                        if (i == 0)
                        {
                            sumaDia = false;
                        }
                    }
                }
                else {
                    sumaDia = false;    
                }
            }

            fechaFinal = ValidaDiaSiguienteHabil(fechaFinal, contexto);
            return fechaFinal;
        }
        //Método de apoyo al módulo de vacaciones, valida que la fecha siguiente del 
        public static DateTime ValidaDiaSiguienteHabil(DateTime fecha, NominaDbContext contexto)
        {
            bool validaDiaHabil = true;
            //Valida que la fecha final obtenida más un día sea un día hábil.
            while (validaDiaHabil)
            {
                var validaDiaSiguienteHabil = fecha.AddDays(1);
                // Si no es día hábil, se debe adicionar un día, hasta que
                // encontrar el día siguiente hábil
                if (ValidaDiaHabil(validaDiaSiguienteHabil, contexto) == false)
                {
                    fecha = fecha.AddDays(1);
                }
                else
                {
                    validaDiaHabil = false;
                }
            }
            return fecha;
        }

        //Método de apoyo obtiene la cantida de días habiles entre dos fechas.
        public static int CantidadDiasHabilesEntreFechas(DateTime fechaInicial, DateTime fechaFinal, NominaDbContext contexto)
        {
            int dias = 0;
            bool validaDiaHabil = true;

            while (validaDiaHabil)
            {
                fechaInicial = fechaInicial.AddDays(1);

                if (ValidaDiaHabil(fechaInicial, contexto) == true)
                {
                    dias++;
                }

                if (fechaInicial.Date == fechaFinal.Date)
                {
                    validaDiaHabil = false;
                }

            }
            return dias;
        }
    }
}
