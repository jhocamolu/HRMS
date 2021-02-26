using System;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Infraestructura.Utilidades
{
    public static class DigitoVerificacion
    {
        public static string CalcularDigitoVerificacion(string nit)
        {
            string Temp;
            int Contador;
            int Residuo;
            int Acumulador;
            int[] Vector = new int[15];

            Vector[0] = 3;
            Vector[1] = 7;
            Vector[2] = 13;
            Vector[3] = 17;
            Vector[4] = 19;
            Vector[5] = 23;
            Vector[6] = 29;
            Vector[7] = 37;
            Vector[8] = 41;
            Vector[9] = 43;
            Vector[10] = 47;
            Vector[11] = 53;
            Vector[12] = 59;
            Vector[13] = 67;
            Vector[14] = 71;

            Acumulador = 0;

            Residuo = 0;

            for (Contador = 0; Contador < nit.Length; Contador++)
            {
                Temp = nit[nit.Length - 1 - Contador].ToString();
                Acumulador = Acumulador + Convert.ToInt32(Temp) * Vector[Contador];
            }

            Residuo = Acumulador % 11;

            if (Residuo > 1)
                return Convert.ToString(11 - Residuo);

            return Residuo.ToString();
        }
    }
}
