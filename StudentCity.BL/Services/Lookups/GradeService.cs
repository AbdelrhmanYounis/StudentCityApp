using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Lookups
{
    public class GradeService
    {
        private readonly GradeRepository _reoRepository;
        public GradeService(GradeRepository GradeRepository)
        {
            _reoRepository = GradeRepository;
            //SeedCountries();
        }
       
        public List<Grade> GetAll()
        {
            return _reoRepository.GetAll().ToList();
        }

        public Grade GetById(int id)
        {
            return _reoRepository.GetById(id);
        }

        public Grade Post(Grade Grade)
        {
           return _reoRepository.Add(Grade);
        }
        public bool Update(Grade model)
        {
            return (_reoRepository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _reoRepository.DeleteObj(id);
        }
    }
}
