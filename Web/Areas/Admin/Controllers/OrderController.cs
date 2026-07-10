using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Models;
using Web.Models.EF;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Core.Database.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly FoodContext _dbContext;
        public OrderController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(Guid id)
        {
            var order = _dbContext.Orders.Find(id);
            ViewBag.Customer = _dbContext.Customers.Find(order.CustomerId);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> GetDetailsList(jDatatable model, Guid orderid)
        {
            var items = (from i in _dbContext.Details where i.OrderId == orderid select i);
            int recordsTotal = items.Count();
            var data = await items.Select(i => new
            {
                i.Id,
                productName = i.Product.Title,
                i.Amount,
                i.price,
                total = i.Amount * i.price
            }).Skip(model.start).Take(model.length).ToListAsync();
            var jsonData = new { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
            
        [HttpPost]

        public async Task<IActionResult> getList(jDatatable model)
        {
            var items = (from i in _dbContext.Orders select i);
            int recordsTotal = 0;
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.Customer.Name.Contains(model.search.value));

            }
            recordsTotal = items.Count();
            // Lấy dữ liệu thô từ DB trước (EF Core không dịch được extension method GetDisplayName sang SQL)
            var raw = await items.Select(i => new
            {
                i.Id,
                customerName = i.Customer.Name,
                i.CreatedOn,
                i.UpdatedOn,
                i.Status
            }).Skip(model.start).Take(model.length).ToListAsync();

            // Định dạng hiển thị ở phía C# (in-memory)
            var data = raw.Select(i => new
            {
                i.Id,
                i.customerName,
                i.CreatedOn,
                i.UpdatedOn,
                status = (int)i.Status,
                statusName = i.Status.GetDisplayName()
            });
            var jsonData = new { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
        /// <summary>
        /// Cập nhật trạng thái đơn hàng.
        /// Khi chuyển từ "Chờ duyệt" -> "Đang làm bánh" (tức Admin duyệt đơn), hệ thống sẽ:
        ///  - Kiểm tra tồn kho (StockQuantity) của tất cả sản phẩm trong đơn.
        ///  - Nếu bất kỳ sản phẩm nào không đủ hàng => báo lỗi, KHÔNG duyệt đơn, KHÔNG trừ kho.
        ///  - Nếu đủ hàng cho tất cả sản phẩm => trừ kho + cập nhật trạng thái, tất cả trong 1 transaction.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatus status)
        {
            // Dùng transaction để đảm bảo việc trừ kho và đổi trạng thái luôn "ăn cả hoặc thua cả"
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var order = await _dbContext.Orders
                    .Include(o => o.Details)
                        .ThenInclude(d => d.Product)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    return NotFound(new { success = false, message = "Không tìm thấy đơn hàng." });

                // Chặn chuyển trạng thái không hợp lệ (ví dụ đơn đã hủy/hoàn thành mà vẫn sửa)
                if (!order.Status.CanTransitionTo(status))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Không thể chuyển trạng thái từ \"{order.Status.GetDisplayName()}\" sang \"{status.GetDisplayName()}\"."
                    });
                }

                // ===== LOGIC CHÍNH: Khi duyệt đơn (chuyển sang "Đang làm bánh") =====
                if (status == OrderStatus.DangLamBanh)
                {
                    var thieuHang = new List<string>();

                    foreach (var detail in order.Details)
                    {
                        if (detail.Product == null) continue;

                        if (detail.Product.StockQuantity < detail.Amount)
                        {
                            thieuHang.Add(
                                $"{detail.Product.Title} (còn {detail.Product.StockQuantity}, cần {detail.Amount})");
                        }
                    }

                    // Nếu có bất kỳ sản phẩm nào không đủ hàng -> báo lỗi, không duyệt đơn
                    if (thieuHang.Any())
                    {
                        return BadRequest(new
                        {
                            success = false,
                            message = "Không đủ số lượng tồn kho cho các sản phẩm: " + string.Join("; ", thieuHang)
                        });
                    }

                    // Đủ hàng cho tất cả sản phẩm -> trừ kho
                    foreach (var detail in order.Details)
                    {
                        if (detail.Product == null) continue;
                        detail.Product.StockQuantity -= detail.Amount;
                    }
                }

                // (Tùy chọn nâng cao) Nếu hủy đơn sau khi đã trừ kho -> hoàn lại kho cho đúng nghiệp vụ
                if (status == OrderStatus.DaHuy &&
                    (order.Status == OrderStatus.DangLamBanh || order.Status == OrderStatus.DangGiao))
                {
                    foreach (var detail in order.Details)
                    {
                        if (detail.Product == null) continue;
                        detail.Product.StockQuantity += detail.Amount;
                    }
                }

                order.Status = status;
                order.UpdatedOn = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = $"Cập nhật trạng thái đơn hàng thành \"{status.GetDisplayName()}\" thành công.",
                    status = (int)status,
                    statusName = status.GetDisplayName()
                });
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi cập nhật trạng thái đơn hàng." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            var item = await _dbContext.Orders.FindAsync(id);
            if (item.UpdatedOn != null)
                return Ok(false);
            var details=await _dbContext.Details.Where(i =>i.OrderId == id).ToListAsync();
            _dbContext.Details.RemoveRange(details);
            _dbContext.Entry(item).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok(true);
            
        }
    }
}
