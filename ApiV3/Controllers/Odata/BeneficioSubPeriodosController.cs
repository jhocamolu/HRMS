using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// Controlador
/// Sprint8

namespace ApiV3.Controllers.Odata
{
    public class BeneficioSubPeriodosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public BeneficioSubPeriodosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Beneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BeneficioSubPeriodos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<BeneficioSubperiodo> Get()
        {
            return this.contexto.BeneficioSubperiodos;
        }


        //GET: odata/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BeneficioSubPeriodos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<BeneficioSubperiodo> Get([FromODataUri] int key)
        {
            IQueryable<BeneficioSubperiodo> beneficios = this.contexto.BeneficioSubperiodos.Where(p => p.Id == key);
            return SingleResult.Create(beneficios);
        }
    }
}