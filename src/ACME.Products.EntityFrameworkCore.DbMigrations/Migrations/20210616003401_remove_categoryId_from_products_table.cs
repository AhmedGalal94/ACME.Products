using Microsoft.EntityFrameworkCore.Migrations;

namespace ACME.Products.Migrations
{
    public partial class remove_categoryId_from_products_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "AppProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
