using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddOrderAndElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Order_Id = table.Column<Guid>(nullable: false),
                    Item_Id = table.Column<Guid>(nullable: false),
                    Items_Count = table.Column<int>(nullable: false),
                    Item_Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Customer_id = table.Column<Guid>(nullable: false),
                    Order_Date = table.Column<DateTime>(nullable: false),
                    Shipment_Date = table.Column<DateTime>(nullable: false),
                    Order_Number = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderElements");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
