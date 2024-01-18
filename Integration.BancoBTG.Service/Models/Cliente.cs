namespace Integration.BancoBTG.Service.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}
