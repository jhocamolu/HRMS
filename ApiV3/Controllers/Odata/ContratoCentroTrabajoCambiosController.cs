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
/// @email  desarrollador2@alcanosesp.com
/// @Description  HU112 Adm Cambio Contrato Centro Trabajo
/// Controlador
namespace ApiV3.Controllers.Odata
{
   
    public class ContratoCentroTrabajoCambiosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ContratoCentroTrabajoCambiosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }



        // GET: odata/ContratoCentroTrabajoCambios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoCentroTrabajoCambios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ContratoCentroTrabajoCambio>> Get()
        {
            return contexto.ContratoCentroTrabajoCambios;
        }

        // GET: odata/ContratoCentroTrabajoCambios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoCentroTrabajoCambios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ContratoCentroTrabajoCambio> Get([FromODataUri] int key)
        {
            IQueryable<ContratoCentroTrabajoCambio> result = this.contexto.ContratoCentroTrabajoCambios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}