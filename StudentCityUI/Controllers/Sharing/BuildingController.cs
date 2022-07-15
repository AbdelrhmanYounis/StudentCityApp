using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCity.Application.AppService;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCityUI.Dto;
using StudentCityUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers.Sharing
{
    [UserIsAuthenticated]
    public class BuildingController : Controller
    {
        public BuildingAppService BuildingAppService { get; }
        public CollegeAppService CollegeAppService { get; }
        private readonly StudentService StudentService;

        public BuildingController(BuildingAppService buildingAppService, CollegeAppService collegeAppService , StudentService studentService)
        {
            BuildingAppService = buildingAppService;
            CollegeAppService = collegeAppService;
            StudentService = studentService;
        }
        public IActionResult Index()
        {

            return View(BuildingAppService.GetAll());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Building model)
        {
                BuildingAppService.Post(model);
                return RedirectToAction("Index", "Building");
            
        }
        public IActionResult Edit(int id)
        {
            var model = BuildingAppService.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotAuthorized", "Home");
        }
        [HttpPost]
        public IActionResult Edit(Building model)
        {
            if (BuildingAppService.Update(model))
                return RedirectToAction("Index");
           
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(BuildingAppService.Delete(id));
        }
        public IActionResult BuildingStatistics(int id)
        {
            ViewBag.BuildingId = id;
            return PartialView();
        }
        [HttpGet]
        public IActionResult GetBuildingData(int BuildingId)
        {
            var colleges = CollegeAppService.GetAll();
            var building = BuildingAppService.GetById(BuildingId);
            var buildingCapacity = building.R_Students_num * building.F_Rooms_num * building.B_Floors_Num;
            List<data> buildingDt = new List<data> { };
            int studentCounter=0;
            foreach (var item in colleges)
            {
                int studentCounterPerCollege = StudentService.GetAll().Where(q => q.College.Id == item.Id && q.Building!=null && q.Building.Id== BuildingId).Count();
                if (studentCounterPerCollege > 0)
                {
                    studentCounter += studentCounterPerCollege;
                    buildingDt.Add( new data
                    {
                        name = item.NameEn,
                        y =((studentCounterPerCollege*1.0) / (buildingCapacity*1.0))*100,
                        drilldown= item.NameEn
                    });
                }
                ;
            }
            if (buildingCapacity == 0)
            {
                buildingDt.Add(new data
                {
                    name = "No Seats",
                    y = 100.0,
                    drilldown = "No Seats"
                });
            }
            else if ((buildingCapacity - studentCounter) > 0)
            {
                buildingDt.Add(new data
                {
                    name = "Free Seats",
                    y = (((buildingCapacity - studentCounter) * 1.0) / (buildingCapacity * 1.0))*100,
                    drilldown = "Free Seats"
                });
            }
            BuildingData BD = new BuildingData
            {
                building= building.B_Number,
                data= buildingDt
            };
            return Json(BD);
        }

    }
}