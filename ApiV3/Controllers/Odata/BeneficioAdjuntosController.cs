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
    public class BeneficioAdjuntosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public BeneficioAdjuntosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Beneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BeneficioAdjuntos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<BeneficioAdjunto> Get()
        {
            return this.contexto.BeneficioAdjuntos;
        }


        //GET: odata/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BeneficioAdjuntos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<BeneficioAdjunto> Get([FromODataUri] int key)
        {
            IQueryable<BeneficioAdjunto> beneficioAdjunto = this.contexto.BeneficioAdjuntos.Where(p => p.Id == key);
            return SingleResult.Create(beneficioAdjunto);
        }
    }
}