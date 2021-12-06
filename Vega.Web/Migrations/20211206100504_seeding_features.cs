using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Web.Migrations
{
    public partial class seeding_features : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 20; i++)
            {
                migrationBuilder.Sql(string.Format("INSERT INTO features (Name) VALUES ('Feature{0}')", i));
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM features WHERE Name like 'Feature%'");
        }
    }
}
