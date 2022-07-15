using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Permissions
{
    public class StudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Student GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Student Post(Student model)
        {
            return _repository.Add(model);
        }
        public bool Update(Student model)
        {
            return (_repository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _repository.DeleteObj(id);
        }
    }
}
