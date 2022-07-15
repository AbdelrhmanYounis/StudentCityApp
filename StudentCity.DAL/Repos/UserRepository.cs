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
  
    public class UserRepository : GenericRepository<UserModel>
    {
        public UserRepository(IOptions<DbSettings> settings) : base(settings, "Users")
        {
        }


        public UserModel GetUser(string email, string password)
        {
            var exists = GetAll().FirstOrDefault();
            if (exists != null)
            {
                var salt = SecurityHelper.Encrypt(exists.Email);
                string hashedPassword = SecurityHelper.ComputeHash(password, "SHA256", Encoding.UTF8.GetBytes(salt));
                var user =  GetAll().SingleOrDefault(x => x.Email == email && x.HashedPassword == hashedPassword);
               
                return user;
            }
           return null;
        }

        public Task<object> GetUserBy(Expression<Func<UserModel, bool>> exp) => GetBy(exp);

        public UserModel GetUserWithRoles(string userName, string password, string Salt)
        {
           

            string hashedPassword = SecurityHelper.ComputeHash(password, "SHA256", Encoding.UTF8.GetBytes(Salt));
            return GetAll().SingleOrDefault(x => x.Email == userName && x.HashedPassword == hashedPassword);
        }
       

        public Student CreatePassword(Student model, string password = null)
        {

            string defaultPassword = password ?? "P@ssw0rd";
            //try
            //{
            //    MailServer.SendMail(model.user.Email, "Account Details", $"Welcome {model.user.FirstName} you have an account on student city with this password : {defaultPassword} ");
            //}
            //catch
            //{
            //    // ignored
            //}
            // generate salt by encrypting user email [it can be any field ]
            model.user.Salt = SecurityHelper.Encrypt(model.user.Email);

            // compute hash of new password using special hashing algorithm 
            model.user.HashedPassword = SecurityHelper.ComputeHash(defaultPassword, "SHA256", Encoding.UTF8.GetBytes(model.user.Salt));


            return model;

        }




    }
}
