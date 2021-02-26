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
    public class ActividadFuncionarioDatoActualesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ActividadFuncionarioDatoActualesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/FuncionarioDatoActuales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarioDatoActuales_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<ActividadFuncionarioDatoActual> Get()
        {
            return this.contexto.ActividadFuncionarioDatoActuales;
        }

        // GET: odata/FuncionarioDatoActuales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarioDatoActuales_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ActividadFuncionarioDatoActual> Get([FromODataUri] int key)
        {
            IQueryable<ActividadFuncionarioDatoActual> funcionarioDatos = this.contexto.ActividadFuncionarioDatoActuales.Where(p => p.Id == key);
            return SingleResult.Create(funcionarioDatos);
        }
    }
}