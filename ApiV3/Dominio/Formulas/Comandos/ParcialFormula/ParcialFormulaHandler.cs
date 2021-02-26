using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ApiV3.Dominio.Formulas.Comandos.ParcialFormula
{
    public class ParcialFormulaHandler : IRequestHandler<ParcialFormulaRequest, CommandResult>
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
        // Schemas base de datos
        const string DBO = "dbo";
        const string UTIL = "util";
        // Formato del procedimiento
        const string SALTO = "\r\n";
        const string ESPACIO = " ";
        const string INDENTACION = "    ";

        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        /// <summary>
        /// Lista de parámetros que se ubicarán en el encabezado del procedimiento. Ej: CREATE PROCEDURE Nombre (Lo que va aquí)
        /// </summary>
        List<string> listaParametros = new List<string>();

        /// <summary>
        /// Lista de variables que se deben declarar dentro del procedimiento
        /// </summary>
        List<string> listaVariables = new List<string>();

        /// <summary>
        /// Lista de valores se le debe pasar al procedimiento
        /// cuando se vaya a ejecutar dentro del procedimiento de calcular la nómina
        /// </summary>
        List<string> listaValoresEjecucion = new List<string>();

        /// <summary>
        /// lista de procedimientos y funciones a ejecutar para calculos
        /// dentro del procedimiento a crear
        /// </summary>
        List<string> listaElementosAEjecutar = new List<string>();

        /// <summary>
        /// diccionario de datos para los elementos de la formula según su tipo
        /// permite insertar los valores en la tabla ConceptoNominaElementoFormula
        /// </summary>
        List<ConceptoNominaElementoFormula> elementoFormula = new List<ConceptoNominaElementoFormula>();

        /// <summary>
        /// Incluye el sql que se debe utilizar para la validación de inconsistencias en caso de que se requiera.
        /// </summary>
        string usoInconsistencia = string.Empty;

        /// <summary>
        /// Id del concepto al cual se esta realizando la formula.
        /// </summary>
        private int idConceptoFormula;


        public ParcialFormulaHandler(NominaDbContext contexto, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ParcialFormulaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNomina concepto = await this.contexto.ConceptoNominas.FindAsync(request.Id);

                this.idConceptoFormula = concepto.Id;

                string quitarAcentos = Texto.QuitarAcentos(concepto.Nombre);
                string nombreProcedimiento = "DSP_Calcular" + Texto.LetraCapital(quitarAcentos).Replace(" ", "");

                if (request.Formula != null)
                {
                    string logicaProcedimiento = string.Empty;
                    string sqlProcedimiento = string.Empty;

                    logicaProcedimiento = await CrearProcedimiento(this.ConvertirXml(request.Formula));

                    listaValoresEjecucion.Add("@Resultado = @ConceptoValor OUTPUT");
                    listaValoresEjecucion.Add("@Inconsistencia = @ConceptoInconsistencia OUTPUT");

                    //Se añade el encabezado del procedimiento
                    sqlProcedimiento = this.DefinirEncabezadoProcedimiento(nombreProcedimiento, concepto.Nombre);

                    //Se añaden las instrucciones para obtener los valores de los conceptos y funciones que se requieran
                    sqlProcedimiento += listaElementosAEjecutar.Count > 0 ? SALTO + $"{String.Join(SALTO, listaElementosAEjecutar)}" : "";

                    //Se añade la lógica propia de la fórmula del concepto
                    sqlProcedimiento += SALTO + SALTO + INDENTACION + INDENTACION + "-- Lógica de la fórmula asociada al concepto.";
                    sqlProcedimiento += SALTO + INDENTACION + INDENTACION + $"SELECT @Resultado = ({logicaProcedimiento});";

                    //Se añaden las validaciones asociadas a las inconsistencias
                    sqlProcedimiento += (usoInconsistencia != null) ? SALTO + SALTO + INDENTACION + INDENTACION + $"{usoInconsistencia}" : "";

                    //Se añade el pie del procedimiento
                    sqlProcedimiento += this.DefinirPieProcedimiento();

                    //Se crea o modifica el procedimiento en la base de datos
                    Console.WriteLine(sqlProcedimiento);
                    var crearEnBd = repositorio.Query(sqlProcedimiento);

                    //Si no se presenta ningún error en la base de datos, se actualiza la información de la formula para el concepto
                    if (!crearEnBd.Any())
                    {
                        string ejecucionParametros = listaValoresEjecucion.Count > 0 ? $"{String.Join(",", listaValoresEjecucion)}" : "";
                        concepto.Formula = request.Formula;
                        concepto.ProcedimientoSql = sqlProcedimiento;
                        concepto.ProcedimientoNombre = $"[{DBO}]." + nombreProcedimiento + " " + ejecucionParametros;

                        var c = concepto.ProcedimientoNombre.ToString().Length;
                    }
                    else
                    {
                        return CommandResult.Fail("No se ha podido crear la fórmula en la base de datos.");
                    }
                }
                this.contexto.ConceptoNominas.Update(concepto);
                await this.contexto.SaveChangesAsync();


                if (elementoFormula.Any())
                {
                    ElementoFormulas();
                }

                return CommandResult.Success(concepto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// Este metodo crea los SQL segun el comportamiento de la formula.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private async Task<string> CrearProcedimiento(XmlElement root)
        {
            try
            {
                string sql = "";
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes.Item(i);
                    if (node.NodeType.Equals(XmlNodeType.Element))
                    {
                        XmlElement element = (XmlElement)node;
                        var tipoDatoActual = element.GetAttribute("data");
                        var valor = element.GetAttribute("valor");
                        if (tipoDatoActual == CONCEPTO)
                        {
                            sql = sql + this.Concepto(element);
                        }
                        if (tipoDatoActual == FUNCION)
                        {
                            sql = sql + await this.Funcion(element);
                        }
                        if (tipoDatoActual == AGRUPADOR)
                        {
                            sql = sql + $"{valor}";
                        }
                        if (tipoDatoActual == NUMERO)
                        {
                            sql = sql + $"{valor}";
                        }
                        if (tipoDatoActual == OPERADOR)
                        {
                            if (valor == "!=")
                            {
                                valor = " <> ";
                            }
                            if (valor == "MENOR")
                            {
                                valor = " < ";
                            }
                            if (valor == "MAYOR")
                            {
                                valor = " > ";
                            }
                            if (valor == "MENORIGUAL")
                            {
                                valor = " <= ";
                            }
                            if (valor == "MAYORIGUAL")
                            {
                                valor = " >= ";
                            }
                            if (valor == "Y")
                            {
                                valor = " AND ";
                            }
                            if (valor == "O")
                            {
                                valor = " OR ";
                            }
                            sql = sql + $"{valor}";
                        }
                        if (tipoDatoActual == CONDICIONAL)
                        {
                            Condicional condicional = this.CrearCondicional(element);
                            string con = await this.CrearProcedimiento(condicional.Condicion);
                            string v = await this.CrearProcedimiento(condicional.Verdadero);
                            string f = await this.CrearProcedimiento(condicional.Falso);
                            sql = sql + $"CASE WHEN {con} THEN {v} ELSE {f} END ";
                        }
                    }
                }
                return sql;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Crea una condicional.
        /// </summary>
        /// <param name="root"></param>
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return condicional;
        }

        /// <summary>
        /// Validacion de conceptos.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private string Concepto(XmlElement element)
        {
            string sql = "";
            string nombreVariableAlias = string.Empty;
            string nombreVariableValor = string.Empty;
            string declaracionVariable = string.Empty;
            string obtenerValorConcepto = string.Empty;

            try
            {
                int value = int.Parse(element.GetAttribute("valor"));
                var atributo = element.GetAttribute("data");
                var concepto = contexto.ConceptoNominas.FirstOrDefault(f => f.Id == value);

                // Se adicionan a la lista de parámetros y lista de ejecución los datos respectivos para el concepto
                if (!listaParametros.Contains("@FuncionarioId INT"))
                {
                    listaParametros.Add("@FuncionarioId INT");
                    listaValoresEjecucion.Add("@FuncionarioId = {{FuncionarioId}}");
                }
                if (!listaParametros.Contains("@NominaId INT"))
                {
                    listaParametros.Add("@NominaId INT");
                    listaValoresEjecucion.Add("@NominaId = {{NominaId}}");
                }
                if (!listaParametros.Contains("@NominaDetalleId INT"))
                {
                    listaParametros.Add("@NominaDetalleId INT");
                    listaValoresEjecucion.Add("@NominaDetalleId = NULL");
                }


                // Se adicionan a la lista de variables los datos respectivos para el concepto

                // Se adiciona la variable que contendrá el alias del concepto
                nombreVariableAlias = concepto.Alias.Replace("_", " ");
                nombreVariableAlias = "Alias" + Texto.LetraCapital(nombreVariableAlias).Replace(" ", "");
                declaracionVariable = $"DECLARE @{nombreVariableAlias} VARCHAR(MAX) = '{concepto.Alias}';";

                if (!listaVariables.Contains(declaracionVariable))
                {
                    listaVariables.Add(declaracionVariable);
                }

                // Se adiciona la variable para la obtención del valor del concepto
                nombreVariableValor = concepto.Alias.Replace("_", " ");
                nombreVariableValor = "Valor" + Texto.LetraCapital(nombreVariableValor).Replace(" ", "");
                declaracionVariable = $"DECLARE @{nombreVariableValor} MONEY;";

                if (!listaVariables.Contains(declaracionVariable))
                {
                    listaVariables.Add(declaracionVariable);
                }

                // Se la obtención del valor del concepto a la lista pertinente
                obtenerValorConcepto = SALTO + INDENTACION + INDENTACION + $"-- Se calcula el valor para el concepto \"{concepto.Alias}\".";
                obtenerValorConcepto += SALTO + INDENTACION + INDENTACION + $@"EXECUTE [{DBO}].[USP_ObtenerValorConceptoNomina] @FuncionarioId, @NominaId, @{nombreVariableAlias}, @NominaDetalleId, @{nombreVariableValor} OUTPUT;";

                if (!listaElementosAEjecutar.Contains(obtenerValorConcepto))
                {
                    listaElementosAEjecutar.Add(obtenerValorConcepto);
                }
                var validarElemento = elementoFormula.Where(x => x.ConceptoNominaId == this.idConceptoFormula && x.ElementoFormulaId == concepto.Id && x.Tipo == TipoElementoFormula.Concepto);
                if (!validarElemento.Any())
                {
                    this.elementoFormula.Add(new ConceptoNominaElementoFormula
                    {
                        ConceptoNominaId = this.idConceptoFormula,
                        ElementoFormulaId = concepto.Id,
                        Tipo = TipoElementoFormula.Concepto
                    });
                }
                sql = $"@{nombreVariableValor}";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return sql;
        }

        /// <summary>
        /// Validacion de funciones.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private async Task<string> Funcion(XmlElement element)
        {
            string declaracionVariable = string.Empty;
            string salidaInconsistencia = null;
            string parametroOutput = "";
            string salida = "";
            string procedimientoSalida = "";
            string obtenerValorFuncion = string.Empty;

            //Listado de parámetros que se pasarán al procedimiento o función
            List<string> parametroInput = new List<string>();

            //Listado de declaración de variables que se deben tener para hacer llamado de la función
            List<string> declareInput = new List<string>();

            try
            {
                int value = int.Parse(element.GetAttribute("valor"));
                //var atributo = element.GetAttribute("data");
                FuncionNomina funcion = await contexto.FuncionNominas.FindAsync(value);
                List<FuncionVariable> funcionVariable = contexto.FuncionVariables.Where(fv => fv.FuncionNominaId == funcion.Id).Include(fv => fv.VariableNomina).OrderBy(fv => fv.Orden).ToList();

                string NombreVariable = funcion.Alias.Replace("_", " ");
                NombreVariable = Texto.LetraCapital(NombreVariable).Replace(" ", "");

                //Se recorren todas las variables asociadas a la función
                foreach (var item in funcionVariable)
                {
                    //Si el valor por defecto es nulo, quiere decir que la variable debe ser un parámetro del procedimiento
                    if (item.ValorDefecto == null)
                    {
                        string parametroGeneral = $"@{item.VariableNomina.Codigo} {item.VariableNomina.TipoDato}";
                        parametroGeneral += item.VariableNomina.Tamanio == null ? "" : $"({item.VariableNomina.Tamanio})";

                        if (!listaParametros.Contains(parametroGeneral))
                        {
                            if (item.VariableNomina.TipoVariable == TipoVariable.INPUT)
                            {
                                listaParametros.Add(parametroGeneral);
                                listaValoresEjecucion.Add("@" + item.VariableNomina.Codigo + " = {{" + item.VariableNomina.Codigo + "}}");
                            }
                        }
                    }
                    else
                    {
                        if (item.VariableNomina.TipoVariable == TipoVariable.INPUT)
                        {
                            declaracionVariable = $"DECLARE @{item.VariableNomina.Codigo}{NombreVariable} {item.VariableNomina.TipoDato}";
                            declaracionVariable += item.VariableNomina.Tamanio == null ? "" : $"({item.VariableNomina.Tamanio})";
                            declaracionVariable += $" = '{item.ValorDefecto}';";

                            //Se adiciona la declaración la variable con valor por defecto al listado de variables del procedimiento
                            if (!listaVariables.Contains(declaracionVariable))
                            {
                                listaVariables.Add(declaracionVariable);
                            }

                            //declareInput.Add(declareGeneral);
                        }
                    }

                    //Se establece la variable como parametro en caso de que sea procedimiento almacenado a nivel de base de datos
                    string parametroProceso = item.ValorDefecto == null ? $"@{item.NombreParametro} =  @{item.VariableNomina.Codigo}" : $"@{item.NombreParametro} = @{item.VariableNomina.Codigo}{NombreVariable}";

                    //Se establece la variable como parametro en caso de que sea una función a nivel de base de datos
                    string parametroFuncion = item.ValorDefecto == null ? $"@{item.VariableNomina.Codigo}" : $"@{item.VariableNomina.Codigo}{NombreVariable} ";


                    if (!parametroInput.Contains(parametroProceso))
                    {

                        if (item.VariableNomina.TipoVariable == TipoVariable.INPUT)
                        {
                            if (funcion.TipoFuncion == TipoFuncion.USP) parametroInput.Add(parametroProceso);

                            if (funcion.TipoFuncion == TipoFuncion.UFS) parametroInput.Add(parametroFuncion);

                        }
                        else if (item.VariableNomina.TipoVariable == TipoVariable.OUTPUT)
                        {
                            if (item.NombreParametro != null)
                            {
                                parametroOutput = $"@{Texto.LetraCapital(item.NombreParametro.Replace("_", ""))}{NombreVariable}";
                            }
                            else
                            {
                                parametroOutput = $"@{Texto.LetraCapital(item.VariableNomina.Codigo.Replace("_", ""))}{NombreVariable}";
                            }

                            if (item.VariableNomina.Codigo != "Inconsistencia")
                            {
                                salida = parametroOutput;
                                procedimientoSalida = $"@{item.NombreParametro} = {parametroOutput}";
                                declaracionVariable = $@"DECLARE {parametroOutput} {item.VariableNomina.TipoDato}";
                                declaracionVariable += item.VariableNomina.Tamanio == null ? "" : $"({item.VariableNomina.Tamanio})";
                                declaracionVariable += item.ValorDefecto == null ? ";" : item.ValorDefecto == "NULL" ? $"= {item.ValorDefecto};" : $"= '{item.ValorDefecto}';";
                            }
                            else
                            {
                                //Se agrega variable para Auditoria Activo
                                string variableAuditoriaActivo = "DECLARE @AuditoriaActivo VARCHAR(255) = (SELECT vce.AUDITORIA_ACTIVO FROM util.VW_ConstanteEstado vce);";
                                if (!listaVariables.Contains(variableAuditoriaActivo))
                                {
                                    listaVariables.Add(variableAuditoriaActivo);
                                }

                                //Se define la validación de la inconsistencia
                                usoInconsistencia = "-- Se valida si el resultado corresponde a alguna de las inconsistencias de liquidación de nómina.";
                                usoInconsistencia += SALTO + INDENTACION + INDENTACION + $"SELECT @Inconsistencia = tic.Mensaje FROM dbo.TipoInconsistencia tic WHERE tic.Valor = @Resultado AND tic.EstadoRegistro = @AuditoriaActivo;";

                                declaracionVariable = $@"DECLARE {parametroOutput} {item.VariableNomina.TipoDato}";
                                declaracionVariable += item.VariableNomina.Tamanio == null ? "" : $"({item.VariableNomina.Tamanio})";
                                declaracionVariable += item.ValorDefecto == null ? ";" : item.ValorDefecto == "NULL" ? $"= {item.ValorDefecto};" : $"= '{item.ValorDefecto}';";

                                salidaInconsistencia = $"@{item.NombreParametro} = {parametroOutput}";
                            }

                            //Se adiciona la variable donde se almacenará el resultado de llamar al procedimiento o función
                            if (!listaVariables.Contains(declaracionVariable))
                            {
                                listaVariables.Add(declaracionVariable);
                            }

                        }
                    }
                }

                if (funcion.TipoFuncion == TipoFuncion.USP)
                {
                    string parameter = parametroInput.Count > 0 ? $"{String.Join(",", parametroInput)}" : null;
                    //string declares = declareInput.Count > 0 ? $"{String.Join(";", declareInput)}" : "";

                    // Se la obtención del valor del concepto a la lista pertinente
                    obtenerValorFuncion = SALTO + INDENTACION + INDENTACION + $"-- Se calcula el valor para la función de aplicación \"{funcion.Alias}\".";
                    obtenerValorFuncion += SALTO + INDENTACION + INDENTACION + $@"EXECUTE {funcion.Proceso} ";
                    obtenerValorFuncion += parameter != null ? $"{parameter}, " : "";
                    obtenerValorFuncion += $"{procedimientoSalida} OUTPUT";
                    obtenerValorFuncion += salidaInconsistencia != null ? $", {salidaInconsistencia} OUTPUT; " : "";
                }
                else if (funcion.TipoFuncion == TipoFuncion.UFS)
                {
                    string parameter = parametroInput.Count > 0 ? $"{String.Join(",", parametroInput)}" : "";
                    //string declares = declareInput.Count > 0 ? $"{String.Join(";", declareInput)}" : "";

                    obtenerValorFuncion = SALTO + INDENTACION + INDENTACION + $"-- Se calcula el valor para la función de aplicación \"{funcion.Alias}\".";
                    obtenerValorFuncion += SALTO + INDENTACION + INDENTACION + $@"SELECT {parametroOutput} = {funcion.Proceso} ({parameter});";
                }

                if (!listaElementosAEjecutar.Contains(obtenerValorFuncion))
                {
                    listaElementosAEjecutar.Add(obtenerValorFuncion);
                }

                var sql = $"{salida}";
                var validarElemento = elementoFormula.Where(x => x.ConceptoNominaId == this.idConceptoFormula && x.ElementoFormulaId == funcion.Id && x.Tipo == TipoElementoFormula.Funcion);
                if (!validarElemento.Any())
                {
                    this.elementoFormula.Add(new ConceptoNominaElementoFormula
                    {
                        ConceptoNominaId = this.idConceptoFormula,
                        ElementoFormulaId = funcion.Id,
                        Tipo = TipoElementoFormula.Funcion
                    });
                }

                return sql;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Leer formula y convertir a XML
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
                return (XmlElement)xml.FirstChild;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool ExisteProcedimiento(string procedureNombre)
        {
            try
            {
                string consultarProcedure = $@"SELECT [{UTIL}].[UFS_ExisteObjetoBaseDatos] ('{procedureNombre}') as Existe";
                var existe = repositorio.Query(consultarProcedure);

                if (existe.First().Existe)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Define el encabezado del procedimiento. {Create | Alter} nombre, parámetros y sección de 
        /// instrucciones de configuración
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre para procedimiento almacenado</param>
        /// <param name="nombreConcepto">Nombre del concepto para el que se crea el procedimiento</param>
        /// <returns>Encabezado del procedimiento</returns>
        private string DefinirEncabezadoProcedimiento(string nombreProcedimiento, string nombreConcepto)
        {
            string encabezadoProcedimiento;

            // Se añaden los parámetros de salida que tienen todos los procedimientos asociados a fórmulas de conceptos
            listaParametros.Add("@Resultado MONEY OUTPUT");
            listaParametros.Add("@Inconsistencia VARCHAR(255) OUTPUT");

            //Sección con la documentación del procedimiento
            encabezadoProcedimiento = $"-- ==========================================================================================";
            encabezadoProcedimiento += SALTO + $"-- Author:      Sistema";
            encabezadoProcedimiento += SALTO + $"-- Create date: {DateTime.Now.ToString()}";
            encabezadoProcedimiento += SALTO + $"-- Description: Procedimiento dinámico creado a partir de la fórmula del concepto ";
            encabezadoProcedimiento += SALTO + $"--              de nómina \"{nombreConcepto}\".  Permite el cálculo del valor ";
            encabezadoProcedimiento += SALTO + $"--              del concepto durante el proceso de liquidación de nómina.";
            encabezadoProcedimiento += SALTO + $"-- ==========================================================================================";
            encabezadoProcedimiento += SALTO;

            //Sección con el nombre del procedimiento y parámetros
            if (!this.ExisteProcedimiento(nombreProcedimiento))
            {
                encabezadoProcedimiento += $"CREATE PROCEDURE [{DBO}].{nombreProcedimiento}";
            }
            else
            {
                encabezadoProcedimiento += $"ALTER PROCEDURE [{DBO}].{nombreProcedimiento}";
            }
            encabezadoProcedimiento += listaParametros.Count > 0 ? SALTO + $"({String.Join("," + SALTO + ESPACIO, listaParametros) + SALTO})" : "";
            encabezadoProcedimiento += SALTO;
            encabezadoProcedimiento += "AS";
            encabezadoProcedimiento += SALTO;

            //Inicio del procedimiento
            encabezadoProcedimiento += "BEGIN";
            encabezadoProcedimiento += SALTO;

            // Sección con instrucciones de configuración y manejo de errores
            encabezadoProcedimiento += SALTO + INDENTACION + "--------------------------------------------------------------------------";
            encabezadoProcedimiento += SALTO + INDENTACION + "-- Instrucciones de configuración y manejo de errores";
            encabezadoProcedimiento += SALTO + INDENTACION + "--------------------------------------------------------------------------";
            encabezadoProcedimiento += SALTO + INDENTACION + "SET XACT_ABORT, NOCOUNT, ANSI_NULLS, QUOTED_IDENTIFIER ON;";
            encabezadoProcedimiento += SALTO + INDENTACION + "DECLARE @Parametros VARCHAR(MAX) = NULL";
            encabezadoProcedimiento += SALTO + INDENTACION + "DECLARE @NombreObjeto VARCHAR(256)= OBJECT_NAME(@@PROCID);";

            // Sección con declaración de variables utilizadas por el procedimiento
            encabezadoProcedimiento += SALTO;
            encabezadoProcedimiento += SALTO + INDENTACION + "--------------------------------------------------------------------------";
            encabezadoProcedimiento += SALTO + INDENTACION + "-- Variables";
            encabezadoProcedimiento += SALTO + INDENTACION + "--------------------------------------------------------------------------";
            encabezadoProcedimiento += listaVariables.Count > 0 ? SALTO + INDENTACION + $"{String.Join(SALTO + INDENTACION, listaVariables)}" : "";

            // Sección donde inicia el proceso o lógica del procedimiento dentro del bloque Try principal
            encabezadoProcedimiento += SALTO;
            encabezadoProcedimiento += SALTO + INDENTACION + "--------------------------------------------------------------------------";
            encabezadoProcedimiento += SALTO + INDENTACION + "-- Proceso";
            encabezadoProcedimiento += SALTO + INDENTACION + "--------------------------------------------------------------------------";
            encabezadoProcedimiento += SALTO + INDENTACION + "BEGIN TRY";

            return encabezadoProcedimiento;
        }

        /// <summary>
        /// Define el pie del procedimiento, cierre del bloque try principal, catch.
        /// </summary>
        /// <returns></returns>
        private string DefinirPieProcedimiento()
        {
            string pieProcedimiento = string.Empty;

            //Fin bloque Try principal
            pieProcedimiento += SALTO + SALTO + INDENTACION + "END TRY";

            //Inicio bloque catch principal
            pieProcedimiento += SALTO + INDENTACION + "BEGIN CATCH";

            //Sección para el rollback de la transacción si se presenta un error
            pieProcedimiento += SALTO;
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "-- Rollback de la transacción si existe";
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "IF XACT_STATE() <> 0 AND @@TRANCOUNT > 0";
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "BEGIN";
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + INDENTACION + "ROLLBACK";
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "END;";

            //Sección para el rollback de la transacción si se presenta un error
            pieProcedimiento += SALTO;
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "-- Se almacena la información del error";
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "EXEC util.USP_Registrarerror @NombreObjeto, @Parametros;";

            //Sección para lanzar la excepción 
            pieProcedimiento += SALTO;
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "-- Se lanza la excepción";
            pieProcedimiento += SALTO + INDENTACION + INDENTACION + "EXEC util.USP_LanzarExcepcion;";

            //Fin bloque catch principal
            pieProcedimiento += SALTO;
            pieProcedimiento += SALTO + INDENTACION + "END CATCH;";

            //Cierre del procedimiento
            pieProcedimiento += SALTO;
            pieProcedimiento += SALTO + "END";

            return pieProcedimiento;
        }

        private void ElementoFormulas()
        {
            try
            {
                var consultaExisteElementos = contexto.ConceptoNominaElementoFormulas.Where(e => e.ConceptoNominaId == this.idConceptoFormula).ToList();

                if (consultaExisteElementos.Any())
                {
                    int status = contexto.Database.ExecuteSqlRaw("DELETE FROM ConceptoNominaElementoFormula WHERE ConceptoNominaId={0}", this.idConceptoFormula);
                }

                if (this.elementoFormula.Any())
                {
                    foreach (var item in this.elementoFormula)
                    {
                        this.contexto.ConceptoNominaElementoFormulas.Add(item);
                        this.contexto.SaveChanges();
                    }
                }
            }
            catch (Exception m)
            {
                throw new Exception(m.Message);
            }
        }
    }

}
