using Reportes.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;

namespace Reportes.Models.NoMapeado
{
    public class VistaFrontendReporte
    {
        [Key]
        public string Alias { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int SubcategoriaId { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }

        public string VistaGeneracion { get; set; }

        public Extension Extension { get; set; }

        public bool EsModal { get; set; }
    }
}
