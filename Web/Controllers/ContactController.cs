using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.EF;
using Core.Database.Models; // Ép dùng chuẩn model này
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly FoodContext _dbContext;
        public ContactController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.Contacts.OrderByDescending(c => c.CreatedAt).ToListAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Name, string Email, string Message)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Message))
            {
                // SỬA TẠI ĐÂY: Thay đổi từ Web.Models.EF thành class Contact chuẩn gốc
                var newContact = new Contact
                {
                    Name = Name,
                    Email = Email,
                    Message = Message,
                    CreatedAt = DateTime.Now
                };

                _dbContext.Contacts.Add(newContact);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}