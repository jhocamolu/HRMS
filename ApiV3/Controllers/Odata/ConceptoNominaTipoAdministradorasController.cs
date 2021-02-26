using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using Microsoft.AspNet.OData;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU111
/// Controlador Odata para busqueda personalizada
/// 

namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoNominaTipoAdministradorasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ConceptoNominaTipoAdministradorasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        // GET: odata/ConceptoNominaTipoAdministradora
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaTipoAdministradoras_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<ConceptoNominaTipoAdministradora>> Get()
        {
            return this.contexto.ConceptoNominaTipoAdministradoras;
        }
           
        // GET: odata/ConceptoNominaTipoAdministradora/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaTipoAdministradoras_Obtener })]
        [HttpGet]
        [EnableQuery]

        public SingleResult<ConceptoNominaTipoAdministradora> Get([FromODataUri] int key)
        {
            IQueryable<ConceptoNominaTipoAdministradora> result = this.contexto.ConceptoNominaTipoAdministradoras.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }


    }
}
