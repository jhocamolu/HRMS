using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Crear
{
    public class CrearTipoContribuyenteHandler : IRequestHandler<CrearTipoContribuyenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoContribuyenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoContribuyenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoContribuyente tipoContribuyente = new TipoContribuyente
                {
                    Codigo = request.Codigo,
                    Nombre = Texto.TipoOracion(request.Nombre.ToUpper()),
                    Persona = request.Persona
                };

                this.contexto.TipoContribuyentes.Add(tipoContribuyente);
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
