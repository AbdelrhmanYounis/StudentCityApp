using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Shared;
using StudentCity.DAL.Utilities;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos
{
   
    public class RequestRepository : GenericRepository<Request>
    {
        public RequestRepository(IOptions<DbSettings> settings) : base(settings, "Requests")
        {
        }

    }
}
