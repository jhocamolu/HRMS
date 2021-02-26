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
using Microsoft.AspNet.OData;
using ApiV3.Infraestructura.Utilidades;

namespace ApiV3.Controllers.Odata
{
    /// @author Laura Katherine Estrada Arango
    /// @email  desarrollador3@alcanosesp.com
    /// @Description  HU111
    public class ActividadFuncionarioCentroCostosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ActividadFuncionarioCentroCostosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/ActividadFuncionarioCentroCostos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarioCentroCostos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<ActividadFuncionarioCentroCosto> Get()
        {
            return this.contexto.ActividadFuncionarioCentroCostos;
        }

        // GET: api/ActividadFuncionarioCentroCostos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarioCentroCostos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<ActividadFuncionarioCentroCosto> Get([FromODataUri] int key)
        {
            IQueryable<ActividadFuncionarioCentroCosto> result = this.contexto.ActividadFuncionarioCentroCostos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
