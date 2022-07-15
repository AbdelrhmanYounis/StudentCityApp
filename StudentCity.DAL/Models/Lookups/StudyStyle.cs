using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class StudyStyle:BaseModel
    {
        public int Is_ST_Continuity { get; set; }
        public int Is_ST_Individual { get; set; }
        public int Is_ST_AttendingCollege { get; set; }
        public StudyWay study_way { get; set; }
    }
}
