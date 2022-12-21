using Microsoft.EntityFrameworkCore.Migrations;

namespace KQF.Floor.Web.Data.Migrations
{
    public partial class RunIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RunNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("update Transactions set RunNumber =  productionordernumber + mixerid +  format( dateposted, 'yyyyMMddHHmmss')  where isposted = 1;");
            migrationBuilder.Sql("update Transactions set RunNumber =  productionordernumber +  mixerid + format( dateposted, 'yyyyMMdd') + '000000'  where isposted = 0;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RunNumber",
                table: "Transactions");
        }
    }
}
