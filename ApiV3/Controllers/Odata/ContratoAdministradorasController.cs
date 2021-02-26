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
    public class ContratoAdministradorasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public ContratoAdministradorasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/ContratoAdministradoras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradoras_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<ContratoAdministradora> Get()
        {
            return this.contexto.ContratoAdministradoras;
        }


        //GET: odata/ContratoAdministradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradoras_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ContratoAdministradora> Get([FromODataUri] int key)
        {
            IQueryable<ContratoAdministradora> resultado = this.contexto.ContratoAdministradoras.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
