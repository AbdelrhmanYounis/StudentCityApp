using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Shared;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos
{
   
    public class ResetCodeRepository : GenericRepository<ResetCodeModel>
    {
        public ResetCodeRepository(IOptions<DbSettings> settings) : base(settings, "ResetCode")
        {
        }

    }
}
