using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibaket.Ifra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductId1",
                table: "ProductGalleries");

            migrationBuilder.DropIndex(
                name: "IX_ProductGalleries_ProductId1",
                table: "ProductGalleries");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductGalleries");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductGalleries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TagTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductGalleries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductGalleries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductId1",
                table: "ProductGalleries",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductId1",
                table: "ProductGalleries",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
