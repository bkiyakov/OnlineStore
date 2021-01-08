using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.API.Migrations
{
    public partial class AddInitUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97a96588-9230-4504-997e-d2c1ab1da6a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9d0f5c0-ffd0-46be-9e51-ac690c4d7468");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a1a111a-1a1a-223b-33bb-444c56df7890", "c6eef3a5-e417-4e0c-9a0c-337d58c6f45a", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8dfa9978-492a-4365-b1f7-d322120c0eb3", "06363510-2f79-4c6b-9d1b-668750c541d7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1a1d111m-1i1n-223b-33bb-444c56df7890", 0, "ac386368-782d-42fc-bf3d-39d8363ed431", "manager@onlinestore.com", true, null, null, false, null, "manager@onlinestore.com", "manager@onlinestore.com", "AQAAAAEAACcQAAAAEBxb7eo3cLyOhbGp6GgE56SJ8sRqY5mQT8e3Ph10J1wLWlfaEc8nDwTZsLE9AEwXDA==", null, false, "eeb0e2db-d5c8-4e02-bac8-6a07f0febd19", false, "manager@onlinestore.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1a1d111m-1i1n-223b-33bb-444c56df7890", "1a1a111a-1a1a-223b-33bb-444c56df7890" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dfa9978-492a-4365-b1f7-d322120c0eb3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1a1d111m-1i1n-223b-33bb-444c56df7890", "1a1a111a-1a1a-223b-33bb-444c56df7890" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a1a111a-1a1a-223b-33bb-444c56df7890");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a1d111m-1i1n-223b-33bb-444c56df7890");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9d0f5c0-ffd0-46be-9e51-ac690c4d7468", "2dbe5a42-567d-43d6-acc9-468270e5cfce", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97a96588-9230-4504-997e-d2c1ab1da6a5", "e608cb77-c9d0-41b2-90cc-868207846d8b", "User", "USER" });
        }
    }
}
