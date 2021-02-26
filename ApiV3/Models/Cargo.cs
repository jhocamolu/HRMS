using ApiV3.Infraestructura.Enumerador;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Cargo")]
    public class Cargo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string ObjetivoCargo { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool CostoSicom { get; set; }

        public int NivelCargoId { get; set; }
        public virtual NivelCargo NivelCargo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public ClaseCargo Clase { get; set; }

        public virtual ICollection<Sustituto> ASustituir { get; set; }

        public virtual ICollection<Sustituto> Sustituto { get; set; }

    }
}