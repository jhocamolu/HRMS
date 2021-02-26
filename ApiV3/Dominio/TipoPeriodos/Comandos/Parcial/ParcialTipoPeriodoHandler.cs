using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoPeriodos.Comando.Parcial
{
    public class ParcialTipoPeriodoHandler : IRequestHandler<ParcialTipoPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoPeriodo tipoPeriodo = this.contexto.TipoPeriodos.Find(request.Id);
                if (!String.IsNullOrEmpty(request.Nombre))
                {
                    tipoPeriodo.Nombre = Texto.TipoOracion(request.Nombre);
                }
                if (request.PagoPorDefecto != null)
                {
                    tipoPeriodo.PagoPorDefecto = (bool)request.PagoPorDefecto;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoPeriodo.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoPeriodo.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                this.contexto.TipoPeriodos.Update(tipoPeriodo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoPeriodo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
