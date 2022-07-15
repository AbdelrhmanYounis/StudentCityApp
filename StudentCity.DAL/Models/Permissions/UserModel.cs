using System.Collections.Generic;
using StudentCity.DAL.Shared;
using MongoDB.Bson;

namespace StudentCity.DAL.Models.Permissions
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public int BankAccount { get; set; }
        public bool IsDisabled { get; set; }
        public string Mobile { get; set; }
        public List<string> Image { get; set; }
        public bool IsSystemAdmin { get; set; }

    }
}
