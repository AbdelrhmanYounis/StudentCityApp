using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using StudentCity.BL.Dtos;
using StudentCity.BL.Helpers;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Shared;
using StudentCity.DAL.Utilities;

namespace StudentCity.BL.Services.Permissions
{
    public class UserService
    {
        private readonly UserRepository UserRepository;
        private readonly StudentRepository StudentRepository;
        private readonly ResetCodeRepository ResetCodeRepository;
        private readonly StudentService StudentService;
        public UserService(UserRepository userRepository, StudentRepository studentRepository, ResetCodeRepository resetCodeRepository , StudentService studentService)
        {
            UserRepository = userRepository;
           StudentRepository = studentRepository;
            ResetCodeRepository = resetCodeRepository;
            StudentService = studentService;
        }

        /// <summary>
        /// Get All user including roles that represent them 
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetAllUsers()
        {
            return UserRepository.GetAll().ToList();
        }

        /// <summary>
        /// updates user using dto passed as parameter 
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(Student model)
        {
            try
            {
                var existUser = StudentService.GetById(model.Id);

                if (existUser != null) // edit user
                {
                    // update only properties from ui
                    return StudentService.Update(UserRepository.CreatePassword(model, model.user.HashedPassword));
                   // existUser.FirstName = user.FirstName;
                   // existUser.LastName = user.LastName;
                   // existUser.Email = user.Email;
                   // existUser.Mobile = user.Mobile;
                   // existUser.IsDisabled = user.IsDisabled;
                   //// existUser.SSN = user.SSN;
                   // UserRepository.InsertOrUpdate(x=>x.Id == existUser.Id, existUser);

                }

            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        public Student CreateUser(Student model)
        {
            try
            {
               //try to find if another user exist with same email
                if (CheckEmailIsUnique(model.user.Email))
                {
                    return StudentService.Post(UserRepository.CreatePassword(model, model.user.HashedPassword));
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public UserModel GetUserByCredentials(string email, string password)
        {
            var salt = SecurityHelper.Encrypt(email);
            string hashedPassword = SecurityHelper.ComputeHash(password, "SHA256", Encoding.UTF8.GetBytes(salt));
            //var stdt = StudentService.GetById(1);
            var stdt =StudentService.GetAll().SingleOrDefault(x=>x.user.Email==email);
          //  var m = StudentRepository.GetAll().ToList();
         //   var stdt = m.SingleOrDefault(x => x.user.Email == email);
            if (stdt ==null)
            {
                var user = GetAllUsers().FirstOrDefault();
                if (user.Email==email && user.HashedPassword == hashedPassword)
                {
                    return user;

                }
            }
            else if (stdt.user.HashedPassword== hashedPassword)
            {
                stdt.user.Id = stdt.Id;
                return stdt.user;
                
            }
           else
                throw new Exception("Invalid Credentials");
            return null;
        }

        #region Roles
        public bool CheckEmailIsUnique(string email)
        {
            return !(UserRepository.GetAll().Any(u => u.Email == email));
             
        }

        public Student CheckEmailIsExists(string email)
        {
            //var user = UserRepository.GetAll().SingleOrDefault(u => u.Email == email);

            return StudentService.GetAll().FirstOrDefault(u => u.user.Email == email);

        }
        #endregion

        public string ForgetPassword(ForgotPasswordDto model)
        {
            var user = CheckEmailIsExists(model.Email);

            // generate reset code by encrypting anonymous object that hold UserID & Expiration Date and convert it to Ok
            string resetCode = ReturnCode() ;
            string body = string.Format("please press here to reset your password <a href='http://localhost:2169/Account/ResetPassword/{0}'>Reset</a>", resetCode);
            // send this code by email to the user 
            MyMailServer.SendMail(user.user.Email,"Reset Passowrd",body);
            // save this code with user id 
            ResetCodeModel RCM = new ResetCodeModel
            {
                UserId = user.Id,
                Code=resetCode
            };
            ResetCodeRepository.Add(RCM);
            return resetCode;

        }

        public Student ResetPassword(ResetPasswordDto model)
        {
            var userId = ResetCodeRepository.GetAll().FirstOrDefault().UserId;
            var existUser = StudentService.GetById(userId);

            if (existUser != null) // reset password
            {
                existUser.user.HashedPassword = SecurityHelper.ComputeHash(model.Password, "SHA256", Encoding.UTF8.GetBytes(existUser.user.Salt));
                 StudentService.Update(existUser);
                return existUser;

            }
            throw new Exception("Invalid");
        }
        public UserModel GetById(int id)
        {
            return UserRepository.GetById(id);
        }
        public bool ChangePassword(ChangePasswordDto model ,int UserId)
        {
            var existUser = UserRepository.GetAll().SingleOrDefault(x => x.Id == UserId);

            model.OldPassword = SecurityHelper.ComputeHash(model.OldPassword, "SHA256", Encoding.UTF8.GetBytes(existUser.Salt));

            if (existUser.HashedPassword == model.OldPassword)// change password
            {
                existUser.HashedPassword = SecurityHelper.ComputeHash(model.NewPassword, "SHA256", Encoding.UTF8.GetBytes(existUser.Salt));
                 UserRepository.InsertOrUpdate(x => x.Id == existUser.Id, existUser);
                return true;
            }
            throw new Exception("Wrong Old Password");
        }
        private string ReturnCode() {
            Random r = new Random();
           return r.Next(1000000 , 1000000000).ToString();
        }
    }
}
