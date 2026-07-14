using Core.Database.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.Models;
using Web.Models.EF;

namespace Web.Controllers
{
    /// <summary>
   
    /// </summary>
    public class AccountController : Controller
    {
        private readonly FoodContext _dbContext;

        public AccountController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View(new RegisterViewModel());
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = model.Email.Trim().ToLower();
            var phone = model.Phone?.Trim();

            bool isEmailTaken = await _dbContext.Customers.AnyAsync(c => c.LoginName != null && c.LoginName.ToLower() == email);
            if (isEmailTaken)
            {
                ModelState.AddModelError(nameof(model.Email), "Email này đã được đăng ký. Vui lòng dùng email khác.");
            }

            var existingByPhone = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
            if (existingByPhone != null && !string.IsNullOrEmpty(existingByPhone.Password))
            {
                ModelState.AddModelError(nameof(model.Phone), "Số điện thoại này đã được đăng ký.");
            }

            if (!ModelState.IsValid)
                return View(model);

            Customer customer;
            if (existingByPhone != null)
            {
                customer = existingByPhone;
                customer.Name = model.FullName.Trim();
                customer.LoginName = email;
                customer.Password = model.Password;
            }
            else
            {
                customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = model.FullName.Trim(),
                    Phone = phone,
                    LoginName = email,
                    Password = model.Password,
                    Address = ""
                };
                await _dbContext.Customers.AddAsync(customer);
            }

            await _dbContext.SaveChangesAsync();

            await SignInCustomerAsync(customer);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = model.Email.Trim().ToLower();

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.LoginName != null && c.LoginName.ToLower() == email);

            if (customer == null || model.Password != customer.Password)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                return View(model);
            }

            await SignInCustomerAsync(customer);
            HttpContext.Session.SetString("UserId", customer.Id.ToString());

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInCustomerAsync(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Name ?? customer.LoginName ?? ""),
                new Claim(ClaimTypes.Email, customer.LoginName ?? ""),
                new Claim(ClaimTypes.Role, "Customer")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                });
        }
        // GET: /Account/OrderHistory
        // GET: /Account/OrderHistory
        [HttpGet]
        [Route("Account/OrderHistory")]
        public async Task<IActionResult> OrderHistory()
        {
            // 1. Lấy Tên hiển thị (hoặc Username) của tài khoản đang đăng nhập
            var currentUserName = User.Identity?.Name;

            // Nếu hệ thống của bạn lưu tên trong Claim NameIdentifier, lấy như cũ:
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserName) && string.IsNullOrEmpty(userIdClaim))
            {
                return RedirectToAction("Login", "Account");
            }

            // 2. Lọc đơn hàng dựa theo Tên khách hàng liên kết để đảm bảo chuẩn xác
            // Cách này giúp ông A đăng nhập chỉ thấy đơn ông A, bà Trang đăng nhập chỉ thấy đơn bà Trang
            var orders = await _dbContext.Orders
                .Include(o => o.Customer)
                .Where(o => o.Customer != null &&
                            (o.Customer.Name == currentUserName ||
                             o.Customer.Id.ToString() == userIdClaim))
                .OrderByDescending(o => o.Id) // Đơn mới đặt (như bánh Choux Cream) sẽ nhảy lên đầu
                .ToListAsync();

            return View(orders);
        }
        // GET: /Account/Profile
        [HttpGet]
        public IActionResult Profile()
        {
            // 1. Lấy chuỗi ID từ Session
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account");
            }

            // 2. Chuyển đổi sang dạng Guid
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("Định dạng ID người dùng không hợp lệ.");
            }

            // 3. Thay đổi truy vấn sang bảng Customers của bạn
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == userId);

            if (customer == null)
            {
                return NotFound();
            }

            // 4. Truyền dữ liệu customer sang View
            return View(customer);
        }
    }
}