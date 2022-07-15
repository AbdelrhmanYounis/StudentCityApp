using System.Linq;
using StudentCity.Application.AppService;
using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using StudentCity.BL.Services.Utilities;
using StudentCity.DAL.Models.Permissions;
using StudentCityUI.Dto;
using StudentCityUI.Helpers;
using StudentCityUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers
{
    [UserIsAuthenticated]
    public class ProfileController : Controller
    {
        public UserAppService AppService { get; set; }
        public readonly StudentService StudentService; 
            public readonly ProfileHandeling ProfileHandeling; 
        public ProfileController(UserAppService appService, StudentService studentService, ProfileHandeling profileHandeling)
        {
            AppService = appService;
            StudentService = studentService;
            ProfileHandeling = profileHandeling;
        }


        public IActionResult Index()
        {
            //  var userInfo = HttpContext.Session.GetString("userInfo");
            CookiesManager cookiesManager = new CookiesManager(HttpContext.Request, HttpContext.Response);
            var userInfo = cookiesManager.Get("token");
            if (userInfo != null)
            {
                TokenDto tokenDto = JsonConverter.Deserialize(userInfo);
                if (tokenDto.UserStudent)
                {
                    var model = StudentService.GetAll().FirstOrDefault(x => x.Id == tokenDto.UserId);
                    return View(ProfileHandeling.GetStudent(tokenDto.UserId));
                }
            }
            return View();
        }
       
       
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDto model)
        {
            CookiesManager cookiesManager = new CookiesManager(HttpContext.Request, HttpContext.Response);
            var userInfo = cookiesManager.Get("token");
            if (userInfo != null)
            {
                TokenDto tokenDto = JsonConverter.Deserialize(userInfo);
                AppService.ChangePassword(model, tokenDto.UserId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("NotAuthorized", "Home");
        }

        [HttpPost]
        public IActionResult ChangeKey()
        {
            var student = GetStudent();
            if (student != null)
            {
                student.RoomKey = (student.RoomKey == 0) ? 1 : 0;
                StudentService.Update(student);
                return Json(student.RoomKey);
            }
            return Json(false);
        }

        public Student GetStudent()
        {
            CookiesManager cookiesManager = new CookiesManager(HttpContext.Request, HttpContext.Response);
            var userInfo = cookiesManager.Get("token");
            TokenDto tokenDto = JsonConverter.Deserialize(userInfo);
            if (tokenDto.UserStudent)
                return StudentService.GetById(tokenDto.UserId);
            else
                return null;
        }
    }
}