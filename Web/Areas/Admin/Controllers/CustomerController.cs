using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Models.EF;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Quản lý danh sách Khách hàng đã tự đăng ký tài khoản trên site bán hàng (bảng Customer).
    /// Đây là mục riêng, tách biệt với "Thành viên" (Member - tài khoản nhân viên/quản trị).
    /// </summary>
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly FoodContext _dbContext;
        public CustomerController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> getList(jDatatable model)
        {
            var items = (from i in _dbContext.Customers select i);
            int recordsTotal = 0;

            // 1. Sắp xếp dữ liệu động từ jQuery DataTable đẩy lên
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }
            else
            {
                // SỬA LỖI: Thay thế cột 'CreatedOn' không tồn tại bằng khóa chính 'Id' để sắp xếp mặc định
                items = items.OrderByDescending(i => i.Id);
            }

            // 2. Tìm kiếm dữ liệu theo từ khóa
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i =>
                    (i.Name != null && i.Name.Contains(model.search.value)) ||
                    (i.LoginName != null && i.LoginName.Contains(model.search.value)) || // SỬA LỖI: Đổi i.Email thành i.LoginName
                    (i.Phone != null && i.Phone.Contains(model.search.value)));
            }

            // Sử dụng CountAsync() để tối ưu hóa xử lý bất đồng bộ (async/await)
            recordsTotal = await items.CountAsync();

            // 3. Phân trang và trả ra định dạng JSON ẩn danh (Anonymous Object)
            var data = await items.Select(i => new
            {
                i.Id,
                i.Name,
                Email = i.LoginName, // SỬA LỖI: Lấy dữ liệu từ cột LoginName và gán nhãn tên là Email để khớp cấu trúc cũ ngoài giao diện JS
                i.Phone,
                i.Address, // Thêm trường thông tin Address của database thực tế nếu cần dùng
                orderCount = i.Orders.Count
            }).Skip(model.start).Take(model.length).ToListAsync();

            var jsonData = new { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
    }
}