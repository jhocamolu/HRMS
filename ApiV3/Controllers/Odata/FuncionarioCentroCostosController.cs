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

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU111
namespace ApiV3.Controllers.Odata
{

    public class FuncionarioCentroCostosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public FuncionarioCentroCostosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/FuncionarioCentroCostos
        
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioCentroCostos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<FuncionarioCentroCosto> Get()
        {
            return this.contexto.FuncionarioCentroCostos;
        }

        // GET: odata/FuncionarioCentroCostos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioCentroCostos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<FuncionarioCentroCosto> Get([FromODataUri] int key)
        {
            IQueryable<FuncionarioCentroCosto> result = this.contexto.FuncionarioCentroCostos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
