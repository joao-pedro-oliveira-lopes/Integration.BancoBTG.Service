using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integration.BancoBTG.Service.Migrations
{
    public partial class LastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Nome" },
                values: new object[] { 100, "Cliente Padrão" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$vgaG/tNOmJBzwR/Gadv.Duqsgfd1fJ0f3COAH7rgv5iLT6xWkdmY2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 100);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$kYtwK0NVQAeVH.XeZLr4/u/KUxp/e2OyMaB1PQvCHKF8Y5YzByJii");
        }
    }
}
