using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Models
{
    [Table("ArticuloClave")]
    public class ArticuloClave
    {
        [ForeignKey("ArticuloId")]
        public int ArticuloId { get; set; }
        public virtual Articulo Articulo { get; set; }

        [ForeignKey("ClaveId")]
        public int ClaveId { get; set; }
        public virtual Clave Clave { get; set; }

    }
}
