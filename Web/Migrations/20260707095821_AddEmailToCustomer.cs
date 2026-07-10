using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("c3558c41-7ea5-4109-b4b4-de043a5adf3a"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("db9ae35e-1c22-48be-a8cb-ea9d0b3f8e31"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 7, 16, 58, 20, 961, DateTimeKind.Local).AddTicks(6641));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 7, 16, 58, 20, 961, DateTimeKind.Local).AddTicks(6652));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 7, 16, 58, 20, 961, DateTimeKind.Local).AddTicks(6646));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 7, 16, 58, 20, 961, DateTimeKind.Local).AddTicks(6649));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 7, 16, 58, 20, 961, DateTimeKind.Local).AddTicks(6643));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 7, 16, 58, 20, 961, DateTimeKind.Local).AddTicks(6599));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("db9ae35e-1c22-48be-a8cb-ea9d0b3f8e31"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("c3558c41-7ea5-4109-b4b4-de043a5adf3a"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 12, 53, 47, 905, DateTimeKind.Local).AddTicks(5797));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 12, 53, 47, 905, DateTimeKind.Local).AddTicks(5811));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 12, 53, 47, 905, DateTimeKind.Local).AddTicks(5805));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 12, 53, 47, 905, DateTimeKind.Local).AddTicks(5808));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 12, 53, 47, 905, DateTimeKind.Local).AddTicks(5801));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 12, 53, 47, 905, DateTimeKind.Local).AddTicks(5721));
        }
    }
}
