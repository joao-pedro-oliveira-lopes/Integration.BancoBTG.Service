﻿// <auto-generated />
using System;
using Integration.BancoBTG.Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Integration.BancoBTG.Service.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240118104251_LastMigration")]
    partial class LastMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");

                    b.HasData(
                        new
                        {
                            ClienteId = 100,
                            Nome = "Cliente Padrão"
                        });
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.ItemPedido", b =>
                {
                    b.Property<int>("ItemPedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ItemPedidoId"));

                    b.Property<int>("PedidoId")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("numeric");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.HasKey("ItemPedidoId");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItensPedido");
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.Pedido", b =>
                {
                    b.Property<int>("CodigoPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CodigoPedido"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<int>("CodigoCliente")
                        .HasColumnType("integer");

                    b.HasKey("CodigoPedido");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PasswordHash = "$2a$11$vgaG/tNOmJBzwR/Gadv.Duqsgfd1fJ0f3COAH7rgv5iLT6xWkdmY2",
                            Username = "usuarioTeste"
                        });
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.ItemPedido", b =>
                {
                    b.HasOne("Integration.BancoBTG.Service.Models.Pedido", "Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.Pedido", b =>
                {
                    b.HasOne("Integration.BancoBTG.Service.Models.Cliente", null)
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Integration.BancoBTG.Service.Models.Pedido", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
