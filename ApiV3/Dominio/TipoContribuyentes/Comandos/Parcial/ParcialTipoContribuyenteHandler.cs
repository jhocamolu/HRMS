using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Parcial
{
    public class ParcialTipoContribuyenteHandler : IRequestHandler<ParcialTipoContribuyenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoContribuyenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoContribuyenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoContribuyente tipoContribuyente = await this.contexto.TipoContribuyentes.FindAsync(request.Id);
                if (request.Codigo != null)
                {
                    tipoContribuyente.Codigo = request.Codigo;
                }

                if (request.Nombre != null)
                {
                    tipoContribuyente.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                }

                if (request.Persona != null)
                {
                    tipoContribuyente.Persona = (TipoPersonaContribuyente)request.Persona;
                }

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoContribuyente.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoContribuyente.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }


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
