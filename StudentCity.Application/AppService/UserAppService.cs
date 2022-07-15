using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class UserAppService
    {
        private readonly UserService _service;

        public UserAppService(UserService service)
        {
            _service = service;
        }

        public UserModel GetUserByCredentials(string email, string password)
        {
            return _service.GetUserByCredentials(email, password);
        }
        public List<UserModel> GetAll()
        {
            return _service.GetAllUsers();
        }
        public Student Post(Student model)
        {
            // any gurd validation 
            
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.Email, "Email");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.FirstName, "FirstName");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.LastName, "LastName");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.Mobile, "Mobile");
             return _service.CreateUser(model);
            //return null;
           
        }
        public bool Update(Student model)
        {
            // any gurd validation 

            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.Email, "Email");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.FirstName, "FirstName");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.LastName, "LastName");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.user.Mobile, "Mobile");
            return _service.UpdateUser(model);
        }
        public UserModel GetById(int id)
        {
            return _service.GetById(id);
        }
        public string ForgetPassword(ForgotPasswordDto model)
        {
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.Email, "Email");
            return _service.ForgetPassword(model);
        }
        public Student ResetPassword(ResetPasswordDto model)
        {
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.Password, "Password");
            return _service.ResetPassword(model);
        }
        public bool ChangePassword(ChangePasswordDto model,int UserId)
        {
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.OldPassword, "OldPassword");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NewPassword, "NewPassword");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.ConfirmPassword, "ConfirmPassword");
            return _service.ChangePassword(model, UserId);
        }
    }
}
