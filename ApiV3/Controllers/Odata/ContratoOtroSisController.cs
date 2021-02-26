using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class ContratoOtroSisController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public ContratoOtroSisController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/OtroSis
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradoras_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ContratoOtroSi>> Get()
        {
            return this.contexto.ContratoOtroSis;
        }

        //GET: odata/OtroSis/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoOtroSis_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ContratoOtroSi> Get([FromODataUri] int key)
        {
            IQueryable<ContratoOtroSi> otroSis = this.contexto.ContratoOtroSis.Where(p => p.Id == key);
            return SingleResult.Create(otroSis);
        }
    }
}
