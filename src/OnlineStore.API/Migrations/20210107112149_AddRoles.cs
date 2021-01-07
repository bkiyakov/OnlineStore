using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.API.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9d0f5c0-ffd0-46be-9e51-ac690c4d7468", "2dbe5a42-567d-43d6-acc9-468270e5cfce", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97a96588-9230-4504-997e-d2c1ab1da6a5", "e608cb77-c9d0-41b2-90cc-868207846d8b", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97a96588-9230-4504-997e-d2c1ab1da6a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9d0f5c0-ffd0-46be-9e51-ac690c4d7468");
        }
    }
}
