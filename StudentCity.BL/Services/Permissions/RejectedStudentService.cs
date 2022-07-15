using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Permissions
{
    public class RejectedStudentService
    {
        private readonly RejectedStudentRepository _repository;

        public RejectedStudentService(RejectedStudentRepository repository)
        {
            _repository = repository;
        }

        public List<RejectedStudentModel> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public bool GetBySSN(string SSN)
        {
            return GetAll().Any(Q=>Q.SSN ==SSN);
        }

        public RejectedStudentModel Post(RejectedStudentModel model)
        {
            return _repository.Add(model);
        }
       
        public bool Delete(int id)
        {
            return _repository.DeleteObj(id);
        }
    }
}
