using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU046_Concepto_Nomina
/// Controlador Odata para TipoCotizanteTipoPlanillasController requerido pila

namespace ApiV3.Controllers.Odata
{
    public class TipoCotizanteTipoPlanillasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TipoCotizanteTipoPlanillasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TipoCotizanteTipoPlanilla
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCotizanteTipoPlanillas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<TipoCotizanteTipoPlanilla> Get()
        {
            return this.contexto.TipoCotizanteTipoPlanillas;
        }


        //GET: odata/TipoCotizanteTipoPlanilla/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCotizanteTipoPlanillas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoCotizanteTipoPlanilla> Get([FromODataUri] int key)
        {
            IQueryable<TipoCotizanteTipoPlanilla> resultado = this.contexto.TipoCotizanteTipoPlanillas.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
