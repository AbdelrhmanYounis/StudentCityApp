using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Services.Lookups;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class CollegeAppService
    {
        private readonly CollegeService _service;

        public CollegeAppService(CollegeService service)
        {
            _service = service;
        }
        
        public List<College> GetAll()
        {
            return _service.GetAll();
        }
        public College GetById(int id)
        {
            return _service.GetById(id);
        }
        public College Post(College model)
        {
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameAr, "NameAr");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameEn, "NameEn");
            return _service.Post(model);
        }
        public bool Update(College CollegeModel)
        {
            return _service.Update(CollegeModel);
        }
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
