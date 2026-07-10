using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class s4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("d1f23f1b-5b2a-4b59-88cc-2d04533a4967"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("a2fda332-14ae-404f-839b-df07b181b388"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 17, 16, 34, 24, 50, DateTimeKind.Local).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 17, 16, 34, 24, 50, DateTimeKind.Local).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 17, 16, 34, 24, 50, DateTimeKind.Local).AddTicks(3107));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 17, 16, 34, 24, 50, DateTimeKind.Local).AddTicks(3113));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 17, 16, 34, 24, 50, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 6, 17, 16, 34, 24, 50, DateTimeKind.Local).AddTicks(3032));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("a2fda332-14ae-404f-839b-df07b181b388"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Product");

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
        }
    }
}
