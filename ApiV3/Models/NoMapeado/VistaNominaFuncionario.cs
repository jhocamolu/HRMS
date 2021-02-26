using ApiV3.Infraestructura.Enumerador;

namespace ApiV3.Models.NoMapeado
{


    public class VistaNominaFuncionario
    {
        public int Id { get; set; }

        public int NominaFuncionarioId { get; set; }

        public EstadoNominaFuncionario EstadoNominaFuncionario { get; set; }

        public string CriterioBusqueda { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public int? TipoDocumentoId { get; set; }

        public string TipoDocumentoNombre { get; set; }

        public string NumeroDocumento { get; set; }

        public string Nit { get; set; }

        public int? DigitoVerificacion { get; set; }

        public double? Sueldo { get; set; }

        public int? CargoId { get; set; }

        public string CargoNombre { get; set; }

        public int? DependenciaId { get; set; }

        public string DependenciaNombre { get; set; }

        public int? CentroOperativoId { get; set; }

        public string CentroOperativoNombre { get; set; }

        public int? GrupoNominaId { get; set; }

        public string GrupoNominaNombre { get; set; }

        public int NominaId { get; set; }

        public double NetoPagar { get; set; }

        public string EstadoFuncionario { get; set; }

    }
}
