using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Database.Models;
using Web.Areas.Admin.Extensions;
using Web.Models;
using Web.Models.EF;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly FoodContext _dbContext;
        private const string CART_SESSION_KEY = "cart";

        public CartController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        private List<CartItem> GetCart()
        {
            return HttpContext.Session.GetObject<List<CartItem>>(CART_SESSION_KEY) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObject(CART_SESSION_KEY, cart);
        }

        // GET: /Cart/Index -> Hiển thị trang giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // GET: /Cart/GetCartCount -> Lấy số lượng sản phẩm trong giỏ (dùng để đổ vào badge trên Navbar)
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            return Ok(new { cartCount = cart.Sum(i => i.Amount) });
        }

        // POST: /Cart/AddToCart -> AJAX thêm sản phẩm vào giỏ
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int amount = 1)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                return Ok(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

            // Nếu sản phẩm đang sale thì lấy PriceSale, không thì lấy Price
            double currentPrice = (product.IsSale == true && product.PriceSale.HasValue)
                ? product.PriceSale.Value
                : product.Price;

            if (existingItem != null)
            {
                existingItem.Amount += amount;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Picture = product.Picture,
                    Price = currentPrice,
                    Amount = amount
                });
            }

            SaveCart(cart);

            return Ok(new
            {
                success = true,
                message = "Đã thêm \"" + product.Title + "\" vào giỏ hàng.",
                cartCount = cart.Sum(i => i.Amount),
                cartTotal = cart.Sum(i => i.Total)
            });
        }

        // POST: /Cart/UpdateAmount -> AJAX đổi số lượng ngay trên trang giỏ hàng
        [HttpPost]
        public IActionResult UpdateAmount(Guid productId, int amount)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                return Ok(new { success = false, message = "Sản phẩm không có trong giỏ hàng." });
            }

            if (amount <= 0)
            {
                cart.Remove(item);
            }
            else
            {
                item.Amount = amount;
            }

            SaveCart(cart);

            return Ok(new
            {
                success = true,
                itemTotal = amount > 0 ? item.Total : 0,
                cartCount = cart.Sum(i => i.Amount),
                cartTotal = cart.Sum(i => i.Total)
            });
        }

        // POST: /Cart/RemoveItem -> AJAX xóa 1 sản phẩm khỏi giỏ
        [HttpPost]
        public IActionResult RemoveItem(Guid productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return Ok(new
            {
                success = true,
                cartCount = cart.Sum(i => i.Amount),
                cartTotal = cart.Sum(i => i.Total)
            });
        }

        // ===================================================
        // 1. HÀM GET: Hiển thị trang điền thông tin (Địa chỉ, SĐT...)
        // ===================================================
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = GetCart(); // Hoặc tên hàm lấy giỏ hàng từ Session của bạn
            if (cart == null || !cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index");
            }

            // ĐỒNG BỘ Ở ĐÂY: Truyền thẳng danh sách giỏ hàng (cart) ra ngoài View
            // Khớp 100% với khai báo @model List<Web.Models.CartItem> của file Checkout.cshtml
            return View(cart);
        }

        // ===================================================
        // 2. HÀM POST: Xử lý lưu vào Database khi bấm "Xác nhận đặt hàng"
        // ===================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string CustomerName, string CustomerPhone, string CustomerAddress)
        {
            var cart = GetCart();
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index");
            }

            // Kiểm tra dữ liệu thô từ form nhập
            string name = string.IsNullOrEmpty(CustomerName) ? "Khách vãng lai" : CustomerName;
            string phone = string.IsNullOrEmpty(CustomerPhone) ? "0000000000" : CustomerPhone;
            string address = string.IsNullOrEmpty(CustomerAddress) ? "Chưa cung cấp" : CustomerAddress;

            // 🌟 ĐỒNG BỘ CHO SOMEE & LOCAL: Lấy Id của tài khoản đang đăng nhập
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Guid? loggedInUserId = null;
            if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out Guid parsedGuid))
            {
                loggedInUserId = parsedGuid;
            }

            Customer customer = null;

            // Nếu người dùng đã đăng nhập, ưu tiên tìm Customer theo Id tài khoản đó
            if (loggedInUserId.HasValue)
            {
                customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == loggedInUserId.Value);
            }

            // Nếu chưa có hoặc chưa đăng nhập, tìm theo số điện thoại
            if (customer == null)
            {
                customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
            }

            if (customer == null)
            {
                customer = new Customer
                {
                    // Nếu có tài khoản đăng nhập thì dùng luôn Id tài khoản, không thì sinh mới vãng lai
                    Id = loggedInUserId ?? Guid.NewGuid(),
                    Name = name,
                    Phone = phone,
                    Address = address
                };
                _dbContext.Customers.Add(customer);
            }
            else
            {
                customer.Name = name;
                customer.Address = address;
            }

            // Tạo đơn hàng chính thức
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id, // Đã khớp 100% với Id tài khoản đăng nhập nếu có
                Customer = customer,
                CreatedOn = DateTime.Now,
                Status = OrderStatus.ChoDuyet,
                Details = new List<Details>()
            };

            // Đổ bánh từ giỏ hàng Session vào bảng chi tiết đơn hàng trong database
            foreach (var item in cart)
            {
                order.Details.Add(new Details
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    price = item.Price,
                    Amount = item.Amount
                });
            }

            // Lưu chính thức xuống SQL Server
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // Nạp lại đầy đủ thông tin kèm liên kết bảng để hiển thị sang trang thành công
            var savedOrder = await _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            // 🌟 SỬA TẠI ĐÂY: Dùng đúng biến CART_SESSION_KEY ("cart") để xóa sạch giỏ hàng tạm
            HttpContext.Session.Remove(CART_SESSION_KEY);

            // Chuyển tới trang Đặt hàng thành công và truyền đối tượng Order thật vào
            return View("CheckoutSuccess", savedOrder);
        }





        // GET: /Cart/CheckoutSuccess -> Trang cảm ơn sau khi đặt hàng thành công
        [HttpGet]
        public async Task<IActionResult> CheckoutSuccess(Guid id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }
    }
}