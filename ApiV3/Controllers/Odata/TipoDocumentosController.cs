using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU011_Administrar_Tipos_Documento
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class TipoDocumentosController : ControllerBase
    {


        private readonly NominaDbContext contexto;
        public TipoDocumentosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        // GET: odata/TipoDocumentos
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoDocumento>> Get()
        {
            return this.contexto.TipoDocumentos;
        }


        // GET: api/TipoDocumentos/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TipoDocumento> Get([FromODataUri] int key)
        {
            IQueryable<TipoDocumento> tipoDocumento = this.contexto.TipoDocumentos.Where(p => p.Id == key);
            return SingleResult.Create(tipoDocumento);

        }
    }
}
