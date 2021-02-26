using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria Rubio
/// @email  desarrollador5@alcanosesp.com
/// @Description HU105_Documentos_Soporte_Hoja_de_Vida

namespace ApiV3.Controllers.Odata
{
    public class HojaDeVidaDocumentosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public HojaDeVidaDocumentosController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: api/HojaDeVidaDocumentos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaDocumentos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<HojaDeVidaDocumento>> Get()
        {
            return this.contexto.HojaDeVidaDocumentos;
        }

        // GET: api/HojaDeVidaDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaDocumentos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<HojaDeVidaDocumento> Get([FromODataUri] int key)
        {
            IQueryable<HojaDeVidaDocumento> result = this.contexto.HojaDeVidaDocumentos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}