using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU106
/// Controlador

namespace ApiV3.Controllers.Odata
{
    public class ContratoAdministradoraCambiosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ContratoAdministradoraCambiosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }



        // GET: api/Administradoras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradorasCambios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ContratoAdministradoraCambio>> Get()
        {
            return contexto.ContratoAdministradoraCambios;
        }

        // GET: api/Administradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradorasCambios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ContratoAdministradoraCambio> Get([FromODataUri] int key)
        {
            IQueryable<ContratoAdministradoraCambio> result = this.contexto.ContratoAdministradoraCambios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}