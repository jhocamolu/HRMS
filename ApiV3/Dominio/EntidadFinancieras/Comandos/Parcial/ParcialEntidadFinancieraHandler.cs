using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EntidadFinancieras.Comandos.Parcial
{
    public class ParcialEntidadFinancieraHandler : IRequestHandler<ParcialEntidadFinancieraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialEntidadFinancieraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialEntidadFinancieraRequest request, CancellationToken cancellationToken)
        {
            EntidadFinanciera entidadFinanciera = this.contexto.EntidadFinancieras.Find(request.Id);
            try
            {

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        entidadFinanciera.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        entidadFinanciera.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Codigo != null)
                {
                    entidadFinanciera.Codigo = request.Codigo;
                }
                if (request.Nit != null)
                {
                    entidadFinanciera.Nit = request.Nit;
                }
                if (request.Dv != null)
                {
                    entidadFinanciera.Dv = request.Dv;
                }
                if (request.Nombre != null)
                {
                    entidadFinanciera.Nombre = request.Nombre;
                }
                if (request.DivisionPoliticaNivel2Id != null)
                {
                    entidadFinanciera.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                }
                if (request.Direccion != null)
                {
                    entidadFinanciera.Direccion = request.Direccion;
                }
                if (request.RepresentanteLegal != null)
                {
                    entidadFinanciera.RepresentanteLegal = Texto.LetraCapital(request.RepresentanteLegal);
                }
                if (request.Telefono != null)
                {
                    entidadFinanciera.Telefono = request.Telefono;
                }
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
