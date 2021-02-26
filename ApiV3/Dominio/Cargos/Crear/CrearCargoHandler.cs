using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Cargos.Crear
{
    public class CrearCargoHandler : IRequestHandler<CrearCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Cargo cargo = new Cargo();

                cargo.Codigo = request.Codigo.ToUpper();
                cargo.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                cargo.NivelCargoId = request.NivelCargoId;
                cargo.ObjetivoCargo = request.ObjetivoCargo;
                cargo.CostoSicom = (bool)request.CostoSicom;
                cargo.Clase = (ClaseCargo)request.Clase;

                contexto.Cargos.Add(cargo);
                await contexto.SaveChangesAsync();

                //Se agrega el registro al grupo por defecto.
                if (request.GrupoId != null)
                {
                    CargoGrupo cargoGrupo = new CargoGrupo();
                    cargoGrupo.CargoId = cargo.Id;
                    cargoGrupo.GrupoId = (int)request.GrupoId;
                    cargoGrupo.Defecto = true;
                    contexto.CargoGrupos.Add(cargoGrupo);
                    await contexto.SaveChangesAsync();
                }

                return CommandResult.Success(cargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
