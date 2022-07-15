using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Shared;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos.Lookups
{
   public class CountryRepository  : GenericRepository<CountryModel>
    {
        // countries --> is my table name
        public CountryRepository(IOptions<DbSettings> settings): base(settings, "Countries")
        {
        }
    }
}
