using System.Collections.Generic;
using StudentCity.DAL.Shared;
using MongoDB.Bson;

namespace StudentCity.DAL.Models.Permissions
{
    public class ResetCodeModel : BaseModel
    {
        public int UserId { get; set; }
        public string Code { get; set; }

    }
}
