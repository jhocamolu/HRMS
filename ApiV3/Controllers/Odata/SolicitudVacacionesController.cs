using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU067_LibroVacaciones
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class SolicitudVacacionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SolicitudVacacionesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/SolicitudVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<SolicitudVacacion> Get()
        {
            // Funcionario funcionario = InformacionToken.ObtenerInformacionFuncionario(Request.Headers["JwtToken"], contexto);
            // var sol = Interceptacion.FiltroVacaciones(funcionario , contexto);
            // return this.contexto.SolicitudVacaciones.Where(w => sol.Contains(w.Id));
            return this.contexto.SolicitudVacaciones;
        }

        // GET: odata/SolicitudVacaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<SolicitudVacacion> Get([FromODataUri] int key)
        {
            IQueryable<SolicitudVacacion> resultado = this.contexto.SolicitudVacaciones.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
