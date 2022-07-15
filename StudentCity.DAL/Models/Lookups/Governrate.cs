using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class Governrate:BaseModel
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public CountryModel Country { get; set; }
    }
}
