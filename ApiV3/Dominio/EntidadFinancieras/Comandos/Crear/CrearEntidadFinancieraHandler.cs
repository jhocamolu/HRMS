using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EntidadesFinancieras.Comandos.Crear
{
    public class CrearEntidadFinancieraHandler : IRequestHandler<CrearEntidadFinancieraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearEntidadFinancieraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearEntidadFinancieraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                EntidadFinanciera entidadFinanciera = new EntidadFinanciera
                {
                    Codigo = request.Codigo,
                    Nit = request.Nit,
                    Dv = request.Dv,
                    Nombre = request.Nombre,
                    DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id,
                    Direccion = request.Direccion,
                    RepresentanteLegal = Texto.LetraCapital(request.RepresentanteLegal),
                    Telefono = request.Telefono,
                    EntidadPorDefecto = request.EntidadPorDefecto
                };
                contexto.EntidadFinancieras.Add(entidadFinanciera);

                await contexto.SaveChangesAsync();
                return CommandResult.Success(entidadFinanciera);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }
}
