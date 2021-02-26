using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU055_NovedadesNomina
/// Controlador Odata para busqueda personalizada
namespace ApiV3.Controllers.Odata
{
    public class NominaDetallesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public NominaDetallesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        // GET: odata/NominaDetalles
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaDetalles_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<NominaDetalle> Get()
        {
            IQueryable<NominaDetalle> result = this.contexto.NominaDetalles;
            foreach (var item in result)
            {
                if (item.NominaFuenteNovedadId != null)
                {
                    item.ValorEditable = this.contexto.CategoriaNovedades.Where(p => p.ConceptoNominaId == item.ConceptoNominaId).Select(i => i.ValorEditable);
                }
            }
            return result;
        }

        //// GET: odata/NominaDetalles
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaDetalles_ObtenerNominaFuncionario })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<NominaDetalle> GetNominaFuncionario([FromODataUri] int id)
        {
            IQueryable<NominaDetalle> result = this.contexto.NominaDetalles.Where(p => p.NominaFuncionarioId == id);
            foreach (var item in result)
            {
                if (item.NominaFuenteNovedadId != null)
                {
                    item.ValorEditable = this.contexto.CategoriaNovedades.Where(p => p.ConceptoNominaId == item.ConceptoNominaId).Select(i => i.ValorEditable);
                }
            }
            return result;
        }

        // GET: odata/NominaDetalles/1
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaDetalles_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<NominaDetalle> Get([FromODataUri] int key)
        {
            IQueryable<NominaDetalle> result = this.contexto.NominaDetalles.Where(p => p.Id == key);
            foreach (var item in result)
            {
                if (item.NominaFuenteNovedadId != null)
                {
                    item.ValorEditable = this.contexto.CategoriaNovedades.Where(p => p.ConceptoNominaId == item.ConceptoNominaId).Select(i => i.ValorEditable);
                }
            }
            return SingleResult.Create(result);
        }
    }
}
