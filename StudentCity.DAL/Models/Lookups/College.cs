using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class College :BaseModel
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int LevelNum { get; set; }

        public Grade Grade { get; set; }
        public int Level { get; set; }
    }
}
