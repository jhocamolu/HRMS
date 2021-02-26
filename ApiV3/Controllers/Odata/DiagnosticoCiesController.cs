using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU017_Administrar_CIE10
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class DiagnosticoCiesController : ControllerBase
    {

        private readonly NominaDbContext contexto;
        public DiagnosticoCiesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/DiagnosticoCies
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DiagnosticoCies_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<DiagnosticoCie>> Get()
        {
            return contexto.DiagnosticoCies;
        }


        //GET: odata/DiagnosticoCies/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DiagnosticoCies_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<DiagnosticoCie> Get([FromODataUri] int key)
        {
            IQueryable<DiagnosticoCie> diagnosticoCies = this.contexto.DiagnosticoCies.Where(p => p.Id == key);
            return SingleResult.Create(diagnosticoCies);
        }

    }
}
