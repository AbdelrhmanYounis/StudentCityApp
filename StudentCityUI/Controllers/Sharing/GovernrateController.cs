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
    public class GovernrateController : Controller
    {
        public GovernrateAppService GovernrateAppService { get; }
        public CountryAppService CountryAppService { get; }

        public GovernrateController(GovernrateAppService governrateAppService, CountryAppService countryAppService)
        {
            GovernrateAppService = governrateAppService;
            CountryAppService = countryAppService;
        }
        public IActionResult Index(int id)
        {
            ViewBag.CountryId = id;
            if (id == 0)
            {
                return View(GovernrateAppService.GetAll());
            }
            return View(GovernrateAppService.GetAll().Where(x => x.Country.Id == id));
        }

        //used to back from cities to Governrate
        public IActionResult IndexBack(int id)
        {
            var countryId = GovernrateAppService.GetById(id).Country.Id;
            ViewBag.CountryId = countryId;
            return View("Index", GovernrateAppService.GetAll().Where(x => x.Country.Id == countryId));
        }
        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.CountryId= id;
            if (id == 0)
            {
                ViewBag.countries = CountryAppService.GetAll();

            }
            return View();
        }
        [HttpPost]
        public IActionResult Add(Governrate model)
        {
            CountryModel country = null;

            if (model.Country != null)
            {
                country = CountryAppService.GetById(model.Country.Id);
                if (country != null)
                {
                model.Country = country;
                GovernrateAppService.Post(model);
                return RedirectToAction("Index/0");
                }
            }
            else
            {
                country = CountryAppService.GetById(model.Id);
                if (country != null)
                {
                    model.Country = country;
                    GovernrateAppService.Post(model);
                    return RedirectToAction("Index/" + model.Country.Id);
                }
            }
            
            return BadRequest();
        }
        [HttpGet]
        public IActionResult Edit(int id ,int navigate)
        {
            ViewBag.CountryId = navigate;
            if (navigate == 0)
            {
                ViewBag.countries = CountryAppService.GetAll();

            }
            var model = GovernrateAppService.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotAuthorized", "Home");
        }
        [HttpPost]
        public IActionResult Edit(Governrate model)
        {
            CountryModel country = null;

            if (model.Country != null)
            {
                country = CountryAppService.GetById(model.Country.Id);
                if (country != null)
                {
                    model.Country = country;
                    if (GovernrateAppService.Update(model))
                        return RedirectToAction("Index/0");
                }
            }
            else
            {
                country = CountryAppService.GetById(model.Id);
                if (country != null)
                {
                    model.Country = country;
                    if (GovernrateAppService.Update(model))
                        return RedirectToAction("Index/" + country.Id);
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(GovernrateAppService.Delete(id));
        }
    }
}