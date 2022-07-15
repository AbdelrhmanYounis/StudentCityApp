using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Shared;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos.Lookups
{
   public class CityRepository  : GenericRepository<CityModel>
    {
        public CityRepository(IOptions<DbSettings> settings): base(settings, "Cities")
        {
        }
    }
}
