using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Idiomas.Comandos.Parcial
{
    public class ParcialIdiomaHandler : IRequestHandler<ParcialIdiomaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialIdiomaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialIdiomaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var Idioma = contexto.Idiomas.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        Idioma.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        Idioma.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                if (request.Codigo != null)
                {
                    Idioma.Codigo = request.Codigo.ToLower();
                }

                if (request.Nombre != null)
                {
                    Idioma.Nombre = Texto.TipoOracion(request.Nombre);
                }
                contexto.Idiomas.Update(Idioma);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(Idioma);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);

            }
        }
    }
}
