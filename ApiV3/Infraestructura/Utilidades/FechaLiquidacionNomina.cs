using System;

namespace ApiV3.Infraestructura.Utilidades
{
    public class FechaLiquidacionNomina
    {
        /// <summary>
        /// Método obtiene la fecha de inicio de la liquidación de la nomina reemplazando el dia del periodo contable 
        /// con el día de inicio .
        /// </summary>
        /// <param name="fechaPeriodoContable"></param>
        /// <param name="diaInicio"></param>
        /// <returns></returns>
        public static dynamic CalculaFechaInicioLiquidacionNomina(DateTime fechaPeriodoContable, int diaInicio)
        {
            try
            {
                var Mes = fechaPeriodoContable.Month.ToString();
                var Anio = fechaPeriodoContable.Year.ToString();

                var fecha = diaInicio.ToString() + "/" + Mes + "/" + Anio;
                var FechaInicio = Convert.ToDateTime(fecha);

                return FechaInicio;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        /// <summary>
        /// Método obtiene fecha de Final del la liquidación de la nomina a partir de la fecha de inicio
        /// calculada en el método CalculaFechaInicioLiquidacionNomina, se le debe sumar los dias y compararlo.
        /// </summary>
        /// <param name="fechaPeriodoContable"></param>
        /// <param name="dias"></param>
        /// <param name="diaInicio"></param>
        /// <returns></returns>
        public static dynamic CalculaFechaFinalLiquidacionNomina(DateTime fechaPeriodoContable, int dias, int diaInicio)
        {
            try
            {
                DateTime fechaInicio = FechaLiquidacionNomina.CalculaFechaInicioLiquidacionNomina(fechaPeriodoContable, diaInicio);
                DateTime sumaDiasfechaFinal = fechaInicio.AddDays(dias);

                DateTime fechaFinal = DateTime.MinValue;
                if (sumaDiasfechaFinal.AddDays(-1) < fechaPeriodoContable.AddDays(-1))
                {
                    fechaFinal = sumaDiasfechaFinal.AddDays(-1);
                }
                else if (sumaDiasfechaFinal.AddDays(-1) >= fechaPeriodoContable.AddDays(-1))
                {
                    fechaFinal = fechaPeriodoContable;
                }
                return fechaFinal;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
