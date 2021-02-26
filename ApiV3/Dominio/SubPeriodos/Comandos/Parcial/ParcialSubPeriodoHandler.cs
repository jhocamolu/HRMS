using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SubPeriodos.Comandos.Parcial
{
    public class ParcialSubPeriodoHandler : IRequestHandler<ParcialSubPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialSubPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialSubPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SubPeriodo subPeriodo = this.contexto.SubPeriodos.Find(request.Id);
                if (request.TipoPeriodoId != null)
                {
                    subPeriodo.TipoPeriodoId = (int)request.TipoPeriodoId;
                }
                if (!String.IsNullOrEmpty(request.Nombre))
                {
                    subPeriodo.Nombre = Texto.TipoOracion(request.Nombre);
                }
                if (request.Dias != null)
                {
                    subPeriodo.Dias = (int)request.Dias;
                }
                if (request.DiaInicial != null)
                {
                    subPeriodo.DiaInicial = (int)request.DiaInicial;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        subPeriodo.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        subPeriodo.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }


                this.contexto.SubPeriodos.Update(subPeriodo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(subPeriodo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
