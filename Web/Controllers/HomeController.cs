using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.EF;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoodContext _dbContext;
        public HomeController(FoodContext dbContext)
        { 
            _dbContext = dbContext;
        }


        public async Task<IActionResult> Index()
        {
            // 1. Phần lấy danh mục sản phẩm hiện tại của bạn (Dành cho Client)
            var adminCategoryNames = new List<string> { "Root", "Authorized", "Nhóm quyền", "Product", "Article" };

            ViewBag.Categories = await _dbContext.Categories
                .Where(c => !adminCategoryNames.Contains(c.Name))
                .ToListAsync();


            // 2. CHÈN THÊM DÒNG NÀY VÀO ĐÂY ĐỂ LẤY 3 BÀI VIẾT CHO CLIENT XEM
            ViewBag.LatestBlogs = await _dbContext.Articles
                .OrderByDescending(a => a.CreatedOn)
                .Take(3)
                .ToListAsync();


            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Booking()
        {
            // Lấy danh sách lịch đặt đã được duyệt hoặc tất cả để hiển thị công khai ngoài trang web
            var approvedBookings = _dbContext.Bookings
                                          .OrderByDescending(b => b.CreatedAt)
                                          .ToList();

            ViewBag.ApprovedBookings = approvedBookings;
            return View();
        }

        // 4. Hàm xử lý Nhận thông tin từ Form gửi lên và lưu cứng vào DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking model)
        {
            if (ModelState.IsValid)
            {
                // Gán các giá trị mặc định cho đơn mới
                model.CreatedAt = DateTime.Now;
                model.Status = "Chờ duyệt";

                // Lệnh lưu trực tiếp vào bảng Bookings trong Database
                _dbContext.Bookings.Add(model);
                _dbContext.SaveChanges(); // Xác nhận lưu thay đổi xuống SQL

                TempData["SuccessMessage"] = "Đặt lịch thành công! Vui lòng đợi Admin duyệt và phản hồi.";
                return RedirectToAction("Booking");
            }

            // Nếu form lỗi, lấy lại danh sách cũ để hiển thị lại khung bên phải không bị trống
            ViewBag.ApprovedBookings = _dbContext.Bookings.OrderByDescending(b => b.CreatedAt).ToList();
            return View("Booking", model);
        }
        // ==================== KHU VỰC DÀNH CHO ADMIN ====================

        // 1. Trang hiển thị danh sách tất cả các lịch đặt bánh của khách gửi về
        [HttpGet]
        public IActionResult AdminBookingList()
        {
            // Lấy toàn bộ danh sách đặt lịch từ Database, đơn mới nhất xếp lên đầu
            var allBookings = _dbContext.Bookings.OrderByDescending(b => b.CreatedAt).ToList();

            // Trả dữ liệu ra cho giao diện Admin xem
            return View(allBookings);
        }

        // 2. Xử lý khi Admin gõ câu trả lời và bấm nút "Cập nhật / Phản hồi"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminReplyBooking(int bookingId, string adminComment, string newStatus)
        {
            // Tìm đúng đơn đặt lịch của khách trong Database dựa vào Id
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking != null)
            {
                booking.AdminReply = adminComment;    // Lưu câu trả lời của bạn
                booking.Status = newStatus;            // Đổi trạng thái (Đã xác nhận / Từ chối)

                _dbContext.SaveChanges();              // Lưu trực tiếp xuống Database
                TempData["AdminSuccess"] = "Cập nhật trạng thái đơn thành công!";
            }

            return RedirectToAction("AdminBookingList");
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

       

        public IActionResult Error404()
        {
            return View();
        }


    }
}
