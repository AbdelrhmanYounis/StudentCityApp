using System.Collections.Generic;
using StudentCity.DAL.Shared;
using MongoDB.Bson;

namespace StudentCity.DAL.Models.Permissions
{
    public class RejectedStudentModel : BaseModel
    {
        public string SSN { get; set; }
    }
}
