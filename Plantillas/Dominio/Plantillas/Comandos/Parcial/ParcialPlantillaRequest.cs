using MediatR;
using Plantillas.Dominio.Enumerador;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plantillas.Dominio.Plantillas.Comandos.Parcial
{
    public class ParcialPlantillaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public int? GrupoDocumentoId { get; set; }

        public int? DocumentoId { get; set; }

        public string Version { get; set; }

        public DateTime? FechaVigencia { get; set; }

        public int? EncabezadoId { get; set; }

        public int? PiePaginaId { get; set; }
        public string CuerpoDocumento { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (PlantillasDbContext)validationContext.GetService(typeof(PlantillasDbContext));
                var existe = dbContexto.Plantillas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                    return errores;
                }

                var elemento = dbContexto.Plantillas.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre de la plantilla que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }

                if (GrupoDocumentoId != null)
                {
                    var grupoDocumento = dbContexto.GrupoDocumentos.FirstOrDefault(x => x.Id == GrupoDocumentoId);
                    if (grupoDocumento == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El grupo de documentos que intentas guardar no existe.",
                            new[] { "GrupoDocumentoId" }));
                    }
                }

                if (GrupoDocumentoId != null)
                {
                    var documento = dbContexto.Documentos.FirstOrDefault(x => x.Id == DocumentoId);
                    if (documento == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El documento que intentas guardar no existe.",
                            new[] { "DocumentoId" }));
                    }
                }

                if (EncabezadoId != null)
                {
                    var existeEncabezado = dbContexto.ComplementoPlantillas.FirstOrDefault(x => x.Id == EncabezadoId && x.Tipo == TipoComplemento.Encabezado);
                    if (existeEncabezado == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El encabezado que intentas guardar no existe.",
                            new[] { "EncabezadoId" }));
                    }
                }

                if (PiePaginaId != null)
                {
                    var existePiePagina = dbContexto.ComplementoPlantillas.FirstOrDefault(x => x.Id == PiePaginaId && x.Tipo == TipoComplemento.PiePagina);
                    if (existePiePagina == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El pie de página que intentas guardar no existe.",
                            new[] { "PiePaginaId" }));
                    }
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
