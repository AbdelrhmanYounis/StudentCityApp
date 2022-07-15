using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class BuildingAppService
    {
        private readonly BuildingService _service;

        public BuildingAppService(BuildingService service)
        {
            _service = service;
        }
        
        public List<Building> GetAll()
        {
            return _service.GetAll();
        }
        public Building GetById(int id)
        {
            return _service.GetById(id);
        }
        public Building Post(Building model)
        {
            return _service.Post(model);
        }
        public bool Update(Building model)
        {
            return _service.Update(model);
        }
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
