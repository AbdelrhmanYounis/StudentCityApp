using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace StudentCityUI.Helpers
{
    public class Helper
    {
        private readonly IHttpContextAccessor _httpContext;

        public Helper(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public Guid BranchId
        {
            get
            {
                try
                {
                    
                    return Guid.Parse(_httpContext.HttpContext.Request.Headers.Values.FirstOrDefault(r => r == "BranchId"));

                }
                catch (Exception)
                {
                    return Guid.Empty;
                }
            }
        }
    }
}