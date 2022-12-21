using Microsoft.EntityFrameworkCore.Migrations;

namespace KQF.Floor.Web.Data.Migrations
{
    public partial class LineToTx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MixerId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MixerId",
                table: "Transactions");
        }
    }
}
