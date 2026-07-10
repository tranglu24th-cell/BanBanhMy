using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class s13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("ba6106f7-667a-4a55-9e18-b0333a9e1d21"));

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "AdminReply",
            //    table: "Contacts",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "RepliedAt",
            //    table: "Contacts",
            //    type: "datetime2",
            //    nullable: true);

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("bdfd8f24-9050-4a9a-87b6-0a949480cf96"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 4, 21, 43, 55, 44, DateTimeKind.Local).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 4, 21, 43, 55, 44, DateTimeKind.Local).AddTicks(6987));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 4, 21, 43, 55, 44, DateTimeKind.Local).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 4, 21, 43, 55, 44, DateTimeKind.Local).AddTicks(6984));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 4, 21, 43, 55, 44, DateTimeKind.Local).AddTicks(6976));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 4, 21, 43, 55, 44, DateTimeKind.Local).AddTicks(6917));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("bdfd8f24-9050-4a9a-87b6-0a949480cf96"));

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AdminReply",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "RepliedAt",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("ba6106f7-667a-4a55-9e18-b0333a9e1d21"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 3, 20, 45, 19, 57, DateTimeKind.Local).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 3, 20, 45, 19, 57, DateTimeKind.Local).AddTicks(7259));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 3, 20, 45, 19, 57, DateTimeKind.Local).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 3, 20, 45, 19, 57, DateTimeKind.Local).AddTicks(7256));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 3, 20, 45, 19, 57, DateTimeKind.Local).AddTicks(7216));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 3, 20, 45, 19, 57, DateTimeKind.Local).AddTicks(7149));
        }
    }
}
