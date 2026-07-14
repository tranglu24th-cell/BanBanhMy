using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailCreatedOnToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Bổ sung 2 cột cho bảng Customer (đang dùng chung cho khách hàng đặt hàng và khách hàng tự đăng ký):
            // - Email: dùng để đăng ký / đăng nhập, phải là duy nhất.
            // - CreatedOn: ngày tạo tài khoản (NgayTao).
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(256)", // <-- ĐÃ SỬA: Thay nvarchar(max) thành nvarchar(256) ở đây
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Customer",
                type: "datetime2",
                nullable: true);

            // Tạo unique index cho Email để đảm bảo không đăng ký trùng (bỏ qua các dòng NULL đã tồn tại trước đó).
            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email",
                table: "Customer",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_Email",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Customer");
        }
    }
}