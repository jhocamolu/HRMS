using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria Rubio
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU094_Administrar_Requisiciones_Personal
/// Controlador Odata para busqueda los motivos vacante.

namespace ApiV3.Controllers.Odata
{
    public class MotivoVacantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public MotivoVacantesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        //GET: odata/MotivoVacantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.MotivoVacantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<MotivoVacante>> Get()
        {
            return this.contexto.MotivoVacantes;
        }

        //GET: odata/MotivoVacantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.MotivoVacantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<MotivoVacante> Get([FromODataUri] int key)
        {
            IQueryable<MotivoVacante> motivoVacantes = this.contexto.MotivoVacantes.Where(p => p.Id == key);
            return SingleResult.Create(motivoVacantes);
        }
    }
}
