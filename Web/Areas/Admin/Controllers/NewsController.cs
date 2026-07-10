using Core.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Web.Areas.Admin.Extensions;
using Web.Areas.Admin.Models;
using Web.Models.EF;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly FoodContext _dbContext;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public NewsController(FoodContext dbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> getList(jDatatable model)
        {
            var items = (from i in _dbContext.Articles select i);
            int recordsTotal = 0;
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.Title.Contains(model.search.value));
            }
            recordsTotal = items.Count();
            var data = await items.Select(i => new
            {
                i.Id,
                i.Title,
                i.CreatedOn
            }).Skip(model.start).Take(model.length).ToListAsync();
            var jsonData = new { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
        [HttpGet]
        public async Task<IActionResult> getItem(Guid id)
        {
            if (_dbContext.Articles == null)
                return NotFound();
            var item = await _dbContext.Articles.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost]

        public async Task<IActionResult> Save(NewsViewModel model, IFormFile Picture)
        {
            Core.Database.Models.Article item;
            var loggedMember = HttpContext.Session.GetObject<Member>("member");
            if (model.Id == null)
            {
                item = new Core.Database.Models.Article();
                item.Id = Guid.NewGuid();
                item.CreatedBy = loggedMember.Id;
                item.CreatedOn = DateTime.Now;
                await _dbContext.Articles.AddAsync(item);

            }
            else
            {
                item = await _dbContext.Articles.FindAsync(model.Id);
                item.CreatedBy = loggedMember.Id;
                item.ModifiedOn = DateTime.Now;
            }
            item.Title = model.Title;
            item.Content = model.Content;
            item.Keyword = model.Keyword;
            item.Description = model.Description;

            if (Picture != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "img/news/", Picture.FileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await Picture.CopyToAsync(stream);
                    stream.Close();
                }
                item.Picture = "/img/news/" + Picture.FileName;
            }
            await _dbContext.SaveChangesAsync();
            return Ok(item);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                var item = await _dbContext.Articles.FindAsync(id);
                string path = this._environment.WebRootPath + item.Picture;
                _dbContext.Entry(item).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Ok(true);

            }
            catch
            {
                return Ok(false);
            }
        }

            [HttpPost]
            public async Task<IActionResult> UploadPicture(IFormFile Picture)
            {
                if (Picture != null)
                {
                    var path = Path.Combine(this._environment.WebRootPath, "img/news/", Picture.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await Picture.CopyToAsync(stream);
                    }
                    return Ok("/img/news/" + Picture.FileName);
                }

                return Ok("");
            }
        
    }
}
