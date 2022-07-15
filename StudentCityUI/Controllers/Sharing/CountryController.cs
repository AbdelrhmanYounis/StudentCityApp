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
    public class CountryController : Controller
    {
        public CountryAppService CountryAppService { get; }

        public CountryController(CountryAppService countryAppService)
        {
            CountryAppService = countryAppService;
        }
        public IActionResult Index()
        {
            return View(CountryAppService.GetAll());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CountryModel model)
        {
                CountryAppService.Post(model);
                return RedirectToAction("Index", "Country");
            
        }
        public IActionResult Edit(int id)
        {
            var model = CountryAppService.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotAuthorized", "Home");
        }
        [HttpPost]
        public IActionResult Edit(CountryModel model)
        {
            if (CountryAppService.Update(model))
                return RedirectToAction("Index");
           
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(CountryAppService.Delete(id));
        }
    }
}