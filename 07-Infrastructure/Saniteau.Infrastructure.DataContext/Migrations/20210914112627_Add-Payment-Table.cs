using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saniteau.Infrastructure.DataContext.Migrations
{
    public partial class AddPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFacturation = table.Column<int>(type: "int", nullable: false),
                    PaypalOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatut = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_Payments_Facturations_IdFacturation",
                        column: x => x.IdFacturation,
                        principalTable: "Facturations",
                        principalColumn: "IdFacturation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdFacturation",
                table: "Payments",
                column: "IdFacturation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
