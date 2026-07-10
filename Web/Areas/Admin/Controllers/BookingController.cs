using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class BookingController : Controller
    {
        private readonly Web.Models.EF.FoodContext _dbContext;

        public BookingController(Web.Models.EF.FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        // URL tải trang: /Admin/Booking hoặc /Admin/Booking/Index
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            // Đọc dữ liệu thực tế từ bảng Booking trong SQL Server
            var list = _dbContext.Bookings.OrderByDescending(b => b.CreatedAt).ToList();
            return View(list);
        }

        // URL xử lý form: /Admin/Booking/Reply
        [HttpPost("Reply")]
        [ValidateAntiForgeryToken]
        public IActionResult Reply(int bookingId, string adminComment, string newStatus)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking != null)
            {
                // Cập nhật giá trị thay đổi từ Admin gửi lên
                booking.AdminReply = adminComment;
                booking.Status = newStatus;

                // Lưu thay đổi cứng xuống Database
                _dbContext.SaveChanges();

                TempData["Message"] = "Cập nhật phản hồi và trạng thái đơn thành công!";
            }
            // Sau khi lưu xong, ép hệ thống tải lại trang danh sách Admin
            return RedirectToAction("Index");
        }
        // URL xử lý xóa: /Admin/Booking/Delete
        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Tìm kiếm bản ghi lịch đặt theo ID trong cơ sở dữ liệu
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.Id == id);

            if (booking != null)
            {
                _dbContext.Bookings.Remove(booking); // Thao tác xóa bản ghi ra khỏi DbSet
                _dbContext.SaveChanges();           // Thực thi lưu thay đổi và xóa hẳn trong SQL Server

                TempData["Message"] = "Đã xóa lịch đặt bàn thành công khỏi hệ thống!";
            }
            else
            {
                TempData["Message"] = "Không tìm thấy dữ liệu cần xóa!";
            }

            // Điều hướng quay trở lại trang danh sách quản lý
            return RedirectToAction("Index");
        }
    }
}