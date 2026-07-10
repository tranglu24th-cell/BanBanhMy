using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;
using Web.Areas.Admin.Attributes;
using Web.Areas.Admin.Extensions;
using Web.Areas.Admin.Models;
using Web.Models.EF;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        
        private readonly FoodContext _dbContext;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public MemberController(FoodContext dbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        [Authorized(Code = "view-members")]
        [HttpPost]
        public async Task<IActionResult> getList(jDatatable model)
        {
            var items = (from i in _dbContext.Members select i);
            int recordsTotal = 0;
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.Name.Contains(model.search.value));
            }
            recordsTotal = items.Count();
            var data = await items.Select(i => new
            {
                i.Id,
                i.Name,
                groupName = i.Group.Name,
                i.LastLogin,
                i.Picture
            }).Skip(model.start).Take(model.length).ToListAsync();
            var jsonData = new { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorized(Code = "edit-member")]
        [HttpGet]
        public async Task<IActionResult> getItem(Guid id)
        {
            if (_dbContext.Members == null)
                return NotFound();
            var item = await _dbContext.Members.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [Authorized(Code = "save-member")]
        [HttpPost]

        public async Task<IActionResult> Save(MemberViewModel model, IFormFile Picture)
        {
            Core.Database.Models.Member item;
            if (model.Id == null)
            {
                item = new Core.Database.Models.Member();
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
                await _dbContext.Members.AddAsync(item);
            }
            else
            {
                item = await _dbContext.Members.FindAsync(model.Id);
                item.ModifiedOn = DateTime.Now;
            }
            item.Name = model.Name;
            item.LoginName = model.LoginName;
            if (!string.IsNullOrEmpty(model.Password))
            {
                item.Password = MD5Hash(model.Password); //sua
            }
            item.Email = model.Email;
            if(Picture !=null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "/img/users/", Picture.FileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await Picture.CopyToAsync(stream);
                    stream.Close();
                }
                item.Picture = "/img/users/" + Picture.FileName;
            }
            item.GroupId = model.GroupId;
            await _dbContext.SaveChangesAsync();
            return Ok(item);
        }
        [Authorized(Code = "delete-member")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var memberInArticle = await _dbContext.Articles.Where(m => m.CreatedBy == id).FirstOrDefaultAsync();
            if (memberInArticle == null)
            {
                var item = await _dbContext.Members.FindAsync(id);
                _dbContext.Entry(item).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                return Ok(true);
            }
            return Ok(false);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel item)
        {
            string md5Password = MD5Hash(item.Password);
            var member = _dbContext.Members.Where(i => i.LoginName == item.LoginName && i.Password == md5Password).FirstOrDefault();
            if (member != null) 
            { 
                HttpContext.Session.SetObject("member", member);
                var codes = _dbContext.Authorizeds.Where(i=>i.GroupId == member.GroupId).Select(i=>i.Role.Code).ToList();
                HttpContext.Session.SetObject("codes", codes);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Member");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetObject("member", null);
            return RedirectToAction("Index", "Home", new {area = ""});
        }
        public string MD5Hash(string text)
        {
            MD5 md5H = MD5.Create();
            //convert the input string to a byte array and compute its hash
            byte[] data = md5H.ComputeHash(Encoding.UTF8.GetBytes(text));
            // create a new stringbuilder to collect the bytes and create a string
            StringBuilder sB = new StringBuilder();
            //loop through each byte of hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sB.Append(data[i].ToString("x2"));
            }
            //return hexadecimal string
            return sB.ToString();
        }
        

    }
}
