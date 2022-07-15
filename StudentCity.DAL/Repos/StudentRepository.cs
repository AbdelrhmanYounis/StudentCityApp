using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Shared;
using StudentCity.DAL.Utilities;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos
{
   
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(IOptions<DbSettings> settings) : base(settings, "Students")
        {
        }
        public UserModel GetUser(string email, string password)
        {
            var exists = GetAll().SingleOrDefault(x => x.user.Email == email);
            if (exists != null)
            {
                var salt = SecurityHelper.Encrypt(exists.user.Email);
                string hashedPassword = SecurityHelper.ComputeHash(password, "SHA256", Encoding.UTF8.GetBytes(salt));
                var std = GetAll().SingleOrDefault(x => x.user.Email == email && x.user.HashedPassword == hashedPassword);
                if (std == null)
                {
                    throw new Exception("Invalid Password");
                }
                return std.user;
            }
            throw new Exception("Invalid UserName");
        }
    }
}
