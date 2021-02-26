using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Parentescos.Comandos.Crear
{
    public class CrearParentescoHandler : IRequestHandler<CrearParentescoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearParentescoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearParentescoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Parentesco parantesco = new Parentesco
                {
                    Nombre = Texto.TipoOracion(request.Nombre.ToUpper()),
                    Tipo = request.Tipo,
                    Grado = request.Grado,
                    NumeroPersonasPermitidas = request.NumeroPersonasPermitidas
                };

                contexto.Parentescos.Add(parantesco);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(parantesco);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
