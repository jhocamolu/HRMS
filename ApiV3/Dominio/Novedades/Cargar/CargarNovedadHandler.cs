using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using ApiV3.Servicios.Peticion;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Novedades.Cargar
{
    public class CargarNovedadHandler :  IRequestHandler<CargarNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        

        public CargarNovedadHandler( NominaDbContext contexto)
        {
            this.contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<CommandResult> Handle(CargarNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.Validar == true)
                {
                    foreach (var dato in request.Datos)
                    {
                        Novedad novedad = new Novedad();
                        novedad.FuncionarioId = (int)dato.FuncionarioId;
                        novedad.CategoriaNovedadId = (int)request.CategoriaNovedadId;
                        novedad.FechaAplicacion = (DateTime)request.FechaAplicacion;
                        novedad.Unidad = UnidadMedida.Unidad;
                        novedad.Valor = dato.Valor;
                        novedad.Cantidad = dato.Cantidad;
                        if (dato.TerceroId != 0)
                        {
                            novedad.TerceroId = dato.TerceroId;
                        }
                        novedad.Estado = EstadoNovedad.Pendiente;
                        contexto.Novedades.Add(novedad);
                        await contexto.SaveChangesAsync();

                        novedad.NovedadSubperiodos = null;
                        
                        foreach (var item in request.Periodicidad)
                        {
                            NovedadSubperiodo subperiodo = new NovedadSubperiodo { };
                            subperiodo.NovedadId = novedad.Id;
                            subperiodo.SubperiodoId = item;
                            contexto.NovedadSubperiodos.Add(subperiodo);
                            contexto.SaveChanges();
                        }
                        
                    }

                    if (request.DatosErrores.Count == 0)
                    {
                        return CommandResult.Success();
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("InconsistenciasNovedades");
                            worksheet.SetValue(1, 1, "Cédula");
                            worksheet.SetValue(1, 2, "Inconsistencia");
                            var i = 1;
                            foreach (var error in request.DatosErrores)
                            {
                                i++;
                                worksheet.SetValue(i, 1, error.Dato);
                                worksheet.SetValue(i, 2, error.DescripcionError);
                            }
                            i++;
                            worksheet.SetValue(i, 1, "Cantidad de registros procesados: " + request.Datos.Count);
                            i++;
                            worksheet.SetValue(i, 1, "Cantidad de registros inconsistentes: " + request.DatosErrores.Count);
                            package.Save();
                        }
                        stream.Position = 0;

                        string palabaConTildes = request.NombreNovedad;

                        string palabaSinTildes = Regex.Replace(palabaConTildes.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                        string excelName = $"{palabaSinTildes}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                        request.ArchivoErrores = true;

                        dynamic respuestaExcel = new
                        {
                            stream,
                            excelName,
                            request.ArchivoErrores
                        };

                        return CommandResult.Success(respuestaExcel);
                    }
                }
                else
                {
                    request.ArchivoErrores = false;
                    return CommandResult.Success(request);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
