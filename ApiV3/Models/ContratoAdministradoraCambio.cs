using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("VW_ContratoAdministradoraCambios")]
    public class ContratoAdministradoraCambio
    {
        [Key]
        public int Id { get; set; }

        public int ContratoId { get; set; }
        public virtual Contrato Contrato { get; set; }

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public int TipoAdministradoraId { get; set; }
        public virtual TipoAdministradora TipoAdministradora { get; set; }


        public string Anterior { get; set; }

        public string Actual { get; set; }

        public int AdministradoraId { get; set; }
        public virtual Administradora Administradora { get; set; }

        public DateTime? FechaInicio { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

    }
}
