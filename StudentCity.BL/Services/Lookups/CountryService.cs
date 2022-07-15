using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Lookups
{
    public class CountryService
    {
        private readonly CountryRepository _reoRepository;
        public CountryService(CountryRepository countryRepository)
        {
            _reoRepository = countryRepository;
            //SeedCountries();
        }
       
        public List<CountryModel> GetAll()
        {
            return _reoRepository.GetAll().ToList();
        }

        public CountryModel GetById(int id)
        {
            return _reoRepository.GetById(id);
        }

        public CountryModel Post(CountryModel countryModel)
        {
           return _reoRepository.Add(countryModel);
        }
        public bool Update(CountryModel model)
        {
            return (_reoRepository.InsertOrUpdate(x => x.Id == model.Id, model) != null) ? true : false;
        }
        public bool Delete(int id)
        {
            return _reoRepository.DeleteObj(id);
        }
    }
}
