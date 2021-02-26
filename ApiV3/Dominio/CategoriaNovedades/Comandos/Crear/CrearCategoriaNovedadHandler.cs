using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Crear
{
    public class CrearCategoriaNovedadHandler : IRequestHandler<CrearCategoriaNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCategoriaNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCategoriaNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CategoriaNovedad categoriaNovedad = new CategoriaNovedad();

                categoriaNovedad.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                if (request.ConceptoNominaId != null)
                {
                    categoriaNovedad.ConceptoNominaId = (int)request.ConceptoNominaId;
                }
                categoriaNovedad.Modulo = (ModuloSistema)request.Modulo;
                categoriaNovedad.Clase = (ClaseCategoriaNovedad)request.Clase;
                categoriaNovedad.UsaParametrizacion = (bool)request.UsaParametrizacion;
                categoriaNovedad.RequiereTercero = (bool)request.RequiereTercero;
                if (request.UbicacionTercero != null)
                {
                    categoriaNovedad.UbicacionTercero = request.UbicacionTercero;
                }
                categoriaNovedad.ValorEditable = (bool)request.ValorEditable;

                this.contexto.CategoriaNovedades.Add(categoriaNovedad);
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
