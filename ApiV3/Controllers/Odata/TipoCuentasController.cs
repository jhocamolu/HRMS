using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{
    public class TipoCuentasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TipoCuentasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TipoCuentas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCuentas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<TipoCuenta> Get()
        {
            return this.contexto.TipoCuentas;
        }


        //GET: odata/TipoCuentas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCuentas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoCuenta> Get([FromODataUri] int key)
        {
            IQueryable<TipoCuenta> resultado = this.contexto.TipoCuentas.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
