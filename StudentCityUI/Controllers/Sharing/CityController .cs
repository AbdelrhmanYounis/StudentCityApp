using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCity.Application.AppService;
using StudentCity.DAL.Models.Lookups;
using StudentCityUI.Dto;
using StudentCityUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudentCityUI.Controllers.Sharing
{
    [UserIsAuthenticated]
    public class CityController : Controller
    {
        public CityAppService CityAppService { get; }
        public GovernrateAppService GovernrateAppService { get; }
        public CountryAppService CountryAppService { get; }

        public CityController(CityAppService cityAppService , GovernrateAppService governrateAppService , CountryAppService countryAppService)
        {
            CityAppService = cityAppService;
            GovernrateAppService = governrateAppService;
            CountryAppService = countryAppService;
        }      
        public IActionResult Index(int id)
        {
            ViewBag.GovernrateId = id;
            if (id == 0)
            {
                return View(CityAppService.GetAll());
            }
            return View(CityAppService.GetAll().Where(x => x.Governrate.Id == id));
        }
        
        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.GovernrateId = id;
            if (id == 0)
            {
                ViewBag.countries = CountryAppService.GetAll();
               
            }

            return View();
        }
        [HttpPost]
        public IActionResult Add(CityModel model)
        {
                if (model.Governrate.Country != null)
                {
                    var Governrate = GovernrateAppService.GetById(model.Governrate.Id);
                    var country = CountryAppService.GetById(model.Governrate.Country.Id);
                    if (Governrate != null && country != null)
                    {
                        model.Governrate = Governrate;
                        model.Governrate.Country = country;
                        CityAppService.Post(model);
                    }
                    return RedirectToAction("Index/0");
                }

                var newGovernrate = GovernrateAppService.GetById(model.Governrate.Id);
                if (newGovernrate != null )
                {
                    model.Governrate = newGovernrate;
                    CityAppService.Post(model);
                return RedirectToAction("Index/" + model.Governrate.Id);
            }   
            return BadRequest();
        }

        public IActionResult GetGovernrate(int id)
        {
            var Governrate = GovernrateAppService.GetAll().Where(x=>x.Country.Id==id);
            if (Governrate != null)
            {
                return Json(Governrate);
            }
            return Json(false);
        }
        [HttpGet]
        public IActionResult Edit(int id, int navigate)
        {
            ViewBag.GovernrateId = navigate;

            if (navigate == 0)
            {
                ViewBag.countries = CountryAppService.GetAll();

                ViewBag.Governrates = GovernrateAppService.GetAll().Where(x => x.Country.Id ==
                 CountryAppService.GetAll().FirstOrDefault().Id
                );
            }

            var model = CityAppService.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotAuthorized", "Home");
        }
        [HttpPost]
        public IActionResult Edit(CityModel model)
        {
            if (model.Governrate != null)
            {
                var Governrate = GovernrateAppService.GetById(model.Governrate.Id);
                var country = CountryAppService.GetById(model.Governrate.Country.Id);
                if (Governrate != null && country != null)
                {
                    model.Governrate = Governrate;
                    model.Governrate.Country = country;
                    if (CityAppService.Update(model))
                        return RedirectToAction("Index/0");
                }
                return RedirectToAction("Index/0");
            }

            var newGovernrate = GovernrateAppService.GetById(model.Governrate.Id);
                if (newGovernrate != null)
                {
                    model.Governrate = newGovernrate;
                    if (CityAppService.Update(model))
                        return RedirectToAction("Index/" + newGovernrate.Id);
                }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(CityAppService.Delete(id));
        }
    }
}