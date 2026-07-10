using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class s3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("39e6f620-e903-4889-bbe2-48d9c33537ff"));

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("d1f23f1b-5b2a-4b59-88cc-2d04533a4967"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 22, 8, 17, 304, DateTimeKind.Local).AddTicks(7020));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 22, 8, 17, 304, DateTimeKind.Local).AddTicks(7033));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 22, 8, 17, 304, DateTimeKind.Local).AddTicks(7027));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 22, 8, 17, 304, DateTimeKind.Local).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 22, 8, 17, 304, DateTimeKind.Local).AddTicks(7023));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 22, 8, 17, 304, DateTimeKind.Local).AddTicks(6958));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CategoryId", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("70b8d810-029d-438f-9b41-f5ed8cbd7a31"), new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"), "delete-group", "Xóa" },
                    { new Guid("a03ba290-d4a5-45cd-a255-be0a8ed3851d"), new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"), "save-group", "Lưu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("d1f23f1b-5b2a-4b59-88cc-2d04533a4967"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("70b8d810-029d-438f-9b41-f5ed8cbd7a31"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a03ba290-d4a5-45cd-a255-be0a8ed3851d"));

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("39e6f620-e903-4889-bbe2-48d9c33537ff"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4371));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4362));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4309));
        }
    }
}
