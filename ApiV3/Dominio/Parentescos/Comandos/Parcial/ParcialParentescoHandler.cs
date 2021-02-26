using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Parentescos.Comandos.Estado
{
    public class ParcialParentescoHandler : IRequestHandler<ParcialParentescoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialParentescoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialParentescoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parentesco = contexto.Parentescos.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        parentesco.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        parentesco.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                if (request.Nombre != null)
                {
                    parentesco.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                }
                if (request.Tipo != null)
                {
                    parentesco.Tipo = (TipoParentescos)request.Tipo;
                }
                if (request.Grado != null)
                {
                    parentesco.Grado = (GradoParentescos)request.Grado;
                }
                if (request.NumeroPersonasPermitidas != null)
                {
                    parentesco.NumeroPersonasPermitidas = (int)request.NumeroPersonasPermitidas;
                }
                contexto.Parentescos.Update(parentesco);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(parentesco);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
