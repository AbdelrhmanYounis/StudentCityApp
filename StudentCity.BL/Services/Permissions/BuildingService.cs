using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Permissions
{
    public class BuildingService
    {
        private readonly BuildingRepository _repository;

        public BuildingService(BuildingRepository repository)
        {
            _repository = repository;
        }

        public List<Building> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Building GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Building Post(Building model)
        {
            return _repository.Add(model);
        }
        public bool Update(Building model)
        {
            return (_repository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _repository.DeleteObj(id);
        }
    }
}
