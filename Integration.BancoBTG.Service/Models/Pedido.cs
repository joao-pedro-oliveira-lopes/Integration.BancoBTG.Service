using System.ComponentModel.DataAnnotations;

namespace Integration.BancoBTG.Service.Models
{
    public class Pedido
    {

        [Required]
        public int CodigoPedido { get; set; }

        [Required]
        public int CodigoCliente { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "O pedido deve ter pelo menos um item.")]
        public List<ItemPedido> Itens { get; set; }
        public decimal CalcularValorTotal()
        {
            return Itens?.Sum(i => i.Quantidade * i.PrecoUnitario) ?? 0;
        }
    }
}

