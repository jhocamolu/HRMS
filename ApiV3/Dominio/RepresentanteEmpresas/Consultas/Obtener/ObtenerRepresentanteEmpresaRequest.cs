using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;

namespace ApiV3.Dominio.RepresentanteEmpresas.Consultas.Obtener
{
    public class ObtenerRepresentanteEmpresaRequest : IRequest<CommandResult>
    {
        public string GrupoDocumentoSlug { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

    }
}
