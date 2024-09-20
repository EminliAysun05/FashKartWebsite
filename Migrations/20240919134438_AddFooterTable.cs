using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashKartWebsite.Migrations
{
    public partial class AddFooterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Footers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FbLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GgLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Footers");
        }
    }
}
