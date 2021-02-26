using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU030_Niveles_Educativos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class NivelEducativosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public NivelEducativosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        //GET: odata/NivelEducativos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelEducativos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<NivelEducativo>> Get()
        {
            return this.contexto.NivelEducativos;
        }


        //GET: odata/NivelEducativos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelEducativos_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<NivelEducativo> Get([FromODataUri] int key)
        {
            IQueryable<NivelEducativo> nivelEducativos = this.contexto.NivelEducativos.Where(p => p.Id == key);
            return SingleResult.Create(nivelEducativos);
        }
    }
}
