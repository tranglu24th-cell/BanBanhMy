using Core.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Areas.Admin.Extensions;

namespace Web.Areas.Admin.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var area = context.RouteData.Values["area"];
            if (context.RouteData.Values["action"].ToString() != "Login" && area?.ToString()=="Admin")
            {
                var member = context.HttpContext.Session.GetObject<Member>("member");

                if (member == null)
                {
                    context.Result = new LocalRedirectResult("/Admin/Member/Login");
                }
            }
        }
    }
}
