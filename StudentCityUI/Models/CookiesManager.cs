using System;
using Microsoft.AspNetCore.Http;

namespace StudentCityUI.Models
{
    public class CookiesManager 
    {
        private  HttpRequest _request;
        private  HttpResponse _response;

        public CookiesManager(HttpRequest request , HttpResponse response )
        {
            _request = request;
            _response = response;
        }

        public string Get(string key)
        {
            return _request.Cookies[key];
        }
     
        public void Set( string key, string value, int? expireTimeDays)
        {
            CookieOptions option = new CookieOptions();
            if (expireTimeDays.HasValue)
                option.Expires = DateTime.Now.AddDays(expireTimeDays.Value);
            else
                option.Expires = DateTime.Now.AddDays(10);
            _response.Cookies.Append(key, value, option);
        }

        public void Remove(string key)
        {
            _response.Cookies.Delete(key);
        }

    }
}
