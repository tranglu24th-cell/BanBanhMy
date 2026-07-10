using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Admin.Filters;
using Web.Models.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDb")));
builder.Services.AddSession(cfg =>
{
    cfg.Cookie.IsEssential = true;
    cfg.IdleTimeout = new TimeSpan(0, 15, 0);
});

// Cookie Authentication cho tài khoản Khách hàng (đăng ký/đăng nhập ở phía site bán hàng).
// Lưu ý: khu vực Admin (nhân viên/quản trị) vẫn dùng Session như cũ (MemberController), không đổi.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "FoodManagement.Customer";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddMvc(cfg =>
{
    cfg.Filters.Add(new CustomActionFilter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
