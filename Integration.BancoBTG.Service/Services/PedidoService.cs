using Integration.BancoBTG.Service.Data;
using Integration.BancoBTG.Service.Models;
using Integration.BancoBTG.Service.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Integration.BancoBTG.Service.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Result<IEnumerable<Pedido>> GetAllPedidos()
        {
            try
            {
                var pedidos = _context.Pedidos
                                      .Include(p => p.Itens)  // Carrega também os itens do pedido
                                      .ToList();

                return Result<IEnumerable<Pedido>>.Success(pedidos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Pedido>>.Failure(ex.Message);
            }
        }

        public Result<Pedido> GetPedidoById(int id)
        {
            try
            {
                var pedido = _context.Pedidos
                                     .Include(p => p.Itens)
                                     .FirstOrDefault(p => p.CodigoPedido == id);

                if (pedido == null)
                {
                    return Result<Pedido>.Failure("Pedido não encontrado.");
                }

                return Result<Pedido>.Success(pedido);
            }
            catch (Exception ex)
            {
                return Result<Pedido>.Failure(ex.Message);
            }
        }

        public Result<ItemPedido> AdicionarItemAoPedido(int pedidoId, ItemPedido novoItem)
        {
            try
            {
                var pedido = _context.Pedidos.Include(p => p.Itens)
                                             .FirstOrDefault(p => p.CodigoPedido == pedidoId);

                if (pedido == null)
                {
                    return Result<ItemPedido>.Failure("Pedido não encontrado.");
                }

                pedido.Itens.Add(novoItem);
                _context.SaveChanges();

                return Result<ItemPedido>.Success(novoItem);
            }
            catch (Exception ex)
            {
                return Result<ItemPedido>.Failure(ex.Message);
            }
        }

        public Result<decimal> GetValorTotalPedido(int pedidoId)
        {
            var pedido = _context.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.CodigoPedido == pedidoId);
            return pedido != null ? Result<decimal>.Success(pedido.CalcularValorTotal())
                                  : Result<decimal>.Failure("Pedido não encontrado.");
        }

        public Result<int> GetQuantidadePedidosPorCliente(int clienteId)
        {
            var quantidade = _context.Pedidos.Count(p => p.CodigoCliente == clienteId);
            return Result<int>.Success(quantidade);
        }

        public Result<List<Pedido>> GetPedidosPorCliente(int clienteId)
        {
            var pedidos = _context.Pedidos
                                  .Where(p => p.CodigoCliente == clienteId)
                                  .Include(p => p.Itens)
                                  .ToList();
            return Result<List<Pedido>>.Success(pedidos);
        }

    }
}
