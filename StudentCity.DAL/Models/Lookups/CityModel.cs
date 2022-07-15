using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class CityModel : BaseModel
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        
        public Governrate Governrate { get; set; }

    }
}
