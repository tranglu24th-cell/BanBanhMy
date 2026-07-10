using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("ffb2aefc-12c4-46b3-86b5-a9b7a6169f71"));

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminReply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepliedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("c3558c41-7ea5-4109-b4b4-de043a5adf3a"));

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("ffb2aefc-12c4-46b3-86b5-a9b7a6169f71"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 0, 19, 22, 412, DateTimeKind.Local).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 0, 19, 22, 412, DateTimeKind.Local).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 0, 19, 22, 412, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 0, 19, 22, 412, DateTimeKind.Local).AddTicks(1152));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 0, 19, 22, 412, DateTimeKind.Local).AddTicks(1146));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"),
                column: "CreatedOn",
                value: new DateTime(2026, 7, 5, 0, 19, 22, 412, DateTimeKind.Local).AddTicks(1095));
        }
    }
}
