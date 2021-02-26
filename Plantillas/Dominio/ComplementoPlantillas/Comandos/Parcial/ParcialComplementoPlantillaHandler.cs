using MediatR;
using Plantillas.Dominio.Enumerador;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.ComplementoPlantillas.Comandos.Parcial
{
    public class ParcialComplementoPlantillaHandler : IRequestHandler<ParcialComplementoPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public ParcialComplementoPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialComplementoPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ComplementoPlantilla complementoPlantilla = contexto.ComplementoPlantillas.FirstOrDefault(z => z.Id == request.Id);

                if (request.Activo != null)
                {
                    if ((bool)request.Activo)
                    {
                        complementoPlantilla.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        complementoPlantilla.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (null != request.GrupoDocumentoId)
                {
                    complementoPlantilla.GrupoDocumentoId = (int)request.GrupoDocumentoId;
                }

                if (request.Nombre != null)
                {
                    complementoPlantilla.Nombre = Texto.TipoOracion(request.Nombre);
                }

                if (request.Tipo != null)
                {
                    complementoPlantilla.Tipo = (TipoComplemento)request.Tipo;
                }

                if (request.CuerpoDocumento != null)
                {
                    complementoPlantilla.CuerpoDocumento = request.CuerpoDocumento;
                }
                if (request.Alto != null)
                {
                    complementoPlantilla.Alto = request.Alto;
                }

                contexto.ComplementoPlantillas.Update(complementoPlantilla);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(complementoPlantilla);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
