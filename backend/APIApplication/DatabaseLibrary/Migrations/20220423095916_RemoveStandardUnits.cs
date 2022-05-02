using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiApplication.Migrations
{
    public partial class RemoveStandardUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardUnits",
                table: "ProductStandardNames");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StandardUnits",
                table: "ProductStandardNames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
