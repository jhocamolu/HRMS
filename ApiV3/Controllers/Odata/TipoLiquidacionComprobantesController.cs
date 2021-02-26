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
/// @Description  HU050
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    
    public class TipoLiquidacionComprobantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoLiquidacionComprobantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoLiquidacionComprobantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionComprobantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<TipoLiquidacionComprobante>> Get()
        {
            return contexto.TipoLiquidacionComprobantes;
        }
        // GET: odata/TipoLiquidacionComprobantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionComprobantes_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<TipoLiquidacionComprobante> Get([FromODataUri] int key)
        {
            IQueryable<TipoLiquidacionComprobante> result = this.contexto.TipoLiquidacionComprobantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
