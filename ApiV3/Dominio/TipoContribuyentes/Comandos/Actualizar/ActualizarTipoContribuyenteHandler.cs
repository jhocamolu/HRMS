using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Actualizar
{
    public class ActualizarTipoContribuyenteHandler : IRequestHandler<ActualizarTipoContribuyenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoContribuyenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoContribuyenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoContribuyente tipoContribuyente = await this.contexto.TipoContribuyentes.FindAsync(request.Id);

                tipoContribuyente.Codigo = request.Codigo;
                tipoContribuyente.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());

                this.contexto.TipoContribuyentes.Update(tipoContribuyente);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoContribuyente);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
