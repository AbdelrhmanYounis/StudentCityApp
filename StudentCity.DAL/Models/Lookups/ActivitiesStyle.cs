using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class ActivitiesStyle : BaseModel
    {
       
        public bool Is_AC_LoveGames { get; set; }
        public bool Is_AC_LoveHargingOut { get; set; }
        public bool Is_AC_QuranTimeSpecialization { get; set; }
        public bool Is_AC_PrayingInMosque { get; set; }
    }
}
