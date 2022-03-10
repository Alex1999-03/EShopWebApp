using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopWebApp.Data.Migrations
{
    public partial class user_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "57d7329f-ffe5-4725-ba6a-618c37f10574", "62a2f4a7-3b20-450a-baed-7d30e6edbec4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "196a09a8-c6ce-48a3-81f9-eaa5a5d34006", "729f03eb-6933-4fb7-8108-420bedd62e35" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "196a09a8-c6ce-48a3-81f9-eaa5a5d34006");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57d7329f-ffe5-4725-ba6a-618c37f10574");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a2f4a7-3b20-450a-baed-7d30e6edbec4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "729f03eb-6933-4fb7-8108-420bedd62e35");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c08729c-2b80-4c18-81c4-71b85667840b", "864999b6-70b9-4d53-9525-0d688d528ad2", "Buyer", "BUYER" },
                    { "69dd1a05-4cc0-4036-81f1-7a47a7063ad6", "361a76d0-2074-459d-a5b4-6745029e615e", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "58a49be7-0cae-4828-8e78-6b163099ba5b", 0, "74d6457d-7130-4383-b9ca-4817760add95", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEPPfrKmF2Drkp1y9byytxKOr1O3BlZu6jHc+PUv+3X9z3SFwcQrckYzuE8JN2JiqHg==", null, false, "01cae1a9-0efc-4b66-9e2a-dddeef810a44", false, "admin@gmail.com" },
                    { "94352f58-f338-43a1-a334-9bd1f77e1501", 0, "306172a6-c8be-44bd-937f-fb6bd9bc6dca", "buyer@gmail.com", true, false, null, "BUYER@GMAIL.COM", "BUYER@GMAIL.COM", "AQAAAAEAACcQAAAAELw5TqdU091Wem5rJ5fQOuM2qWRN353DMzudOSX7gotK3FT+FVIvDogb41fIYSUU2g==", null, false, "eca459b0-b339-4eb8-9039-609039be6d93", false, "buyer@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "69dd1a05-4cc0-4036-81f1-7a47a7063ad6", "58a49be7-0cae-4828-8e78-6b163099ba5b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4c08729c-2b80-4c18-81c4-71b85667840b", "94352f58-f338-43a1-a334-9bd1f77e1501" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "69dd1a05-4cc0-4036-81f1-7a47a7063ad6", "58a49be7-0cae-4828-8e78-6b163099ba5b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4c08729c-2b80-4c18-81c4-71b85667840b", "94352f58-f338-43a1-a334-9bd1f77e1501" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c08729c-2b80-4c18-81c4-71b85667840b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69dd1a05-4cc0-4036-81f1-7a47a7063ad6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58a49be7-0cae-4828-8e78-6b163099ba5b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94352f58-f338-43a1-a334-9bd1f77e1501");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "196a09a8-c6ce-48a3-81f9-eaa5a5d34006", "e94ae352-573b-4c14-8b7d-2b49110ea820", "Buyer", "BUYER" },
                    { "57d7329f-ffe5-4725-ba6a-618c37f10574", "03d1c0a8-f03a-4dba-8406-3367f47416f7", "Buyer", "BUYER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "62a2f4a7-3b20-450a-baed-7d30e6edbec4", 0, "d5814f2c-2c11-4326-a11a-71abd2e5ac77", "buyer@gmail.com", true, false, null, "BUYER@GMAIL.COM", "BUYER@GMAIL.COM", "AQAAAAEAACcQAAAAEAKpLNu2dWzq8m2sMeLOnKYsfUFvIH1m8alb4Ls9wmiWbzHoNz6P2JrGS8Lx0QE9xw==", null, false, "87752356-18cc-4561-965e-dd6b9df97a80", false, "buyer@gmail.com" },
                    { "729f03eb-6933-4fb7-8108-420bedd62e35", 0, "c57bec2a-d3cb-4f2a-bb78-9003b70faf9f", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEJ4+NnRjCHNQ4VrfokrtgDLUD81XalHZ0To+Sf9BCaGqQAaIcoCC3nbMGG1gX8dEoA==", null, false, "098ec859-321c-4e2a-8ad0-fef782b2f08f", false, "admin@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "57d7329f-ffe5-4725-ba6a-618c37f10574", "62a2f4a7-3b20-450a-baed-7d30e6edbec4" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "196a09a8-c6ce-48a3-81f9-eaa5a5d34006", "729f03eb-6933-4fb7-8108-420bedd62e35" });
        }
    }
}
