using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCity.Application.AppService;
using StudentCity.DAL.Models.Lookups;
using StudentCityUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers.Sharing
{
    [UserIsAuthenticated]
    public class CollegeController : Controller
    {
        public CollegeAppService CollegeAppService { get; }

        public CollegeController(CollegeAppService collegeAppService)
        {
            CollegeAppService = collegeAppService;
        }
        public IActionResult Index()
        {
            return View(CollegeAppService.GetAll());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(College model)
        {
                CollegeAppService.Post(model);
                return RedirectToAction("Index", "College");
            
        }
        public IActionResult Edit(int id)
        {
            var model = CollegeAppService.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotAuthorized", "Home");
        }
        [HttpPost]
        public IActionResult Edit(College model)
        {
            if (CollegeAppService.Update(model))
                return RedirectToAction("Index");
           
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(CollegeAppService.Delete(id));
        }
    }
}