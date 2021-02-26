using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models.NoMapeado
{
    public class _logError
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public int? Numero { get; set; }
        public int? Severidad { get; set; }
        public int? Estado { get; set; }
        public string Procedimiento { get; set; }
        public int? Linea { get; set; }

        [Column(TypeName = "text")]
        public string Parametros { get; set; }

        [Column(TypeName = "text")]
        public string Mensaje { get; set; }
    }
}
