using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.EF;
using System.Linq.Dynamic.Core;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly FoodContext _dbContext;
        public ReportController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult IncomeByMonth()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> getIncomeByMonth(int year)
        {
            var items = from k in (from o in _dbContext.Orders.Where(i => i.CreatedOn.Value.Year == year && i.UpdatedOn != null)
                                   join d in _dbContext.Details on o.Id equals d.OrderId
                                   select new
                                   {
                                       Month = o.CreatedOn.Value.Month,
                                       Income = d.Amount * d.price * 1.1 + 30000
                                   })
                        group k by k.Month into g
                        select new
                        {
                            Months = g.Key,
                            Incomes = g.Sum(p => p.Income)
                        };
            return Ok(await items.OrderBy(p => p.Months).ToListAsync());
        }
        [HttpGet]
        public IActionResult ThongKeBanh()
        {
            var thongKeList = (from d in _dbContext.Details
                               join p in _dbContext.Products on d.ProductId equals p.Id
                               select new
                               {
                                   ProductId = d.ProductId,
                                    
                                   ProductName = p.Title,
                                 
                                   Amount = d.Amount
                               })
                               .GroupBy(x => new { x.ProductId, x.ProductName })
                               .Select(g => new
                               {
                                   TenBanh = g.Key.ProductName,
                                   SoLuongDaBan = g.Sum(x => x.Amount)
                               })
                               .OrderByDescending(x => x.SoLuongDaBan)
                               .ToList();

            return View(thongKeList);
        }
    }
}
