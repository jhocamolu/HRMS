using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ayuda.Models
{
    [Table("Articulo")]
    public class Articulo : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        [Required]
        public int Orden { get; set; }

        [StringLength(255)]
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [IgnoreDataMember]
        public ICollection<ArticuloClave> ArticuloClaves { get; set; }

        public virtual ICollection<string> Palabras
        {
            get
            {
                var list = new List<string>();
                if (ArticuloClaves != null)
                {
                    foreach (var ac in ArticuloClaves)
                    {
                        list.Add(ac.Clave.Palabra);
                    }
                }
                return list;
            }

        }

    }
}
