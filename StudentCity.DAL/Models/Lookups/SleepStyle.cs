using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class SleepStyle:BaseModel
    {
        public int Is_SL_Lightness { get; set; }
        public int Is_SL_Noisy { get; set; }
        public int Is_SL_Deep { get; set; }
        public int Is_SL_Night { get; set; }
        public int Is_SL_Continuity { get; set; }
    }
}
