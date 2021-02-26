using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.InformacionFamiliares.Comandos.Actualizar
{
    public class ActualizarInformacionFamiliarRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string PrimerNombre { get; set; }

        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string PrimerApellido { get; set; }

        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int SexoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int ParentescoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? Dependiente { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Maximo + " 1")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + " 15")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + "]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string NumeroDocumento { get; set; }

        public int? NivelEducativoId { get; set; }

        public string TelefonoFijo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Celular { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int DivisionPoliticaNivel2Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Direccion { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                //Valida si elemento Existe
                var existe = dbContexto.InformacionFamiliares.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
                #endregion
                #region Funcionario 
                var existeFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (existeFuncionario == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "FuncionarioId" }));
                }
                #endregion
                #region Sexo 
                var existeSexo = dbContexto.Sexos.FirstOrDefault(x => x.Id == SexoId);
                if (existeSexo == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "SexoId" }));
                }
                #endregion
                #region TipoDocumento 
                var existeTipoDocumento = dbContexto.TipoDocumentos.FirstOrDefault(x => x.Id == TipoDocumentoId);
                if (existeTipoDocumento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "TipoDocumentoId" }));
                }
                #endregion
                #region Nivel Educativo
                if (NivelEducativoId != null)
                {
                    var existeNivelEducativo = dbContexto.NivelEducativos.FirstOrDefault(x => x.Id == NivelEducativoId);
                    if (existeNivelEducativo == null)
                    {
                        errores.Add(new ValidationResult(
                           $"No Existe",
                           new[] { "NivelEducativoId" }));
                    }
                }
                #endregion
                #region Division Política
                //Valida División Política nivel 2 exista
                var divisionPoliticaNivel2 = dbContexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                if (divisionPoliticaNivel2 == null)
                {
                    errores.Add(new ValidationResult(
                        $"No División política",
                        new[] { "DivisionPoliticaNivel2Id" }
                        ));
                }
                #endregion

                #region Numero Documento
                //Valida que NumeroDocumento sea único
                var element = dbContexto.InformacionFamiliares.FirstOrDefault(x => x.NumeroDocumento == NumeroDocumento && x.Id != Id);
                if (element != null)
                {
                    errores.Add(new ValidationResult(
                        $"El número de documento que intentas guardar ya existe.",
                        new[] { "NumeroDocumento" }));
                }
                #endregion
                #region Parentesco

                // Valida parentesco exista
                var personasPersonas = dbContexto.Parentescos.FirstOrDefault(x => x.Id == ParentescoId);
                if (personasPersonas == null)
                {
                    errores.Add(new ValidationResult(
                        $"El Parentesco no existe",
                        new[] { "ParentescoId" }));
                }
                else
                {
                    //Valida que el parentesco sea menor al numero de personas permitidas
                    var validaFuncionarioPersonas = dbContexto.InformacionFamiliares.Where(x => x.FuncionarioId == FuncionarioId
                                                                                        && x.ParentescoId == ParentescoId
                                                                                        && x.Id != Id)
                                                                                    .ToList();
                    if (validaFuncionarioPersonas != null)
                    {
                        if (validaFuncionarioPersonas.Count >= personasPersonas.NumeroPersonasPermitidas)
                        {
                            errores.Add(new ValidationResult(
                                $"El nombre de parentesco que intentas guardar ya existe.",
                                new[] { "ParentescoId" }));
                        }
                    }
                }
                #endregion
                #region FechaNacimiento
                DateTime fechaVacia = DateTime.MinValue;
                if (FechaNacimiento != fechaVacia)
                {
                    DateTime fechaActual = DateTime.Today;
                    if (FechaNacimiento > fechaActual)
                    {
                        errores.Add(new ValidationResult("La fecha no debe ser mayor a la fecha actual.",
                            new[] { "FechaNacimiento" }));
                    }
                    else if (FechaNacimiento < fechaActual.AddYears(-100))
                    {
                        errores.Add(new ValidationResult("La edad del familiar no debe ser mayor 100 años.",
                            new[] { "FechaNacimiento" }));
                    }
                }
                else
                {
                    errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaNacimiento" }));
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
