using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU020_Administrar_formas_pago
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class FormaPagosController : ControllerBase
    {

        private readonly NominaDbContext contexto;
        public FormaPagosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/FormasPagos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FormaPagos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<FormaPago>> Get()
        {
            return this.contexto.FormaPagos;
        }

        //GET: odata/FormasPagos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FormaPagos_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<FormaPago> Get([FromODataUri] int key)
        {
            IQueryable<FormaPago> formaPagos = this.contexto.FormaPagos.Where(p => p.Id == key);
            return SingleResult.Create(formaPagos);
        }
    }
}
