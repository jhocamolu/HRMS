using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    /// @author Jesus Albeiro Gaviria R
    /// @email  desarrollador5@alcanosesp.com
    /// @Description  HU034_Administrar_Jornadas_Laborales
    /// Controlador Odata para busqueda personalizada
    /// 
    public class JornadaLaboralesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public JornadaLaboralesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/JornadaLaborales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaborales_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<JornadaLaboral>> Get()
        {
            return this.contexto.JornadaLaborales;
        }

        //GET: odata/JornadaLaborales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaborales_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<JornadaLaboral> Get([FromODataUri] int key)
        {
            IQueryable<JornadaLaboral> jornadaLaborales = this.contexto.JornadaLaborales.Where(p => p.Id == key);
            return SingleResult.Create(jornadaLaborales);
        }
    }
}
