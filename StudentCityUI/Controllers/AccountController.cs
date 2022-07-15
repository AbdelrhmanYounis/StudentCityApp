using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using StudentCity.Application.AppService;
using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Lookups;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCityUI.Controllers.Sharing;
using StudentCityUI.Dto;
using StudentCityUI.Helpers;
using StudentCityUI.Models;
using StudentCityUI.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers
{
    public class AccountController : Controller
    {
        public UserAppService UserAppService { get; }
        public ResetCodeAppService ResetCodeAppService { get; }
        private readonly SecurityManager _securityManager;
        public CityAppService CityAppService { get; }
        public GovernrateAppService GovernrateAppService { get; }
        public CountryAppService CountryAppService { get; }
        public CollegeAppService CollegeAppService { get; }
        public GradeService GradeService { get; }
        public StudyWayService StudyWayService { get; }
        private readonly StudentService StudentService;
        private readonly BehaviorService BehaviorService;
        private readonly IHostingEnvironment hostingEnvironment;
        private string profilePhotosPath;

        public AccountController(IHostingEnvironment hostingEnvironment, UserAppService userAppService, SecurityManager securityManager, SecurityManager securityManager1, CityAppService cityAppService, GovernrateAppService governrateAppService, CountryAppService countryAppService, CollegeAppService collegeAppService, StudyWayService studyWayService,
           ResetCodeAppService resetCodeAppService , GradeService gradeService , StudentService studentService ,
            BehaviorService behaviorService
        )
        {
            this.hostingEnvironment = hostingEnvironment;
            string contentRoot = this.hostingEnvironment.WebRootPath;
            profilePhotosPath = Path.Combine(contentRoot, "images");

            UserAppService = userAppService;
            ResetCodeAppService = resetCodeAppService;
            _securityManager = securityManager1;
            CityAppService = cityAppService;
            GovernrateAppService = governrateAppService;
            CountryAppService = countryAppService;
            CollegeAppService = collegeAppService;
            GradeService = gradeService;
            StudyWayService = studyWayService;
            StudentService = studentService;
            BehaviorService = behaviorService;
        }
        public IActionResult Login()
        {
            return PartialView("Login");
        }
       
        public IActionResult LoginRegister(Student model)
        {
            ViewBag.colleges = CollegeAppService.GetAll();
            ViewBag.countries = CountryAppService.GetAll();
            ViewBag.Grades = GradeService.GetAll();
            ViewBag.studyWays= StudyWayService.GetAll();
            return View();
        }
        [HttpGet]
        public IActionResult edit()
        {
            var std = GetStudent();
            ViewBag.colleges = CollegeAppService.GetAll();
            ViewBag.levels = GetLevelArr(std.College.Id);
            ViewBag.countries = CountryAppService.GetAll(); 
            ViewBag.Governrates = GetAreas(std.city.Governrate.Country.Id);
            ViewBag.Cities = GetCities(std.city.Governrate.Id);
            ViewBag.Grades = GradeService.GetAll();
            ViewBag.studyWays = StudyWayService.GetAll();
            return View(std);
        }
        [HttpPost]
        public IActionResult edit(Student model)
        {
            var oldData = GetStudent();
            IFormFile Photo = HttpContext.Request.Form.Files[0];
            if (Photo.Length != 0)
            {
                List<string> temp = new List<string>();
                PhotoUploadService uploadService = new PhotoUploadService(hostingEnvironment, profilePhotosPath);
                temp.Add(uploadService.UploadPhotoFromRequest(Photo));
                model.user.Image = temp;
            }
           else
            {
                model.user.Image = oldData.user.Image;
            }
                var country = CountryAppService.GetById(model.city.Governrate.Country.Id);
                var governrate = GovernrateAppService.GetById(model.city.Governrate.Id);
                var city = CityAppService.GetById(model.city.Id);
                var grade = GradeService.GetById(model.College.Grade.Id);
                var college = CollegeAppService.GetById(model.College.Id);
                if (country != null && governrate != null && city != null && grade != null && college != null)
                {
                model.Building = oldData.Building;
                model.LastMeal = oldData.LastMeal;
                model.GroupId = oldData.GroupId;
                model.RoomKey = oldData.RoomKey;
                model.RoomNum = oldData.RoomNum;
                    model.city.Governrate.Country = country;
                    model.city.Governrate = governrate;
                    model.city = city;
                    college.Grade = grade;
                    college.Level = model.College.Level;
                    model.College = college;
                    model.Sleep_Style = BehaviorService.GetSleepStyle(model.Sleep_Style);
                    model.Food_Style = BehaviorService.GetFoodStyle(model.Food_Style);
                    model.Study_Style = BehaviorService.GetStudyStyle(model.Study_Style);
                    model.Hobbies_Style = BehaviorService.GetHobbiesStyle(model.Hobbies_Style);
                    model.Activities_Style = BehaviorService.GetActivitiesStyle(model.Activities_Style);
                    model.Character_Style = BehaviorService.GetCharacterStyle(model.Character_Style);
                    model.user.IsDisabled = true;
                    UserAppService.Update(model);
                    return RedirectToAction("Index", "Profile");
                }
            
            return BadRequest();
        }
        public int[] GetLevelArr(int id)
        {
            var College = CollegeAppService.GetById(id);
            if (College != null)
            {
                int[] arr = new int[College.LevelNum];
                for (int i = 1; i <= College.LevelNum; i++)
                    arr[i - 1] = i;
                return arr;
            }
            return null;
        }

        public IActionResult GetLevel(int id)
        {
            return Json(GetLevelArr(id));
        }

        public IEnumerable<Governrate> GetAreas(int id)
        {
            return GovernrateAppService.GetAll().Where(x => x.Country.Id == id);
           
        }

        public IActionResult GetArea(int id)
        {
            return Json(GetAreas(id));
        }

        public IEnumerable<CityModel> GetCities(int id)
        {
            return CityAppService.GetAll().Where(x => x.Governrate.Id == id);
        }

        public IActionResult GetCity(int id)
        {
            return Json(GetCities(id));
        }
        [HttpPost]
        public IActionResult Register(Student model)
        {
            IFormFile Photo = HttpContext.Request.Form.Files[0];
            if (Photo != null)
            {
                List<string> temp = new List<string>();
                PhotoUploadService uploadService = new PhotoUploadService(hostingEnvironment, profilePhotosPath);
                temp.Add(uploadService.UploadPhotoFromRequest(Photo));
                model.user.Image = temp;
            }
            if (model.user.Image != null)
            {
                var country = CountryAppService.GetById(model.city.Governrate.Country.Id);
                var governrate = GovernrateAppService.GetById(model.city.Governrate.Id);
                var city = CityAppService.GetById(model.city.Id);
                var grade = GradeService.GetById(model.College.Grade.Id);
                var college = CollegeAppService.GetById(model.College.Id);
                if (country != null && governrate != null && city != null && grade != null && college != null)
                {
                    model.city.Governrate.Country = country;
                    model.city.Governrate = governrate;
                    model.city = city;
                    college.Grade = grade;
                    college.Level = model.College.Level;
                    model.College = college;
                    model.Sleep_Style = BehaviorService.GetSleepStyle(model.Sleep_Style);
                    model.Food_Style = BehaviorService.GetFoodStyle(model.Food_Style);
                    model.Study_Style = BehaviorService.GetStudyStyle(model.Study_Style);
                    model.Hobbies_Style = BehaviorService.GetHobbiesStyle(model.Hobbies_Style);
                    model.Activities_Style = BehaviorService.GetActivitiesStyle(model.Activities_Style);
                    model.Character_Style = BehaviorService.GetCharacterStyle(model.Character_Style);
                    model.user.IsDisabled = true;
                    var student = UserAppService.Post(model);
                    student.user.Id = student.Id;
                    GenerateToken(student.user);
                    return RedirectToAction("Index", "Home");
                }
            }
                return BadRequest();
        }

        [HttpPost]
        public IActionResult RegisterNew(Student model)
        {
           
            PhotoUploadService uploadService = new PhotoUploadService(hostingEnvironment, profilePhotosPath);
            
            if (model.user.Image[0] != null)
            {
                model.user.Image = new List<string> { (uploadService.UploadPhoto(model.user.Image[0])) };
                var city = CityAppService.GetById(model.city.Id);
                var grade = GradeService.GetById(model.College.Grade.Id);
                var college = CollegeAppService.GetById(model.College.Id);
                if (city != null && grade != null && college != null)
                {
                    model.city = city;
                    college.Grade = grade;
                    college.Level = model.College.Level;
                    model.College = college;
                    model.Sleep_Style = BehaviorService.GetSleepStyle(model.Sleep_Style);
                    model.Food_Style = BehaviorService.GetFoodStyle(model.Food_Style);
                    model.Study_Style = BehaviorService.GetStudyStyle(model.Study_Style);
                    model.Hobbies_Style = BehaviorService.GetHobbiesStyle(model.Hobbies_Style);
                    model.Activities_Style = BehaviorService.GetActivitiesStyle(model.Activities_Style);
                    model.Character_Style = BehaviorService.GetCharacterStyle(model.Character_Style);
                    model.user.IsDisabled = true;
                    var student = UserAppService.Post(model);
                    student.user.Id = student.Id;
                    GenerateToken(student.user);
                    return Json(true);
                }
            }
            return Json(false);
        }
        [HttpGet]
        public IActionResult IsEmailUnique(string email)
        {
            return Json(StudentService.GetAll().Any(q=>q.user.Email==email));
           
        }
        [HttpGet]
        public IActionResult IsSSNUnique(string ssn)
        {
            return Json(StudentService.GetAll().Any(q => q.user.SSN == ssn));

        }
        [HttpPost]
        public IActionResult Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LoginWithUserNameAndPassword(model.Email, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    return BadRequest("Wrong Username or password");
                }

            }
            return BadRequest("Wrong Username or password");
        }

        public IActionResult Logout()
        {
            CookiesManager cookiesManager = new CookiesManager(Request, Response);
            cookiesManager.Remove("token");
            //    HttpContext.Session.SetString("userInfo", null);

            return RedirectToAction("Index", "Home");
        }

        private void LoginWithUserNameAndPassword(string email, string password)
        {
            var user = UserAppService.GetUserByCredentials(email, password);
            if (user != null)
            {
                GenerateToken(user);
            }
        }

        private void GenerateToken(UserModel user)
        {
            try
            {
                var claims = _securityManager.ValidateUser(user);
                var roles = claims.Claims.Where(r => r.Type == ClaimTypes.Role)
                            .SelectMany(r => r.Value?.ToString().Split('|'));
                if (claims != null)
                {
                    var token = new TokenDto()
                    {
                        AccessToken = _securityManager.BuildJwtToken(claims),
                        UserName = user.FirstName + " " + user.LastName,
                        UserId = user.Id,
                        UserStudent = (user.IsSystemAdmin == true) ? false :true
                    };
                    CookiesManager cookiesManager = new CookiesManager(Request, Response);
                    cookiesManager.Set("token", JsonConverter.Serialize(token), 1);
                    cookiesManager.Set("AccessToken", token.AccessToken, 1);
                    cookiesManager.Set("UserId", token.UserId.ToString(), 1);
                    cookiesManager.Set("UserName", token.UserName, 1);
                    cookiesManager.Set("UserStudent", token.UserStudent.ToString(), 1);
                   
                    // add to session 
                    HttpContext.Session.SetString("userInfo", JsonConverter.Serialize(token));
                }

            }
            catch (Exception)
            {
                 BadRequest("Wrong Username or password");
            }



        }

        [HttpPost]
        public IActionResult ForgetPassword(string email)
        {
            ForgotPasswordDto forgotPasswordDto = new ForgotPasswordDto
            {
                Email = email
            };
            var result = UserAppService.ForgetPassword(forgotPasswordDto);
            if (result != null)
            {
                return Json(true);
            }
            return Json(false);
        }
        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            var w = ResetCodeAppService.GetAll();
            var isValid = ResetCodeAppService.GetAll().Any(x => x.Code == id);

            if (!isValid)
            {
                ViewBag.code = id;
                return View();
            }

            return RedirectToAction("NotAuthorized", "Home");
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordDto model)
        {
            var user = UserAppService.ResetPassword(model); 
            if (user != null)
            {
                ResetCodeAppService.Delete(user.Id);
                GenerateToken(user.user);
                return RedirectToAction("Index", "Home");
            }
            return BadRequest("Invalid");
        }

        /**********************GetStudent****************************************/

        private Student GetStudent()
        {
            CookiesManager cookiesManager = new CookiesManager(HttpContext.Request, HttpContext.Response);
            var userInfo = cookiesManager.Get("token");
            if (userInfo != null)
            {
                TokenDto tokenDto = JsonConverter.Deserialize(userInfo);
                var std = StudentService.GetById(tokenDto.UserId);

                if (std != null && !std.user.IsSystemAdmin)
                {
                    return std;
                }
            }
            return null;
        }
    }
}