using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class s2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "ParentId" },
                values: new object[] { new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"), new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"), new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4357), null, null, "Root", null });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), "Quản trị viên" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"), new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"), new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4421), null, null, "Product", new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385") },
                    { new Guid("86700b84-de54-426a-9748-da1bce88e424"), new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"), new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4413), null, null, "Article", new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385") },
                    { new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"), new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"), new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4362), null, null, "Authorized", new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385") }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Email", "GroupId", "LastLogin", "LoginName", "ModifiedBy", "ModifiedOn", "Name", "Password", "Picture" },
                values: new object[] { new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"), null, new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4309), "tranglu.24th@sv.dla.edu.vn", new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), null, "tranglu", null, null, "Lâm Uyên Trang", "c4ca4238a0b923820dcc509a6f75849b", "/img/users/uyentrang.jpg" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "ParentId" },
                values: new object[] { new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"), new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"), new DateTime(2026, 6, 16, 19, 28, 30, 499, DateTimeKind.Local).AddTicks(4371), null, null, "Nhóm quyền", new Guid("d951dd74-a153-408c-ab60-44e51bb51f47") });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CategoryId", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("1c229f8c-8d58-4de2-b7a5-96dde1bb9263"), new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"), "edit-group", "Cập nhật" },
                    { new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa"), new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"), "view-groups", "Xem danh sách" }
                });

            migrationBuilder.InsertData(
                table: "Authorized",
                columns: new[] { "Id", "GroupId", "RoleId" },
                values: new object[] { new Guid("39e6f620-e903-4889-bbe2-48d9c33537ff"), new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"), new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorized",
                keyColumn: "Id",
                keyValue: new Guid("39e6f620-e903-4889-bbe2-48d9c33537ff"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b04bd80-c414-4836-ac8c-ca215b574f41"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86700b84-de54-426a-9748-da1bce88e424"));

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("fd48367d-f4a1-4e0b-a1f6-9d72afcebcc9"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("1c229f8c-8d58-4de2-b7a5-96dde1bb9263"));

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("d0cfdf00-afc9-4567-a9ec-0f0db44a18bd"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b5f8852e-1a0f-4fee-a821-1c982c33f9aa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d373c3-dcf6-4634-8a46-7700b82dbe4d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d951dd74-a153-408c-ab60-44e51bb51f47"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ea90800-ec76-4e3c-83f5-0d7446510385"));
        }
    }
}
