using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KQF.Floor.Web.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionOrderNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContainerNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentItemNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ItemNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContainerNumber",
                table: "Transactions",
                column: "ContainerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IsPosted",
                table: "Transactions",
                column: "IsPosted");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ItemNumber",
                table: "Transactions",
                column: "ItemNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ParentItemNumber",
                table: "Transactions",
                column: "ParentItemNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProductionOrderNumber",
                table: "Transactions",
                column: "ProductionOrderNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
