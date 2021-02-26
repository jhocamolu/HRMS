using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Actualizar
{
    public class ActualizarCategoriaNovedadHandler : IRequestHandler<ActualizarCategoriaNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCategoriaNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCategoriaNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CategoriaNovedad categoriaNovedad = contexto.CategoriaNovedades.Find(request.Id);
                categoriaNovedad.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                categoriaNovedad.ConceptoNominaId = request.ConceptoNominaId;
                categoriaNovedad.Modulo = (ModuloSistema)request.Modulo;
                categoriaNovedad.Clase = (ClaseCategoriaNovedad)request.Clase;
                categoriaNovedad.UsaParametrizacion = (bool)request.UsaParametrizacion;
                categoriaNovedad.RequiereTercero = (bool)request.RequiereTercero;
                categoriaNovedad.UbicacionTercero = request.UbicacionTercero;
                categoriaNovedad.ValorEditable = (bool)request.ValorEditable;

                this.contexto.CategoriaNovedades.Update(categoriaNovedad);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(categoriaNovedad);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
