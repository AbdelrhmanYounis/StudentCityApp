using System;
using StudentCityUI.Dto;
using StudentCityUI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentCityUI.Models
{
    public class UserIsAuthenticated : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
             CookiesManager cookiesManager = new CookiesManager(context.HttpContext.Request, context.HttpContext.Response);
            var userInfo = cookiesManager.Get("token");//context.HttpContext.Session.GetString("userInfo"); 
            if (userInfo != null)
            {
                TokenDto tokenDto = JsonConverter.Deserialize(userInfo);
                if (tokenDto == null)
                {
                    context.Result = new RedirectToActionResult("NotAuthorized", "Home", null);
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("NotAuthorized", "Home", null);
            }

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
