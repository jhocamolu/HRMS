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

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU111
namespace ApiV3.Controllers.Odata
{
    public class ActividadFuncionariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ActividadFuncionariosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/ActividadFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<ActividadFuncionario> Get()
        {
            return this.contexto.ActividadFuncionarios;
        }

        // GET: odata/ActividadFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<ActividadFuncionario> Get([FromODataUri] int key)
        {
            IQueryable<ActividadFuncionario> result = this.contexto.ActividadFuncionarios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}