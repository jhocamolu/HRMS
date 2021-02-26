using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU112_Administrar_ContratoCentroTrabajo
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class ContratoCentroTrabajosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ContratoCentroTrabajosController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: odata/ContratoCentroTrabajos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoCentroTrabajos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<ContratoCentroTrabajo> Get()
        {
            return this.contexto.ContratoCentroTrabajos;
        }

        // GET: odata/ContratoCentroTrabajos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoCentroTrabajos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<ContratoCentroTrabajo> Get([FromODataUri] int key)
        {
            IQueryable<ContratoCentroTrabajo> result = this.contexto.ContratoCentroTrabajos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
