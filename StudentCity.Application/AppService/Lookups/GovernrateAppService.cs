using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class GovernrateAppService
    {
        private readonly GovernrateService _service;

        public GovernrateAppService(GovernrateService service)
        {
            _service = service;
        }

      
        public List<Governrate> GetAll()
        {
            return _service.GetAll();
        }
        public Governrate GetById(int id)
        {
            return _service.GetById(id);
        }
        public Governrate Post(Governrate model)
        {
            // any gurd validation 
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameAr, "NameAr");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.NameEn, "NameEn");
            return _service.Post(model);
        }
        public bool Update(Governrate model)
        {
            return _service.Update(model);
        }
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
