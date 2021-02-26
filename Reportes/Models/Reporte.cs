using Reportes.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reportes.Models
{
    [Table("Reporte")]
    public class Reporte : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Alias { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Descripcion { get; set; }

        [Required]
        public int SubcategoriaId { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Link { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string VistaGeneracion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Path { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public Formato Formato { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public Extension Extension { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Alto { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Ancho { get; set; }

        [Column(TypeName = "bit")]
        public bool EsModal { get; set; }
    }
}
