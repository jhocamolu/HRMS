using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Dependencias.Comandos.Actualizar
{
    public class ActualizarDependenciaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int DependenciaJerarquiaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + ConstantesExpresionesRegulares.Guion + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public int? DependenciaPadreId { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                #region Id
                // Elemento no existe
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var existe = dbContexto.Dependencias.SingleOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));

                    return errores;
                }
                #endregion

                #region dependenciaJerarquiaId
                // Elemento no existe
                var existe2 = dbContexto.DependenciaJerarquias.FirstOrDefault(x => x.Id == DependenciaJerarquiaId);
                if (existe2 == null && existe2.DependenciaHijoId == Id)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe ",
                       new[] { "Id" }));

                    return errores;
                }
                #endregion

                #region Codigo

                //Valida que código sea único
                var elemento = dbContexto.Dependencias.SingleOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El código que intentas guardar ya existe.",
                        new[] { "Codigo" }));
                }
                #endregion

                #region Nombre

                //Valida que Nombre sea único
                elemento = dbContexto.Dependencias.SingleOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }
                #endregion

                #region DependenciaPadreId
                if (DependenciaPadreId == null)
                {
                    var padre = dbContexto.DependenciaJerarquias.FirstOrDefault(
                                                                    x => x.DependenciaPadreId == DependenciaPadreId
                                                                    && x.Id != DependenciaJerarquiaId
                                                                    );
                    if (padre != null)
                    {
                        errores.Add(new ValidationResult(
                            $"La dependencia que intentas guardar debe tener una dependencia padre.",
                            new[] { "DependenciaPadreId" }));
                    }
                }
                else
                {
                    var existePadre = dbContexto.Dependencias.FirstOrDefault(x => x.Id == DependenciaPadreId);
                    if (existePadre == null)
                    {

                        errores.Add(new ValidationResult(
                            $"No existe esta dependencia padre.",
                            new[] { "DependenciaPadreId" }));
                    }

                    List<int> decendencia = new List<int>();
                    this.Decendencia(this.Id, dbContexto, decendencia);

                    if (decendencia.Contains((int)this.DependenciaPadreId))
                    {
                        errores.Add(new ValidationResult(
                                 $"La dependencia que intentas guardar no puede ser descendiente de la dependencia que estás asignando.",
                                 new[] { "DependenciaPadreId" }));
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
        private void Decendencia(int id, NominaDbContext contexto, List<int> decendecia)
        {
            try
            {
                var jerarquias = contexto.DependenciaJerarquias
                                                            .Where(x => x.DependenciaPadreId == id)
                                                            .ToList();
                foreach (var item in jerarquias)
                {
                    decendecia.Add(item.DependenciaHijoId);
                    Decendencia(item.DependenciaHijoId, contexto, decendecia);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
    }
}
