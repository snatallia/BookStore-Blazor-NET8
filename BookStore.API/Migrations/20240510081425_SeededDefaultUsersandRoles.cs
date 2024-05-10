using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c408cd7-1b91-4165-a61c-d62de2a7fe42", null, "Administrator", "ADMINISTRATOR" },
                    { "e70fb67b-4e70-4cc0-a321-a98f65fc18df", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "288622b7-5d7f-46e5-a0c7-ef27c344baed", 0, "1bdd9716-0cc4-44db-aaa6-0339f5072289", "admin@bookstore.com", false, "Jon", "Smith", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAELpoV53qYbhF7578ZRLsiKkP5vN2B3AYhjjdEuSWKUfSSzgA99M/MUhpp3Isv+ncww==", null, false, "c5b359b6-297a-4a44-bfe3-aac7731c29c4", false, "admin@bookstore.com" },
                    { "4e29398d-410f-449d-a4da-d811a3727a02", 0, "c06510df-8965-43b0-a1b6-298b10d3ce1a", "user@bookstore.com", false, "Basia", "Nowak", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEB1vI7v5HhjD4jfgz39rp1CXqouhtd+YNUhl74LyGLtY4xYEeSo0/ifZpcYErd/kyg==", null, false, "ece430b7-6284-42cf-bb53-aad1d6839d10", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6c408cd7-1b91-4165-a61c-d62de2a7fe42", "288622b7-5d7f-46e5-a0c7-ef27c344baed" },
                    { "e70fb67b-4e70-4cc0-a321-a98f65fc18df", "4e29398d-410f-449d-a4da-d811a3727a02" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6c408cd7-1b91-4165-a61c-d62de2a7fe42", "288622b7-5d7f-46e5-a0c7-ef27c344baed" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e70fb67b-4e70-4cc0-a321-a98f65fc18df", "4e29398d-410f-449d-a4da-d811a3727a02" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c408cd7-1b91-4165-a61c-d62de2a7fe42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e70fb67b-4e70-4cc0-a321-a98f65fc18df");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "288622b7-5d7f-46e5-a0c7-ef27c344baed");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e29398d-410f-449d-a4da-d811a3727a02");
        }
    }
}
