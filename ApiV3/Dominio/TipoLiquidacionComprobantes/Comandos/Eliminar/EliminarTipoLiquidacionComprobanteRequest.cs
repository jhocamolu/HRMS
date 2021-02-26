using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Eliminar
{
    public class EliminarTipoLiquidacionComprobanteRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
        
    }
}
