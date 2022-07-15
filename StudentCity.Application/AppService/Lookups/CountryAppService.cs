using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Services.Lookups;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class CountryAppService
    {
        private readonly CountryService _service;

        public CountryAppService(CountryService service)
        {
            _service = service;
        }

      
        public List<CountryModel> GetAll()
        {
            return _service.GetAll();
        }
        public CountryModel GetById(int id)
        {
            return _service.GetById(id);
        }
        public CountryModel Post(CountryModel model)
        {
            // any gurd validation 
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameAr, "NameAr");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameEn, "NameEn");
            return _service.Post(model);
        }
        public bool Update(CountryModel model)
        {
            return _service.Update(model);
        }
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
