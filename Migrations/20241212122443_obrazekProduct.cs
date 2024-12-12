using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy.Migrations
{
    /// <inheritdoc />
    public partial class obrazekProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "Description");
        }
    }
}
