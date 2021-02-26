using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Parentescos.Comandos.Actualizar
{
    public class ActualizarParentescoHandler : IRequestHandler<ActualizarParentescoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarParentescoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarParentescoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Parentesco parantesco = this.contexto.Parentescos.Find(request.Id);

                parantesco.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                parantesco.Tipo = request.Tipo;
                parantesco.Grado = request.Grado;
                parantesco.NumeroPersonasPermitidas = request.NumeroPersonasPermitidas;

                this.contexto.Parentescos.Update(parantesco);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(parantesco);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
