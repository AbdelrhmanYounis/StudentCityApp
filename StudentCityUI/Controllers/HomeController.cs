using System;
using System.Diagnostics;
using System.Linq;
using StudentCity.Application;
using StudentCity.BL;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models;
using StudentCityUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ContactUsService _contactUsService;
        private RejectedStudentService RejectedStudentService { get; }
        static bool IsArabic=false;
        private readonly StudentService StudentService;

        public HomeController(ContactUsService contactUsService, RejectedStudentService rejectedStudentService , StudentService studentService)
        {
            _contactUsService = contactUsService;
            RejectedStudentService = rejectedStudentService;
            StudentService =studentService;
        }
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeLanguage(string language, string actionName, string controllerName)
        {
            CookiesManager cookiesManager = new CookiesManager(Request, Response);
            try
            {
                cookiesManager.Set("currentLanguage", language, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction(actionName, controllerName);
        }
        public IActionResult About()
        {

            return View();
        }
        public IActionResult Services()
        {

            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ApplyResult(string SSN)
        {
            if (RejectedStudentService.GetBySSN(SSN))
                return Json(0);
            else if(StudentService.GetAll().Any(x => x.user.SSN == SSN))
                return Json(1);

            return Json(-1);
        }



        [HttpPost]
        public IActionResult Contact([Bind("Name,Mobile,Email,Subject,Message")]ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                ContactUsApp app = new ContactUsApp(_contactUsService);
                app.Post(new ContactUs()
                {
                    Name = contactUs.Name,
                    Mobile = contactUs.Mobile,
                    Subject = contactUs.Subject,
                    Email = contactUs.Email,
                    Message = contactUs.Message
                });
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void setLang(bool isArabic)
        {
           IsArabic=isArabic;
        }
        public bool setLang()
        {
           return IsArabic ;
        }
    }
}
