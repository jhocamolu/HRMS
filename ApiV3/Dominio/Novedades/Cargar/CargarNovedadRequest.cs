using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ApiV3.Dominio.Novedades.Cargar
{
    public class Datos
    {
        public int FuncionarioId { get; set; }
        public decimal? Valor { get; set; }
        public double? Cantidad { get; set; }
        public int TerceroId { get; set; }
    }

    public class DatosErrores
    {
        public string Dato { get; set; }

        public string DescripcionError { get; set; }
    }

    public class CargarNovedadRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int CategoriaNovedadId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaAplicacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int PeriodoPagoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public List<int> Periodicidad { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public IFormFile Archivo { get; set; }

        public int CantidadFilas { get; set; }

        public bool Validar { get; set; }

        public List<Datos> Datos = new List<Datos>();

        public List<DatosErrores> DatosErrores = new List<DatosErrores>();

        public string NombreNovedad { get; set; }

        public bool ArchivoErrores { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {

                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var categoriaNovedad = contexto.CategoriaNovedades.Include(x => x.ConceptoNomina)
                                                                    .FirstOrDefault(x => x.Id == CategoriaNovedadId);

                if (categoriaNovedad == null)
                {
                    errores.Add(new ValidationResult(
                      $"{ConstantesErrores.NoExiste("la categoria de la novedad")}",
                      new[] { "CategoriaNovedadId" }));
                }
                else
                {
                    NombreNovedad = categoriaNovedad.Nombre;
                }
                var periodoPago = contexto.PeriodoContables.FirstOrDefault(x => x.Id == PeriodoPagoId);

                if (periodoPago == null)
                {
                    errores.Add(new ValidationResult(
                      $"{ConstantesErrores.NoExiste("la categoria de la novedad")}",
                      new[] { "CategoriaNovedadId" }));
                }

                var extencion = Archivo.FileName.Split(".");
                if (extencion[1] != "xls" && extencion[1] != "XLS" && extencion[1] != "xlsx" && extencion[1] != "XLSX")
                {

                    errores.Add(new ValidationResult(
                        $"El formato de archivo que intentas cargar no es válido, por favor revise.",
                        new[] { "DialogoError" }));

                }
                else
                {
                    using (var stream = new MemoryStream())
                    {
                        Archivo.CopyToAsync(stream);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var filas = worksheet.Dimension.Rows - 1;

                            var cantidadColumnas = worksheet.Dimension.Columns;

                            var tipoCampo = worksheet.Cells[1, 2].Value.ToString().Trim();
                            tipoCampo = tipoCampo.ToUpper();

                            // Valida que el archivo cargado sea el correcto según el tipo de novedad

                            bool requiereCantidad = categoriaNovedad.ConceptoNomina.RequiereCantidad;
                            bool requiereTercero = categoriaNovedad.RequiereTercero;

                            if (requiereTercero == true && cantidadColumnas != 3)
                            {
                                errores.Add(new ValidationResult(
                                $"El archivo que intentas cargar no cuenta con las columnas requeridas en su estructura para importar la información, por favor revise.",
                                new[] { "DialogoError" }));
                            }
                            if (requiereTercero == false && cantidadColumnas != 2)
                            {
                                errores.Add(new ValidationResult(
                                $"El archivo que intentas cargar no cuenta con las columnas requeridas en su estructura para importar la información, por favor revise.",
                                new[] { "DialogoError" }));
                            }

                            if (requiereCantidad == true && tipoCampo != "CANTIDAD")
                            {
                                errores.Add(new ValidationResult(
                                $"El archivo que intentas cargar no debe llevar en su estructura la columna valor para la novedad seleccionada, por favor revise.",
                                new[] { "DialogoError" }));
                            }
                            if (requiereCantidad == false && tipoCampo != "VALOR")
                            {
                                errores.Add(new ValidationResult(
                                $"El archivo que intentas cargar no debe llevar en su estructura la columna cantidad para la novedad seleccionada, por favor revise.",
                                new[] { "DialogoError" }));
                            }


                            JObject contarNumeroDocumento = new JObject();
                            CantidadFilas = 0;
                            var banderaCamposLlenos = true;
                            for (int row = 2; row <= filas + 1; row++)
                            {
                                var verificaCeldaNumeroDocumento = worksheet.Cells[row, 1];
                                var verificaCeldaCantidadOValor = worksheet.Cells[row, 2];
                                var verificaCeldaNitTercero = worksheet.Cells[row, 3];

                                if (verificaCeldaNumeroDocumento.Any(c => !string.IsNullOrEmpty(c.Text)))
                                {
                                    bool datoCorrecto = true;
                                    CantidadFilas++;
                                    if (errores.Count == 0)
                                    {
                                        if (Validar == true)
                                        {
                                            var numeroDocumento = worksheet.Cells[row, 1].Value.ToString().Trim();

                                            if (contarNumeroDocumento.ContainsKey($"{numeroDocumento}"))
                                            {
                                                DatosErrores.Add(new DatosErrores
                                                {
                                                    Dato = numeroDocumento,
                                                    DescripcionError = "El número de documento registrado se encuentra registrado dos veces para la misma novedad."
                                                });
                                                datoCorrecto = false;
                                            }
                                            else
                                            {
                                                contarNumeroDocumento.Add(numeroDocumento, "");
                                            }

                                            decimal valor = 0;
                                            double cantidad = 0;
                                            if (verificaCeldaCantidadOValor.Any(c => !string.IsNullOrEmpty(c.Text)))
                                            {
                                                if (tipoCampo == "VALOR")
                                                {
                                                    valor = decimal.Parse(worksheet.Cells[row, 2].Value.ToString().Trim());

                                                    if (valor <= 0)
                                                    {
                                                        DatosErrores.Add(new DatosErrores
                                                        {
                                                            Dato = numeroDocumento,
                                                            DescripcionError = "El funcionario tiene registrada una novedad con valor igual a cero o menor."
                                                        });
                                                        datoCorrecto = false;
                                                    }
                                                }
                                                else if (tipoCampo == "CANTIDAD")
                                                {
                                                    cantidad = double.Parse(worksheet.Cells[row, 2].Value.ToString().Trim());
                                                    if (cantidad <= 0)
                                                    {
                                                        DatosErrores.Add(new DatosErrores
                                                        {
                                                            Dato = numeroDocumento,
                                                            DescripcionError = "El funcionario tiene registrada una novedad con valor igual a cero o menor."
                                                        });
                                                        datoCorrecto = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                banderaCamposLlenos = false;
                                            }
                                            var nitTercero = "";
                                            int terceroId = 0;
                                            if (verificaCeldaNitTercero.Any(c => !string.IsNullOrEmpty(c.Text)))
                                            {
                                                nitTercero = worksheet.Cells[row, 3].Value.ToString().Trim();
                                                var nitDigito = nitTercero.Split("-");

                                                //consulta el tercero
                                                Tercero tercero;
                                                if (nitDigito.Count() == 2)
                                                {
                                                    tercero = contexto.Terceros.FirstOrDefault(x => x.Nit == nitDigito[0] && x.DigitoVerificacion == int.Parse(nitDigito[1]));
                                                }
                                                else
                                                {
                                                    tercero = contexto.Terceros.FirstOrDefault(x => x.Nit == nitDigito[0]);
                                                }

                                                if (tercero == null)
                                                {
                                                    DatosErrores.Add(new DatosErrores
                                                    {
                                                        Dato = numeroDocumento,
                                                        DescripcionError = "El Nit registrado para el funcionario no está registrado."
                                                    });
                                                    datoCorrecto = false;

                                                }
                                            }
                                            else
                                            {
                                                if (requiereTercero == true)
                                                {
                                                    banderaCamposLlenos = false;
                                                }
                                            }

                                            //Consulta funcionario según número de cedula
                                            var funcionario = contexto.FuncionarioDatoActuales.Include(c => c.Contrato).FirstOrDefault(x => x.NumeroDocumento == numeroDocumento);
                                            if (funcionario == null)
                                            {
                                                DatosErrores.Add(new DatosErrores
                                                {
                                                    Dato = numeroDocumento,
                                                    DescripcionError = "El número de documento no está registrado."
                                                });
                                                datoCorrecto = false;
                                            }
                                            else
                                            {
                                                if (funcionario.Contrato.Estado == EstadoContrato.Terminado)
                                                {
                                                    DatosErrores.Add(new DatosErrores
                                                    {
                                                        Dato = numeroDocumento,
                                                        DescripcionError = "El funcionario tiene un contrato terminado."
                                                    });
                                                    datoCorrecto = false;
                                                }

                                                // Verifica que la novedad no esté duplicada para la fecha
                                                var consultaNovedad = contexto.Novedades.FirstOrDefault(x => x.FuncionarioId == funcionario.Id &&
                                                                                                             x.FechaAplicacion == FechaAplicacion &&
                                                                                                             x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                                             (x.Estado == EstadoNovedad.EnCurso ||
                                                                                                              x.Estado == EstadoNovedad.Liquidada ||
                                                                                                              x.Estado == EstadoNovedad.Pendiente));

                                                if (consultaNovedad != null)
                                                {
                                                    DatosErrores.Add(new DatosErrores
                                                    {
                                                        Dato = numeroDocumento,
                                                        DescripcionError = "El número de documento ya tiene registrada una novedad en la fecha de aplicación seleccionada."
                                                    });
                                                    datoCorrecto = false;
                                                }
                                            }

                                            if (banderaCamposLlenos == false)
                                            {
                                                DatosErrores.Add(new DatosErrores
                                                {
                                                    Dato = numeroDocumento,
                                                    DescripcionError = "La información requerida para ingresar la novedad del funcionario se encuentra incompleta."
                                                });
                                                datoCorrecto = false;
                                            }

                                            if (datoCorrecto == true)
                                            {
                                                Datos.Add(new Datos
                                                {
                                                    FuncionarioId = funcionario.Id,
                                                    Valor = valor,
                                                    Cantidad = cantidad,
                                                    TerceroId = terceroId
                                                });
                                            }
                                        }
                                    }
                                }
                                else {
                                    DatosErrores.Add(new DatosErrores
                                    {
                                        Dato = "",
                                        DescripcionError = "La información requerida para ingresar la novedad del funcionario se encuentra incompleta."
                                    });
                                }
                            }
                            if (CantidadFilas == 0)
                            {
                                errores.Add(new ValidationResult(
                                $"El archivo que intentas cargar no cuenta con registros para agregar información al sistema, por favor revise.",
                                new[] { "DialogoError" }));
                            }


                        }
                    }
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;

        }
        #endregion
    }
}
