using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using StudentCity.Application;
using StudentCity.Application.AppService;
using StudentCity.BL;
using StudentCity.BL.Services.Lookups;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models;
using StudentCity.DAL.Models.Permissions;
using StudentCityUI.Dto;
using StudentCityUI.Helpers;
using StudentCityUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers
{
    [UserIsAuthenticated]
    public class AdminController : Controller
    {
        public UserAppService UserAppService { get;}
        public GovernrateAppService GovernrateAppService { get; }
        public CollegeAppService CollegeAppService { get; }
        public GradeService GradeService { get; }
        public BuildingAppService BuildingAppService { get; }
        public CityAppService CityAppService { get; }
        public RejectedStudentService RejectedStudentService { get; }
        public readonly StudentService StudentService;
        public AdminController(UserAppService userAppService, StudentService studentService , BuildingAppService buildingAppService , GovernrateAppService governrateAppService , CollegeAppService collegeAppService , GradeService gradeService , CityAppService cityAppService , RejectedStudentService rejectedStudentService)
        {
            UserAppService = userAppService;
            StudentService = studentService;
            BuildingAppService =buildingAppService;
            GovernrateAppService = governrateAppService;
            CollegeAppService = collegeAppService;
            GradeService =gradeService;
            CityAppService = cityAppService;
            RejectedStudentService = rejectedStudentService;
        }
        public IActionResult Index()
        {
            ViewBag.buildings = BuildingAppService.GetAll();
            ViewBag.grades = GradeService.GetAll();
            ViewBag.colleges = CollegeAppService.GetAll();
            ViewBag.governrates = GovernrateAppService.GetAll();
            return View(StudentService.GetAll());
        }
        /******************************************************************************/
        public IActionResult GetLevel(int id)
        {
            var College = CollegeAppService.GetById(id);
            if (College != null)
            {
                int[] arr = new int[College.LevelNum];
                for (int i = 1; i <= College.LevelNum; i++)
                    arr[i - 1] = i;
                return Json(arr);
            }
            return Json(null);
        }
        /******************************************************************************/
        public IActionResult GetCity(int id)
        {
            var Cities = CityAppService.GetAll().Where(x => x.Governrate.Id == id);
            if (Cities != null)
            {
                return Json(Cities);
            }
            return Json(false);
        }
        /******************************************************************************/
        private List<Student> GetStudents(enrollDto BoxesParmData)
        {
            var result = new List<Student> { };
            result = (BoxesParmData.building == 0) ? StudentService.GetAll().Where(x => x.Building == null).ToList() : StudentService.GetAll().Where(x => x.Building != null && x.Building.Id == BoxesParmData.building).ToList();
            if (BoxesParmData.level != 0)
            {
                result = result.Where(x => x.College.Level == BoxesParmData.level).ToList();
            }
            if (BoxesParmData.college != 0)
            {
                result = result.Where(x => x.College.Id == BoxesParmData.college).ToList();
            }
            if (BoxesParmData.grade != 0)
            {
                result = result.Where(x => x.College.Grade.Id == BoxesParmData.grade).ToList();
            }
            if (BoxesParmData.governrate != 0)
            {
                result = result.Where(x => x.city.Governrate.Id == BoxesParmData.governrate).ToList();
            }
            if (BoxesParmData.city != 0)
            {
                result = result.Where(x => x.city.Id == BoxesParmData.city).ToList();
            }
            return result;
        }
       /******************************************************************************/
       [HttpGet]
        public ActionResult UnEnrollAll(enrollDto BoxesParmData)
        {
            try
            {
                var result = GetStudents(BoxesParmData);
                foreach (var std in result)
                {
                    std.Building = null;
                    StudentService.Update(std);
                }
                BoxesParmData.building = 0;
                return Search(BoxesParmData);
            }
            catch
            {
                return Json(false);
            }
        }/******************************************************************************/
        [HttpGet]
        public ActionResult EnrollAll(enrollAllDto BoxesParmData)
        {
            try
            {
                var result = GetStudents(new enrollDto {
                   building= BoxesParmData.building,
                   governrate= BoxesParmData.governrate,
                   city= BoxesParmData.city,
                   college= BoxesParmData.college,
                   grade= BoxesParmData.grade,
                   level= BoxesParmData.level
                });
                int remainderCapacity = CompareCapacity(result.Count(), BoxesParmData.buildingId);
                if (remainderCapacity > -1)
                {
                    var newBuilding = BuildingAppService.GetById(BoxesParmData.buildingId);

                    foreach (var std in result)
                    {
                        std.Building = newBuilding;
                        StudentService.Update(std);
                    }
                    return Search(new enrollDto
                    {
                        building = newBuilding.Id,
                        governrate = BoxesParmData.governrate,
                        city = BoxesParmData.city,
                        college = BoxesParmData.college,
                        grade = BoxesParmData.grade,
                        level = BoxesParmData.level
                    });
                }
                else
                    return Json((remainderCapacity + result.Count())*-1);
            }
            catch
            {
                return Json(false);
            }
        }
        /******************************************************************************/
        public int CompareCapacity(int stds,int buildingId)
        {
           int stdsNum =StudentService.GetAll().Where(x => x.Building!=null && x.Building.Id == buildingId).Count();
            var Building = BuildingAppService.GetById(buildingId);
            int capacity = Building.R_Students_num * Building.F_Rooms_num * Building.B_Floors_Num;
            
                return capacity - (stds + stdsNum);
        }
        /******************************************************************************/
        [HttpGet]
        public ActionResult Search(enrollDto BoxesParmData)//GetStudentTable
        {
            
            return PartialView("Search", GetStudents(BoxesParmData));
        }
        /******************************************************************************/
        public IActionResult Meal()
        {
            return View();
        }
        /******************************************************************************/
        [HttpGet]
         public IActionResult GetStudent(string ssn)
        {
           var student = StudentService.GetAll().FirstOrDefault(Q => Q.user.SSN == ssn);
            if (student != null) {
                if (student.LastMeal.Date == DateTime.Now.ToLocalTime().Date)
                {
                    return Json(new MealDto {
                        image = student.user.Image[0],
                        name = student.user.FirstName + " " + student.user.LastName,
                        buildingInfo = (student.Building != null) ? student.Building.B_Number.ToString() : null,
                        roomInfo = student.RoomNum,
                        day = student.LastMeal.ToString().Split(" ")[0],
                        hour = student.LastMeal.ToString().Split(" ")[1].Split(":")[0] + " : " + student.LastMeal.ToString().Split(" ")[1].Split(":")[1]
                    });
                }
                else
                {
                    return Json(new MealDto
                    {
                        id=student.Id,
                        image = student.user.Image[0],
                        name = student.user.FirstName + " " + student.user.LastName,
                        buildingInfo = (student.Building != null) ? student.Building.B_Number.ToString() : null,
                        roomInfo = student.RoomNum,
                        day = student.LastMeal.ToString().Split(" ")[0],
                        hour =null
                    });
                }
            }
            return Json(null);
         }
        /******************************************************************************/
        [HttpGet]
         public IActionResult TakeMeal(int id)
         {
            var student = StudentService.GetAll().FirstOrDefault(Q => Q.Id == id);
            if (student != null)
            {
                student.LastMeal = DateTime.Now.ToLocalTime();
                return Json(StudentService.Update(student));
            }
            return Json(false);
         }
        /******************************************************************************/
        [HttpPost]
        public IActionResult Enroll(int studentId , int buildingId)
        {
            var student = StudentService.GetAll().FirstOrDefault(Q => Q.Id == studentId);
            if (student != null)
            {
                if(student.Building != null)
                {
                    student.Building = null;
                    student.user.IsDisabled = true;
                    StudentService.Update(student);
                    return Json(0);
                }
                else
                {
                    if (CompareCapacity(1, buildingId) > -1)
                    {
                        student.Building = BuildingAppService.GetById(buildingId);
                        student.user.IsDisabled = false;
                        StudentService.Update(student);
                        return Json(student.Building.B_Number);
                    }
                    else
                        return Json(-1);
                }
            }
            return Json(3);
        }
        /*********************************Reject*********************************************/
        [HttpPost]
        public IActionResult Reject(int id)
        {
          var result=  StudentService.GetById(id);
            if (result != null)
            {
                StudentService.Delete(id);
                
                RejectedStudentService.Post(new RejectedStudentModel {
                    SSN=result.user.SSN
                });
                return Json(true);
            }
            return Json(false);
        }
        /*********************************RejectAllStudent*********************************************/
        [HttpGet]
        public ActionResult RejectAllStudent(enrollDto BoxesParmData)
        {
            try
            {
                var result = GetStudents(BoxesParmData);
                foreach (var std in result)
                {
                    StudentService.Delete(std.Id);
                    RejectedStudentService.Post(new RejectedStudentModel
                    {
                        SSN = std.user.SSN
                    });
                }
                BoxesParmData.building = 0;
                return Search(BoxesParmData);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}