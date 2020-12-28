using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddOrderAndOrderElement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "OrderElements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderElements_Order_Id",
                table: "OrderElements",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderElements_ProductId",
                table: "OrderElements",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Orders_Order_Id",
                table: "OrderElements",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Orders_Order_Id",
                table: "OrderElements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderElements_Products_ProductId",
                table: "OrderElements");

            migrationBuilder.DropIndex(
                name: "IX_OrderElements_Order_Id",
                table: "OrderElements");

            migrationBuilder.DropIndex(
                name: "IX_OrderElements_ProductId",
                table: "OrderElements");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderElements");
        }
    }
}
