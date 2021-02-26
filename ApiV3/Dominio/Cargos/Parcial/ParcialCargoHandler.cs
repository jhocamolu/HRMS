using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Cargos.Parcial
{
    public class ParcialCargoHandler : IRequestHandler<ParcialCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Cargo cargo = this.contexto.Cargos.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        cargo.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        cargo.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Codigo != null)
                {
                    cargo.Codigo = request.Codigo.ToUpper();
                }
                if (request.Nombre != null)
                {
                    cargo.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                }

                if (request.NivelCargoId != null)
                {
                    cargo.NivelCargoId = (int)request.NivelCargoId;
                }
                if (request.ObjetivoCargo != null)
                {
                    cargo.ObjetivoCargo = request.ObjetivoCargo;
                }
                if (request.CostoSicom != null)
                {
                    cargo.CostoSicom = (bool)request.CostoSicom;
                }
                if (request.Clase != null)
                {
                    cargo.Clase = (ClaseCargo)request.Clase;
                }

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
