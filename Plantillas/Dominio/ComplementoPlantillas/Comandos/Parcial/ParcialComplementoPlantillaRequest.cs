using MediatR;
using Plantillas.Dominio.Enumerador;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plantillas.Dominio.ComplementoPlantillas.Comandos.Parcial
{
    public class ParcialComplementoPlantillaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }


        [Range(minimum: 1, maximum: 99, ErrorMessage = ConstantesErrores.Rango + "1 a 99.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Alto { get; set; }

        [EnumDataType(typeof(TipoComplemento), ErrorMessage = "No es un tipo de complemento.")]
        public TipoComplemento? Tipo { get; set; }

        public int? GrupoDocumentoId { get; set; }

        public string CuerpoDocumento { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (PlantillasDbContext)validationContext.GetService(typeof(PlantillasDbContext));
                var existe = dbContexto.ComplementoPlantillas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                    return errores;
                }

                var elemento = dbContexto.ComplementoPlantillas.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre del complemento que intentas guardar ya existe.",
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

            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
