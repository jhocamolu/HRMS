using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU005_Administrar_Profesiones
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class ProfesionesController : ControllerBase
    {


        private readonly NominaDbContext contexto;
        public ProfesionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        // GET: odata/Profesiones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Profesiones_Listar })]
        [HttpGet]
        [EnableQuery]
        public IQueryable<Profesion> Get()
        {
            return this.contexto.Profesiones;
        }


        // GET: odata/Profesiones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Profesiones_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<Profesion> Get([FromODataUri] int key)
        {
            IQueryable<Profesion> profesiones = this.contexto.Profesiones.Where(x => x.Id == key);
            return SingleResult.Create(profesiones);
        }
    }
}
