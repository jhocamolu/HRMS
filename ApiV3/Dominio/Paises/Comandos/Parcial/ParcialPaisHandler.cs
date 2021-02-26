using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Paises.Comandos.Parcial
{
    public class ParcialPaisHandler : IRequestHandler<ParcialPaisRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialPaisHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialPaisRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Pais pais = this.contexto.Paises.Find(request.Id);

                if (request.Codigo != null)
                {
                    pais.Codigo = request.Codigo;
                }

                if (request.Nombre != null)
                {
                    pais.Nombre = request.Nombre;
                }

                if (request.Nacionalidad != null)
                {
                    pais.Nacionalidad = request.Nacionalidad;
                }

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        pais.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        pais.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                this.contexto.Paises.Update(pais);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(pais);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
