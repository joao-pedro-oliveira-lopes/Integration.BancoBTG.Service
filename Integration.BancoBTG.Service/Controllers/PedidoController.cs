using Microsoft.AspNetCore.Mvc;
using Integration.BancoBTG.Service.Services;
using Integration.BancoBTG.Service.Models;
using Integration.BancoBTG.Service.Util;

namespace Integration.BancoBTG.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public IActionResult GetAllPedidos()
        {
            var result = _pedidoService.GetAllPedidos();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpGet("{pedidoId}/valor-total")]
        public IActionResult GetValorTotalPedido(int pedidoId)
        {
            var result = _pedidoService.GetValorTotalPedido(pedidoId);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound(result.Error);
        }

        [HttpGet("cliente/{clienteId}/quantidade")]
        public IActionResult GetQuantidadePedidosPorCliente(int clienteId)
        {
            var result = _pedidoService.GetQuantidadePedidosPorCliente(clienteId);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound(result.Error);
        }

        [HttpGet("cliente/{clienteId}")]
        public IActionResult GetPedidosPorCliente(int clienteId)
        {
            var result = _pedidoService.GetPedidosPorCliente(clienteId);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound(result.Error);
        }
    }
}
