using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones parciales de la entidad FormaPago. 
/// para lo cual se evalua cada campo si trae un valor es decir si es diferente de NUll, 
/// si pasa la validacion se actualizan estos campos.
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Parcial
{
    public class ParcialFormaPagoHandler : IRequestHandler<ParcialFormaPagoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialFormaPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialFormaPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var formaPago = this.contexto.FormaPagos.Find(request.Id);
                //Validamos los campos a actualizar
                #region Nombre
                if (request.Nombre != null) formaPago.Nombre = Texto.TipoOracion(request.Nombre);
                #endregion

                #region Estado Registro
                if (request.Activo != null)
                {
                    formaPago.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) formaPago.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                #endregion
                this.contexto.FormaPagos.Update(formaPago);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(formaPago);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
