using System;
using System.Collections.Generic;
using StudentCity.BL;
using StudentCity.DAL.Models;

namespace StudentCity.Application
{
    public class ContactUsApp
    {
        private readonly ContactUsService _service;

        public ContactUsApp(ContactUsService service)
        {
            _service = service;
        }

        public List<ContactUs> GetAll()
        {
            // any gurd validation ands roles 
          return  _service.GetAll();
        }
        public ContactUs Post(ContactUs contactUs)
        {
            if (string.IsNullOrEmpty(contactUs.Name) || string.IsNullOrEmpty(contactUs.Mobile))
            {
                throw new Exception("Required feilds is missing");
            }
            // any gurd validation 
            return _service.Post(contactUs);
        }
    }
}
