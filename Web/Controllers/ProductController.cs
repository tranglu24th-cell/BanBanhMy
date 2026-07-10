using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.EF;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly FoodContext _dbContext;
        public ProductController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 1. Hàm load trang chủ ban đầu (Đã sửa để loại bỏ tận gốc các danh mục Admin)
        public async Task<IActionResult> Index()
        {
            // Tạo danh sách các tên danh mục của Admin cần loại bỏ hoàn toàn
            var adminCategoryNames = new List<string> { "Root", "Authorized", "Nhóm quyền", "Product", "Article" };

            // Lấy các danh mục mà tên của chúng KHÔNG nằm trong danh sách đen ở trên
            ViewBag.Categories = await _dbContext.Categories
                .Where(c => !adminCategoryNames.Contains(c.Name))
                .ToListAsync();

            return View();
        }

        // 2. Hàm AJAX xử lý lọc theo danh mục khi click nút tròn
        [HttpPost]
        public async Task<IActionResult> FilterProducts([FromBody] ProductFilterModel filter)
        {
            var query = _dbContext.Products.AsQueryable();

            // Lọc theo danh mục nếu có truyền Id lên
            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId);
            }

            var products = await query.ToListAsync();

            // Trả về file giao diện danh sách bánh thu gọn để ném ra trang chủ
            return PartialView("_ProductListPartial", products);
        }

        // 3. Hàm lấy danh sách sản phẩm sắp ra mắt (HÀM GỐC CỦA BẠN - GIỮ LẠI ĐỂ KHÔNG LỖI)
        public async Task<IActionResult> GetIsComming()
        {
            var items = from p in _dbContext.Products
                        where p.IsComming == true
                        orderby p.CreatedBy descending
                        select p;

            return Ok(await items.Take(8).Select(i => new {
                i.Id,
                i.Picture,
                i.Title,
                i.Price,
                i.PriceSale,
                i.IsSale,
                i.CategoryId
            }).ToListAsync());
        }
    }
}