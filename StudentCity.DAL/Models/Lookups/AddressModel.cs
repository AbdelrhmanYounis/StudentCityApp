using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class AddressModel : BaseModel
    {
        public string StreetName { get; set; }
        public string BuildingNum { get; set; }
        public string ZipCode { get; set; }

        public CityModel City { get; set; }
    }
}
