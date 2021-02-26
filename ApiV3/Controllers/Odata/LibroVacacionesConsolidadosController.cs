using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiV3.Controllers.Odata
{
    public class LibroVacacionesConsolidadosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public LibroVacacionesConsolidadosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/FuncionarioDatoActuales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacacionesConsolidados_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<LibroVacacionesConsolidado> Get()
        {
            return this.contexto.LibroVacacionesConsolidados;
        }

        // GET: odata/FuncionarioDatoActuales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacacionesConsolidados_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<LibroVacacionesConsolidado> Get([FromODataUri] int key)
        {
            IQueryable<LibroVacacionesConsolidado> funcionarioDatos = this.contexto.LibroVacacionesConsolidados.Where(p => p.Id == key);
            return SingleResult.Create(funcionarioDatos);
        }
    }
}