using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy.Migrations
{
    /// <inheritdoc />
    public partial class MigracjaStatystyk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MostOrderedProductViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MostOrderedProductViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MostOrderedProductViewModel_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatisticsViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalOrders = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "TEXT", nullable: false),
                    MostOrderedProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatisticsViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatisticsViewModel_MostOrderedProductViewModel_MostOrderedProductId",
                        column: x => x.MostOrderedProductId,
                        principalTable: "MostOrderedProductViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MostOrderedProductViewModel_ProductId",
                table: "MostOrderedProductViewModel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatisticsViewModel_MostOrderedProductId",
                table: "OrderStatisticsViewModel",
                column: "MostOrderedProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStatisticsViewModel");

            migrationBuilder.DropTable(
                name: "MostOrderedProductViewModel");
        }
    }
}
