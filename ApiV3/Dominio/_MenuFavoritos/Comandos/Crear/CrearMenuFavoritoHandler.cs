using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio._MenuFavoritos.Comandos.Crear
{
    public class CrearMenuFavoritoHandler : IRequestHandler<CrearMenuFavoritoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrearMenuFavoritoHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearMenuFavoritoRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var funcionario = InformacionToken.ObtenerInformacionFuncionario(_httpContextAccessor.HttpContext.Request.Headers["JwtToken"], contexto);

                _MenuFavorito menu = new _MenuFavorito
                {
                    FuncionarioId = funcionario.Id,
                    ItemMenu = request.ItemMenu
                };

                this.contexto._MenuFavoritos.Add(menu);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(menu);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
