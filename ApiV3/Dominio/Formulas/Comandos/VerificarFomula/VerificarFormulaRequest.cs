using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace ApiV3.Dominio.Formulas.Comandos.VerificarFomula
{
    /// <summary>
    /// Clase para crear condicionales.
    /// </summary>
    public class Condicional
    {
        public XmlElement Condicion { get; set; }
        public XmlElement Verdadero { get; set; }
        public XmlElement Falso { get; set; }

        public bool EsValido()
        {
            if (this.Condicion == null || this.Verdadero == null || this.Falso == null)
            {
                return false;
            }
            return true;
        }
    }
    public class VerificarFormulaRequest : IRequest<CommandResult>, IValidatableObject
    {

        const string NUMERO = "numero";
        const string OPERADOR = "operador";
        const string CONCEPTO = "concepto";
        const string FUNCION = "funcion";
        const string AGRUPADOR = "agrupador";
        const string CONDICIONAL = "condicional";
        // Datos para condicional
        const string CONDICION = "condicion";
        const string VERDADERO = "verdadero";
        const string FALSO = "falso";
        private int Contador { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public string Formula { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var existe = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                    return errores;
                }

                if (existe.ConceptoAgrupador == false)
                {
                    if (String.IsNullOrEmpty(Formula))
                    {
                        errores.Add(new ValidationResult(
                           $"Debe ingresar un fórmula.",
                           new[] { "Formula" }));
                        return errores;
                    }
                    Contador = 0;
                    XmlElement root = this.ConvertirXml(Formula);
                    if (!this.Validador(root, false, dbContexto))
                    {
                        errores.Add(new ValidationResult(
                         $"Hay un error de sintaxis en la fórmula.",
                         new[] { "Formula" }));
                        return errores;
                    }
                    Console.WriteLine(Contador);
                }
                else
                {
                    errores.Add(new ValidationResult(
                         $"No se puede agregar una fórmula a un concepto nomina agrupador.",
                         new[] { "Formula" }));
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }

        /// <summary>
        /// Lee un elemento xml y crea un condicional 
        /// </summary>
        /// <param name="xmlelement"></param>
        /// <param name="dbContexto"></param>
        /// <returns></returns>
        private Condicional CrearCondicional(XmlElement root)
        {
            Condicional condicional = new Condicional();
            try
            {
                for (int j = 0; j < root.ChildNodes.Count; j++)
                {
                    XmlNode node = root.ChildNodes.Item(j);
                    if (node.NodeType.Equals(XmlNodeType.Element))
                    {
                        XmlElement element = (XmlElement)node;
                        var atr = element.GetAttribute("data");
                        if (atr == CONDICION)
                        {
                            condicional.Condicion = element;
                        }
                        if (atr == VERDADERO)
                        {
                            condicional.Verdadero = element;
                        }
                        if (atr == FALSO)
                        {
                            condicional.Falso = element;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return condicional;
        }

        /// <summary>
        ///  lee un elemento y valida su sintaxis.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="admiteLogico"></param>
        /// <param name="dbContexto"></param>
        /// <returns></returns>
        private bool Validador(XmlElement root, bool admiteLogico, NominaDbContext dbContexto)
        {
            try
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes.Item(i);
                    if (node.NodeType.Equals(XmlNodeType.Element))
                    {
                        XmlElement element = (XmlElement)node;
                        var tipoDatoActual = element.GetAttribute("data");
                        string tipoDatoAntes = this.TipoDeDatoAnterior(i, root);
                        string tipoDatoSiguiente = this.TipoDeDatoSiguiente(i, root);
                        if (tipoDatoActual == NUMERO)
                        {
                            if (!this.Numero(element, tipoDatoAntes)) { return false; }
                        }
                        if (tipoDatoActual == OPERADOR)
                        {
                            if (!this.Operador(element, tipoDatoAntes, tipoDatoSiguiente, admiteLogico)) { return false; }
                        }
                        if (tipoDatoActual == CONDICIONAL)
                        {
                            Condicional condicional = this.CrearCondicional(element);
                            if (!condicional.EsValido()) { return false; }
                            bool con = this.Validador(condicional.Condicion, true, dbContexto);
                            bool v = this.Validador(condicional.Verdadero, false, dbContexto);
                            bool f = this.Validador(condicional.Falso, false, dbContexto);
                            if (!con || !v || !f)
                            {
                                return false;
                            }
                        }

                        if (tipoDatoActual == CONCEPTO)
                        {
                            if (!this.Concepto(element, dbContexto, tipoDatoAntes, tipoDatoSiguiente)) { return false; }
                        }

                        if (tipoDatoActual == FUNCION)
                        {
                            if (!this.Funcion(element, dbContexto, tipoDatoAntes, tipoDatoSiguiente)) { return false; }
                        }

                        if (tipoDatoActual == AGRUPADOR)
                        {
                            if (!this.Agrupador(element, tipoDatoAntes, tipoDatoSiguiente)) { return false; }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        /// <summary>
        /// Metodo para retornar el elemento anterior
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private string TipoDeDatoAnterior(int index, XmlElement element)
        {
            for (int i = index - 1; i >= 0; i--)
            {
                XmlNode node = element.ChildNodes.Item(i);
                if (node.NodeType.Equals(XmlNodeType.Element))
                {
                    return ((XmlElement)node).GetAttribute("data");
                }
            }
            return null;
        }

        /// <summary>
        /// Metodo para retornar el elemento siguiente
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private string TipoDeDatoSiguiente(int index, XmlElement element)
        {
            for (int i = index + 1; i < element.ChildNodes.Count; i++)
            {
                XmlNode node = element.ChildNodes.Item(i);
                if (node.NodeType.Equals(XmlNodeType.Element))
                {
                    return ((XmlElement)node).GetAttribute("data");
                }
            }

            return null;
        }

        /// <summary>
        /// Validaciones de concepto.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="dbContexto"></param>
        /// <returns></returns>
        private bool Concepto(XmlElement element, NominaDbContext dbContexto, string antes, string despues)
        {
            int value = int.Parse(element.GetAttribute("valor"));
            var atributo = element.GetAttribute("data");
            if (atributo == CONCEPTO)
            {
                // Concepto donde se guarda la formula
                var concepto = dbContexto.ConceptoNominas.FirstOrDefault(f => f.Id == Id);

                // Concepto que hace parte de la formula
                var conceptoFormula = dbContexto.ConceptoNominas.FirstOrDefault(f => f.Id == value);
                if (conceptoFormula == null)
                {
                    return false;
                }
                else
                {
                    if (concepto.Id == conceptoFormula.Id)
                    {
                        return false;
                    }

                    if (concepto.ClaseConceptoNomina == ClaseConceptoNomina.Calculo)
                    {
                        if (conceptoFormula.ClaseConceptoNomina == ClaseConceptoNomina.Calculo)
                        {
                            if (conceptoFormula.Orden >= concepto.Orden) return false;
                        }
                    }
                    else if (concepto.ClaseConceptoNomina == ClaseConceptoNomina.Deduccion)
                    {
                        if (conceptoFormula.ClaseConceptoNomina == ClaseConceptoNomina.Calculo)
                        {
                            return false;
                        }
                        if (conceptoFormula.ClaseConceptoNomina == ClaseConceptoNomina.Deduccion)
                        {
                            if (conceptoFormula.Orden >= concepto.Orden) return false;
                        }
                    }
                    else if (concepto.ClaseConceptoNomina == ClaseConceptoNomina.Devengo)
                    {
                        if (conceptoFormula.ClaseConceptoNomina == ClaseConceptoNomina.Calculo)
                        {
                            return false;
                        }
                        if (conceptoFormula.ClaseConceptoNomina == ClaseConceptoNomina.Deduccion)
                        {
                            return false;
                        }
                        if (conceptoFormula.ClaseConceptoNomina == ClaseConceptoNomina.Devengo)
                        {
                            if (conceptoFormula.Orden >= concepto.Orden) return false;
                        }
                    }
                }
            }

            if (antes != null && antes != OPERADOR && antes != AGRUPADOR)
            {
                return false;
            }
            if (despues != null && despues != OPERADOR && despues != AGRUPADOR)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validaciones de funcion.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="dbContexto"></param>
        /// <returns></returns>
        private bool Funcion(XmlElement element, NominaDbContext dbContexto, string antes, string despues)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            int value = int.Parse(element.GetAttribute("valor"));
            var atributo = element.GetAttribute("data");
            var funcion = dbContexto.FuncionNominas.FirstOrDefault(f => f.Id == value);
            if (funcion == null)
            {
                return false;
            }
            if (antes != null && antes != OPERADOR && antes != AGRUPADOR)
            {
                return false;
            }
            if (despues != null && despues != OPERADOR && despues != AGRUPADOR)
            {
                return false;
            }


            return true;
        }

        /// <summary>
        /// Validaciones de operador si son logicos o matematicos. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="logico"></param>
        /// <param name="antes"></param>
        /// <param name="despues"></param>
        /// <returns></returns>
        private bool Operador(XmlElement element, string antes, string despues, bool logico)
        {
            var operadores = new string[] { "+", "-", "*", "/" };
            var logicos = new string[] { "MENORIGUAL", "MAYORIGUAL", "MENOR", "MAYOR", "=", "!=", "Y", "O" };

            var valor = element.GetAttribute("valor");

            if (antes == null || despues == null || despues == OPERADOR)
            {
                return false;
            }

            if (logico)
            {
                return operadores.Contains(valor) || logicos.Contains(valor);
            }

            return operadores.Contains(valor);
        }

        /// <summary>
        /// Validaciones de agrupador.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="antes"></param>
        /// <returns></returns>
        private bool Agrupador(XmlElement element, string antes, string despues)
        {
            var valor = element.GetAttribute("valor");
            var agrupadores = new string[] { "(", ")" };
            if (valor == "(")
            {
                Contador++;
                if (antes != null)
                {
                    if (antes != OPERADOR && antes != AGRUPADOR)
                    {
                        return false;
                    }
                }
                if (despues != null)
                {
                    if (despues == OPERADOR)
                    {
                        return false;
                    }
                }

            }
            if (valor == ")")
            {
                Contador--;
                if (antes != null)
                {
                    if (antes == OPERADOR)
                    {
                        return false;
                    }
                }
                if (despues != null)
                {
                    if (despues != OPERADOR && antes != AGRUPADOR)
                    {
                        return false;
                    }
                }
            }

            return agrupadores.Contains(valor);
        }

        /// <summary>
        /// validaciones de Numero sintaxis si es entero o decimal.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool Numero(XmlElement element, string antes)
        {
            var valor = element.GetAttribute("valor");

            Regex rgx = new Regex(ConstantesExpresionesRegulares.Decimales); // un regex 

            if (!rgx.IsMatch(valor))
            {
                return false;
            }
            if (antes == null || antes == OPERADOR || antes == AGRUPADOR)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Esta funcion convierte el texto de formula en XML.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private XmlElement ConvertirXml(string texto)
        {
            try
            {
                texto = texto.Replace("\r\n", "");
                texto = texto.Replace("<>", "!=");
                texto = texto.Replace("\"<=\"", "\"MENORIGUAL\"");
                texto = texto.Replace("\">=\"", "\"MAYORIGUAL\"");
                texto = texto.Replace("\"<\"", "\"MENOR\"");
                texto = texto.Replace("\">\"", "\"MAYOR\"");

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(texto);
                var a = xml.FirstChild.Name;
                if (a == "div")
                {
                    if (xml.FirstChild.HasChildNodes)
                    {
                        return (XmlElement)xml.FirstChild;
                    }
                    else
                    {
                        return null;
                    }
                }

                return (XmlElement)xml.FirstChild;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
