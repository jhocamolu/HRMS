using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.AplicacionExternaCargos.Comandos.Crear
{
    public class CargoDependencia
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaId { get; set; }
    }


    public class CrearAplicacionExternaCargoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? AplicacionExternaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public TipoAplicacionExternaCargo? Tipo { get; set; }

        public int? CentroOperativoDependienteId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaIndependienteId { get; set; }

        public int? CentroOperativoIndependienteId { get; set; }

        //lista cargo dependiente
        public List<CargoDependencia> CargoDependencia { get; set; } = new List<CargoDependencia>();

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region CargoDependiente

                bool banderaCargoAsociado = false;
                if (CargoDependencia.Count != 0)
                {
                    foreach (var item in CargoDependencia)
                    {

                        var validaCargoDependiente = from te in dbContexto.AplicacionExternaCargos
                                                     join cn in dbContexto.AplicacionExternaCargoDependientes on te.Id equals cn.AplicacionExternaCargoId
                                                     where cn.CargoDependenciaId == item.CargoDependenciaId
                                                     && te.CargoDependenciaIndependienteId == CargoDependenciaIndependienteId
                                                     && te.Tipo == Tipo
                                                     && te.CentroOperativoDependienteId == CentroOperativoDependienteId
                                                     && te.AplicacionExternaId == AplicacionExternaId
                                                     select cn;

                        if (validaCargoDependiente.ToList().Count != 0)
                        {
                            banderaCargoAsociado = true;
                        }

                    }
                }
                else
                {
                    errores.Add(new ValidationResult("Requerido",
                                new[] { "CargoDependencia" }));
                }
                #endregion

                #region CargoDepnediente - CargoIndependiente
                //Valida que cargo dependiente y centro operativo ya asociado a cargo aprobador 

                if (banderaCargoAsociado == true)
                {
                    String nombreTipo = "";
                    switch (Tipo)
                    {
                        case TipoAplicacionExternaCargo.Aprobacion:
                            nombreTipo = "aprobador";
                            break;
                        case TipoAplicacionExternaCargo.Autorizacion:
                            nombreTipo = "autorizador";
                            break;
                        case TipoAplicacionExternaCargo.Revision:
                            nombreTipo = "revisor";
                            break;
                    }

                    errores.Add(new ValidationResult(
                        $"El cargo dependiente con el centro operativo que intentas guardar, ya tiene asociado un cargo " + nombreTipo + " para este visto bueno. Por favor revisa.",
                        new[] { "snackbar" }));
                }
                #endregion
                #region existe 
                var validaAplicacionExterna = dbContexto.AplicacionExternas.FirstOrDefault(x => x.Id == AplicacionExternaId);
                if (validaAplicacionExterna == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("aplicación externa"),
                       new[] { "AplicacionExternaId" }));
                }

                var validaExisteCargoIndependiente = dbContexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaIndependienteId);
                if (validaExisteCargoIndependiente == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo independiente"),
                       new[] { "CargoDependenciaIndependienteId" }));
                }
                if (CentroOperativoDependienteId != null)
                {
                    var validaExisteCentroOperativoDependiente = dbContexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoDependienteId);
                    if (validaExisteCentroOperativoDependiente == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro operativo dependiente"),
                       new[] { "CentroOperativoDependienteId" }));
                    }
                }
                if (CentroOperativoIndependienteId != null)
                {
                    var validaExisteCentroOperativoIndependiente = dbContexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoIndependienteId);
                    if (validaExisteCentroOperativoIndependiente == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro operativo independiente"),
                       new[] { "CentroOperativoIndependienteId" }));
                    }
                }
                #endregion
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
