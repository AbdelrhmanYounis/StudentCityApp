using System.Collections.Generic;

namespace StudentCityUI.Dto
{
    public class BuildingData
    {
        public int building { get; set; }
        public List<data> data { get; set; }
    }
    public class data
    {
        public string name { get; set; }
        public double y  { get; set; }
        public string drilldown { get; set; }
    }
}
