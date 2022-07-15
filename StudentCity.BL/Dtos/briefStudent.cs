using StudentCity.DAL.Models.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCity.BL.Dtos
{
    public class briefBuilding
    {
        public int BuildingNum { get; set; }
        public int BuildingId { get; set; }
        public IList<briefRoom> briefRoom { get; set; }
    }
    public class briefRoom
    {
        public int RoomNum { get; set; }
        public IList<briefStudent> briefStudent { get; set; }
    }

    public class briefStudent
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public College College { get; set; }
        public CityModel City { get; set; }
    }
}
