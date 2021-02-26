using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Cargos.Actualizar
{
    public class ActualizarCargoHandler : IRequestHandler<ActualizarCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Cargo cargo = this.contexto.Cargos.Find(request.Id);

                cargo.Codigo = request.Codigo.ToUpper();
                cargo.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                cargo.NivelCargoId = request.NivelCargoId;
                cargo.ObjetivoCargo = request.ObjetivoCargo;
                cargo.CostoSicom = (bool)request.CostoSicom;
                cargo.Clase = (ClaseCargo)request.Clase;

                contexto.Cargos.Update(cargo);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(cargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
