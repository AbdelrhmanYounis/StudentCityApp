using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Lookups
{
    public class CollegeService
    {
        private readonly CollegeRepository _reoRepository;
        public CollegeService(CollegeRepository CollegeRepository)
        {
            _reoRepository = CollegeRepository;
            //SeedCountries();
        }
       
        public List<College> GetAll()
        {
            return _reoRepository.GetAll().ToList();
        }

        public College GetById(int id)
        {
            return _reoRepository.GetById(id);
        }

        public College Post(College CollegeModel)
        {
           return _reoRepository.Add(CollegeModel);
        }
        public bool Update(College model)
        {
            return (_reoRepository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _reoRepository.DeleteObj(id);
        }
    }
}
