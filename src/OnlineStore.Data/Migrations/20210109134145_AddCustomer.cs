using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements");

            migrationBuilder.DropIndex(
                name: "IX_OrderElements_ProductId",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderElements");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Discount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderElements_Item_Id",
                table: "OrderElements",
                column: "Item_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Products_Item_Id",
                table: "OrderElements",
                column: "Item_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Products_Item_Id",
                table: "OrderElements");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_OrderElements_Item_Id",
                table: "OrderElements");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "OrderElements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderElements_ProductId",
                table: "OrderElements",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
