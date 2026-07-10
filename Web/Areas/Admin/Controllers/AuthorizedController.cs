using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.EF;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorizedController : Controller
    {
        private readonly FoodContext _dbContext;
        public AuthorizedController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Save(Guid GroupId, Guid RoleId)
        {
            var item = await _dbContext.Authorizeds.Where(i => i.GroupId == GroupId && i.RoleId == RoleId).FirstOrDefaultAsync();
            if (item == null)
            {
                item = new Core.Database.Models.Authorized()
                {
                    Id = Guid.NewGuid(),
                    RoleId = RoleId,
                    GroupId = GroupId
                };
                _dbContext.Authorizeds.Add(item);
            }
            else
            {
                _dbContext.Remove(item);
            }
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
