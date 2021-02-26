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
    public class NominaContabilidadesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NominaContabilidadesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/NominaContabilidades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaContabilidades_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<NominaContabilidad> Get()
        {
            return this.contexto.NominaContabilidades;
        }

        // GET: odata/NominaContabilidades/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaContabilidades_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<NominaContabilidad> Get([FromODataUri] int key)
        {
            IQueryable<NominaContabilidad> result = this.contexto.NominaContabilidades.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
