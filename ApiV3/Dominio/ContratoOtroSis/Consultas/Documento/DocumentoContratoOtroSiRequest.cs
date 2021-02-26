using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.ContratoOtroSis.Consultas.Documento
{
    public class DocumentoContratoOtroSiRequest : IRequest<CommandResult>
    {
        public int OtroSi { get; set; }
    }
}
