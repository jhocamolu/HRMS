using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{
    public class _MenuFavoritosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public _MenuFavoritosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/_MenuFavoritos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos._MenuFavoritos_Listar })]
        [EnableQuery(MaxExpansionDepth = 50)]
        [HttpGet]
        public IQueryable<_MenuFavorito> Get_MenuFavoritos()
        {
            Funcionario funcionario = InformacionToken.ObtenerInformacionFuncionario(Request.Headers["JwtToken"], contexto);
            return contexto._MenuFavoritos.Where(m => m.FuncionarioId == funcionario.Id);
        }

        // GET: api/_MenuFavoritos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos._MenuFavoritos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<_MenuFavorito> Get([FromODataUri] int key)
        {
            IQueryable<_MenuFavorito> result = this.contexto._MenuFavoritos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
