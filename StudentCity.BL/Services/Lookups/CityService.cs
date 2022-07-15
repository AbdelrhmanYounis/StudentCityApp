using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Lookups
{
    public class CityService
    {
        private readonly CityRepository _reoRepository;
        public CityService(CityRepository cityRepository)
        {
            _reoRepository = cityRepository;
            //SeedCountries();
        }
       
        public List<CityModel> GetAll()
        {
            return _reoRepository.GetAll().ToList();
        }

        public CityModel GetById(int id)
        {
            return _reoRepository.GetById(id);
        }

        public CityModel Post(CityModel cityModel)
        {
           return _reoRepository.Add(cityModel);
        }
        public bool Update(CityModel model)
        {
            return (_reoRepository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _reoRepository.DeleteObj(id);
        }
    }
}
