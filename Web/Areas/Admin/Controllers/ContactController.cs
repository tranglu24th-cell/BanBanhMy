using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.EF;
using System;
using System.Threading.Tasks;

[Area("Admin")]
public class ContactController : Controller
{
    private readonly FoodContext _dbContext;
    public ContactController(FoodContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _dbContext.Contacts.OrderByDescending(c => c.CreatedAt).ToListAsync();
        return View(list);
    }

    [HttpPost]
    [Route("Admin/Contact/Reply")]
    public async Task<IActionResult> Reply(int Id, string AdminReply)
    {
        // Sử dụng FirstOrDefaultAsync dựa trên Context để tránh lỗi thực thể không định danh
        var contact = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == Id);

        if (contact != null)
        {
            contact.AdminReply = AdminReply;
            contact.RepliedAt = DateTime.Now;

            // Ép hệ thống cập nhật trạng thái thay đổi thay vì dùng lệnh .Update lỏng lẻo
            _dbContext.Entry(contact).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Contact", new { area = "Admin" });
    }
    [HttpPost]
    [Route("Admin/Contact/Delete")]
    public async Task<IActionResult> Delete(int Id)
    {
        // 1. Tìm bản ghi cần xóa dựa theo Id truyền lên
        var contact = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == Id);

        if (contact != null)
        {
            // 2. Thực hiện lệnh xóa bản ghi ra khỏi Database
            _dbContext.Contacts.Remove(contact);

            // 3. Lưu lại thay đổi xuống SQL Server
            await _dbContext.SaveChangesAsync();
        }

        // Xóa xong chuyển hướng quay lại trang danh sách Admin để cập nhật giao diện mới
        return RedirectToAction("Index", "Contact", new { area = "Admin" });
    }
}