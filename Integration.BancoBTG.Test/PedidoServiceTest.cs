using Xunit;
using Moq;
using Integration.BancoBTG.Service.Data;
using Integration.BancoBTG.Service.Services;
using Integration.BancoBTG.Service.Models;

namespace Integration.BancoBTG.Test
{
    public class PedidoServiceTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly PedidoService _pedidoService;

        public PedidoServiceTest()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _pedidoService = new PedidoService(_mockContext.Object);
        }

        [Fact]
        public void GetAllPedidos_ShouldReturnPedidos_WhenPedidosExist()
        {
            var mockContext = new Mock<ApplicationDbContext>();

            var service = new PedidoService(mockContext.Object);

            var result = service.GetAllPedidos();

            Assert.NotNull(result);
            
        }

        [Fact]
        public void GetAllPedidos_ShouldReturnAllPedidos()
        {
            
            var pedidos = new List<Pedido>
            {
                new Pedido { CodigoPedido = 1, CodigoCliente = 1 },
                new Pedido { CodigoPedido = 2, CodigoCliente = 2 }
            };

            _mockContext.Setup(c => c.Pedidos).ReturnsDbSet(pedidos);

            var result = _pedidoService.GetAllPedidos();

            Assert.Equal(2, result.Value.Count());
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetPedidoById_ExistingId_ShouldReturnPedido()
        {
            
            var pedidos = new List<Pedido>
            {
                new Pedido { CodigoPedido = 1, CodigoCliente = 1 },
                new Pedido { CodigoPedido = 2, CodigoCliente = 2 }
            };

            _mockContext.Setup(c => c.Pedidos).ReturnsDbSet(pedidos);

            
            var result = _pedidoService.GetPedidoById(1);

            
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.CodigoPedido);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetPedidoById_NonExistingId_ShouldReturnFailure()
        {
            
            var pedidos = new List<Pedido>
            {
                new Pedido { CodigoPedido = 1, CodigoCliente = 1 },
                new Pedido { CodigoPedido = 2, CodigoCliente = 2 }
            };

            _mockContext.Setup(c => c.Pedidos).ReturnsDbSet(pedidos);

            
            var result = _pedidoService.GetPedidoById(3);

            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
        }

        [Fact]
        public void AdicionarItemAoPedido_ValidData_ShouldAddItem()
        {
            
            var pedidos = new List<Pedido>
            {
                new Pedido { CodigoPedido = 1, CodigoCliente = 1, Itens = new List<ItemPedido>() }
            };

            _mockContext.Setup(c => c.Pedidos).ReturnsDbSet(pedidos);

            var novoItem = new ItemPedido { Produto = "Caneta", Quantidade = 10, PrecoUnitario = 1.5M };

            
            var result = _pedidoService.AdicionarItemAoPedido(1, novoItem);

            Assert.True(result.IsSuccess);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }
    }
}