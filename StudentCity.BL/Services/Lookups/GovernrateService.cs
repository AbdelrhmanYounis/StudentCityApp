using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Permissions
{
    public class GovernrateService
    {
        private readonly GovernrateRepository _repository;

        public GovernrateService(GovernrateRepository repository)
        {
            _repository = repository;
        }

        public List<Governrate> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Governrate GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Governrate Post(Governrate model)
        {
            return _repository.Add(model);
        }
        public bool Update(Governrate model)
        {
            return (_repository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _repository.DeleteObj(id);
        }
    }
}
