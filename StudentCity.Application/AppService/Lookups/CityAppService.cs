using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Services.Lookups;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class CityAppService
    {
        private readonly CityService _service;

        public CityAppService(CityService service)
        {
            _service = service;
        }
        
        public List<CityModel> GetAll()
        {
            return _service.GetAll();
        }
        public CityModel GetById(int id)
        {
            return _service.GetById(id);
        }
        public CityModel Post(CityModel model)
        {
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameAr, "NameAr");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameEn, "NameEn");
            return _service.Post(model);
        }
        public bool Update(CityModel cityModel)
        {
            return _service.Update(cityModel);
        }
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
