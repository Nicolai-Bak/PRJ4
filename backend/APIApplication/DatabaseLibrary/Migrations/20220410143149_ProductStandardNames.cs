using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class ProductStandardNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductStandardNames",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeasureG = table.Column<bool>(type: "bit", nullable: false),
                    MeasureL = table.Column<bool>(type: "bit", nullable: false),
                    MeasureStk = table.Column<bool>(type: "bit", nullable: false),
                    Organic = table.Column<bool>(type: "bit", nullable: false),
                    StandardUnits = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStandardNames", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStandardNames");
        }
    }
}
