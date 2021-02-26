using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Parcial
{
    public class ParcialAplicacionExternaHandler : IRequestHandler<ParcialAplicacionExternaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialAplicacionExternaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialAplicacionExternaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExterna aplicacionExterna = this.contexto.AplicacionExternas.Find(request.Id);
                if (request.Nombre != null)
                {
                    aplicacionExterna.Nombre = Texto.TipoOracion(request.Nombre);
                }
                if (request.Descripcion != null)
                {
                    aplicacionExterna.Descripcion = request.Descripcion;
                }
                if (request.Aprueba != null)
                {
                    aplicacionExterna.Aprueba = (TipoAplicacionExterna)request.Aprueba;
                }
                if (request.Autoriza != null)
                {
                    aplicacionExterna.Autoriza = (TipoAplicacionExterna)request.Autoriza;
                }
                if (request.Revisa != null)
                {
                    aplicacionExterna.Revisa = (TipoAplicacionExterna)request.Revisa;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        aplicacionExterna.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    if (request.Activo == false)
                    {
                        aplicacionExterna.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.AplicacionExternas.Update(aplicacionExterna);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(aplicacionExterna);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}