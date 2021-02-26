using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("VW_LibroVacacionesConsolidado")]
    public class LibroVacacionesConsolidado
    {
        [Key]
        public int Id { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public int PeriodosPendientes { get; set; }
        public int DiasPendientes { get; set; }
        
    }
}
