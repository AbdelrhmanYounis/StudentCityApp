using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using System.Collections.Generic;
using System.Linq;

namespace StudentCity.BL.Services.Permissions
{
    public class RequestService
    {
        private readonly RequestRepository _repository;

        public RequestService(RequestRepository repository)
        {
            _repository = repository;
        }

        public List<Request> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Request GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Request Post(Request model)
        {
            return _repository.Add(model);
        }
        public bool Update(Request model)
        {
            return (_repository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _repository.DeleteObj(id);
        }
    }
}
