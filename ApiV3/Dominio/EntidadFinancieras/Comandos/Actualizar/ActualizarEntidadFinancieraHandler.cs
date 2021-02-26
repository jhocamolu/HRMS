using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EntidadesFinancieras.Comandos.Actualizar
{
    public class ActualizarEntidadFinancieraHandler : IRequestHandler<ActualizarEntidadFinancieraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarEntidadFinancieraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarEntidadFinancieraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                EntidadFinanciera entidadFinanciera = this.contexto.EntidadFinancieras.Find(request.Id);

                entidadFinanciera.Codigo = request.Codigo;
                entidadFinanciera.Nit = request.Nit;
                entidadFinanciera.Dv = request.Dv;
                entidadFinanciera.Nombre = request.Nombre;
                entidadFinanciera.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                entidadFinanciera.Direccion = request.Direccion;
                entidadFinanciera.RepresentanteLegal = Texto.LetraCapital(request.RepresentanteLegal);
                entidadFinanciera.Telefono = request.Telefono;
                entidadFinanciera.EntidadPorDefecto = request.EntidadPorDefecto;

                contexto.EntidadFinancieras.Update(entidadFinanciera);
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
