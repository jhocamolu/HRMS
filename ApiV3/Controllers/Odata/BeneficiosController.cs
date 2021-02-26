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
    public class BeneficiosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public BeneficiosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Beneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Beneficio> Get()
        {
            return this.contexto.Beneficios;
        }


        //GET: odata/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Beneficio> Get([FromODataUri] int key)
        {
            IQueryable<Beneficio> beneficios = this.contexto.Beneficios.Where(p => p.Id == key);
            return SingleResult.Create(beneficios);
        }
    }
}