using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShop.DAL.Migrations
{
    public partial class discountAndSerialnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscountModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinimumPurchase = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<double>(type: "float", nullable: false),
                    ProductModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountModel_Products_ProductModelId",
                        column: x => x.ProductModelId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountModel_ProductModelId",
                table: "DiscountModel",
                column: "ProductModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountModel");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Products");
        }
    }
}
