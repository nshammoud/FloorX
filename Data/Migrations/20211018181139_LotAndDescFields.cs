using Microsoft.EntityFrameworkCore.Migrations;

namespace KQF.Floor.Web.Data.Migrations
{
    public partial class LotAndDescFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemDescription",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LotNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemDescription",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LotNumber",
                table: "Transactions");
        }
    }
}
