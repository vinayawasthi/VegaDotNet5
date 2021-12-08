using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Vega.Web.Migrations
{
    public partial class seeding_make_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 100; i++)
            {
                migrationBuilder.Sql(string.Format("INSERT INTO Makes (Name) VALUES ('Make{0}')", i));
            }

            for (int i = 1; i <= 100; i++)
            {
                migrationBuilder.Sql(string.Format("INSERT INTO Models (Name, MakeId) VALUES ('Make{0}-ModelA', (SELECT Id FROM Makes WHERE Name = 'Make{0}' LIMIT 1 OFFSET 0))", i));
                migrationBuilder.Sql(string.Format("INSERT INTO Models (Name, MakeId) VALUES ('Make{0}-ModelB', (SELECT Id FROM Makes WHERE Name = 'Make{0}' LIMIT 1 OFFSET 0))", i));
                migrationBuilder.Sql(string.Format("INSERT INTO Models (Name, MakeId) VALUES ('Make{0}-ModelC', (SELECT Id FROM Makes WHERE Name = 'Make{0}' LIMIT 1 OFFSET 0))", i));
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM makes WHERE Name like 'Make%'");
        }
    }
}
