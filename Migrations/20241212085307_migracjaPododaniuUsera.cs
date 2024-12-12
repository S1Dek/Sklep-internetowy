using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy.Migrations
{
    /// <inheritdoc />
    public partial class migracjaPododaniuUsera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditUserViewModelId",
                table: "AspNetRoles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditUserViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedRoles = table.Column<string>(type: "TEXT", nullable: false),
                    SelectedRoles = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditUserViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditUserViewModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_EditUserViewModelId",
                table: "AspNetRoles",
                column: "EditUserViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EditUserViewModel_UserId",
                table: "EditUserViewModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_EditUserViewModel_EditUserViewModelId",
                table: "AspNetRoles",
                column: "EditUserViewModelId",
                principalTable: "EditUserViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_EditUserViewModel_EditUserViewModelId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EditUserViewModel");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_EditUserViewModelId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "EditUserViewModelId",
                table: "AspNetRoles");
        }
    }
}
