using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU033_Documentos_Funcionarios
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class DocumentoFuncionariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public DocumentoFuncionariosController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: api/DocumentoFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<DocumentoFuncionario>> Get()
        {
            return this.contexto.DocumentoFuncionarios;
        }

        // GET: api/DocumentoFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<DocumentoFuncionario> Get([FromODataUri] int key)
        {
            IQueryable<DocumentoFuncionario> result = this.contexto.DocumentoFuncionarios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
