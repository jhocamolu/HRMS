using System;

namespace ApiV3.Models.NoMapeado
{
    public class VistaNominaValor
    {
        public int Id { get; set; }

        public string TipoLiquidacion { get; set; }

        public string Subperiodo { get; set; }

        public DateTime? FechaInicial { get; set; }

        public DateTime? FechaFinal { get; set; }

        public double ValorTotal { get; set; }
    }
}
