using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CentroOperativo")]
    public class CentroOperativo : AuditoriaRegistro
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "char(3)")]
        [DefaultValue("000")]
        public string Codigo
        {
            get { return defecto; }
            set
            {
                this.defecto = "000";
            }
        }

        private string defecto;

        public virtual ICollection<Sustituto> CentroASustituir { get; set; }

        public virtual ICollection<Sustituto> CentroSustituto { get; set; }
    }
}
