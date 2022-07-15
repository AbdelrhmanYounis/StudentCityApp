using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Lookups
{
    public class StudyWayService
    {
        private readonly StudyWayRepository _reoRepository;
        public StudyWayService(StudyWayRepository StudyWayRepository)
        {
            _reoRepository = StudyWayRepository;
            //SeedCountries();
        }
       
        public List<StudyWay> GetAll()
        {
            return _reoRepository.GetAll().ToList();
        }

        public StudyWay GetById(int id)
        {
            return _reoRepository.GetById(id);
        }

        public StudyWay Post(StudyWay StudyWay)
        {
           return _reoRepository.Add(StudyWay);
        }
        public bool Update(StudyWay model)
        {
            return (_reoRepository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _reoRepository.DeleteObj(id);
        }
    }
}
