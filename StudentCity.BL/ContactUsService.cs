using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models;
using StudentCity.DAL.Repos;

namespace StudentCity.BL
{
    public class ContactUsService
    {
        private readonly ContactUsRepository _reoRepository;
        public ContactUsService(ContactUsRepository reoRepository)
        {
            _reoRepository = reoRepository;
        }

        public List<ContactUs> GetAll()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    _reoRepository.Add(new Country
            //    {
            //        NameAr = "مصر "  + i  , 
            //        NameEn = "Egypt " + i , 
            //        CreatedAt = DateTime.Now
            //    });
            //}
            return _reoRepository.GetAll().ToList();
        }

        public ContactUs Post(ContactUs contactUs)
        {
           return _reoRepository.Add(contactUs);
        }
    }
}
