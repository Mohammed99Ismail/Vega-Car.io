using Microsoft.EntityFrameworkCore.Migrations;

namespace VEGA.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make1'),('Make2'),('Make3');");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-MoedelA',(SELECT ID FROM MAKES WHERE Name='Make1')),('Make1-MoedelB',(SELECT ID FROM MAKES WHERE Name='Make1')),('Make1-MoedelC',(SELECT ID FROM MAKES WHERE Name='Make1'));");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make2-MoedelA',(SELECT ID FROM MAKES WHERE Name='Make2')),('Make2-MoedelB',(SELECT ID FROM MAKES WHERE Name='Make2')),('Make2-MoedelC',(SELECT ID FROM MAKES WHERE Name='Make2'));");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make3-MoedelA',(SELECT ID FROM MAKES WHERE Name='Make3')),('Make3-MoedelB',(SELECT ID FROM MAKES WHERE Name='Make3')),('Make3-MoedelC',(SELECT ID FROM MAKES WHERE Name='Make3'));");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes");
            migrationBuilder.Sql("DELETE FROM Models");
        }
    }
}
