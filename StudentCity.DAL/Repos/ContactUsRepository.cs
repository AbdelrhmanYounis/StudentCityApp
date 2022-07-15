using StudentCity.DAL.Models;
using StudentCity.DAL.Shared;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos
{
   public class ContactUsRepository : GenericRepository<ContactUs>
    {
        // countries --> is my table name
        public ContactUsRepository(IOptions<DbSettings> settings) : base(settings,"contactUs")
        {
        }
    }
}
