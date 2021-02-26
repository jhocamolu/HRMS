using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones parciales de la entidad Profesion. 
/// para lo cual se evalua cada campo si trae un valor es decir si es diferente de NUll, 
/// si pasa la validacion se envia a actualizar.
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Parcial
{
    public class ParcialProfesionHandler : IRequestHandler<ParcialProfesionRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialProfesionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(ParcialProfesionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var profesiones = this.contexto.Profesiones.Find(request.Id);

                #region Nombre
                if (request.Nombre != null) profesiones.Nombre = Texto.TipoOracion(request.Nombre);
                #endregion

                #region Estado Registro
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        profesiones.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        profesiones.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                #endregion

                this.contexto.Profesiones.Update(profesiones);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(profesiones);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}