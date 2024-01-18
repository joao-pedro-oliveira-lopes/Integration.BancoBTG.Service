using Integration.BancoBTG.Service.Models;
using Integration.BancoBTG.Service.Util;

namespace Integration.BancoBTG.Service.Services
{
    public interface IPedidoService
    {
        Result<IEnumerable<Pedido>> GetAllPedidos();
        Result<Pedido> GetPedidoById(int id);
        Result<ItemPedido> AdicionarItemAoPedido(int pedidoId, ItemPedido novoItem);
        Result<decimal> GetValorTotalPedido(int pedidoId);
        Result<int> GetQuantidadePedidosPorCliente(int clienteId);
        Result<List<Pedido>> GetPedidosPorCliente(int clienteId);
    }
}
