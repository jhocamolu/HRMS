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

namespace ApiV3.Controllers.Odata
{
    public class NominaCentroCostosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NominaCentroCostosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/NominaCentroCostos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaCentroCostos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<NominaCentroCosto> Get()
        {
            return this.contexto.NominaCentroCostos;
        }

        // GET: odata/NominaCentroCostos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaCentroCostos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<NominaCentroCosto> Get([FromODataUri] int key)
        {
            IQueryable<NominaCentroCosto> result = this.contexto.NominaCentroCostos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
