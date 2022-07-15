using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCity.DAL.Models.Permissions;

namespace StudentCityUI.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public bool UserStudent { get; set; }
    }
}
