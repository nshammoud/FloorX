using Microsoft.EntityFrameworkCore.Migrations;

namespace KQF.Floor.Web.Data.Migrations
{
    public partial class IsWaste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWaste",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWaste",
                table: "Transactions");
        }
    }
}
