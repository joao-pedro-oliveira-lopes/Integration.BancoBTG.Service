using Microsoft.EntityFrameworkCore;
using Integration.BancoBTG.Service.Models;

namespace Integration.BancoBTG.Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Username = "usuarioTeste",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("SenhaSegura123")
            });

            modelBuilder.Entity<Pedido>()
                .HasKey(p => p.CodigoPedido);

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    ClienteId = 100,
                    Nome = "Cliente Padrão"
                }
            );
        }
    }
}

