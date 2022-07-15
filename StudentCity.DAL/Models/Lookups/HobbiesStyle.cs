using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class HobbiesStyle:BaseModel
    {
        public bool Is_H_Sport { get; set; }
        public bool Is_H_Arts { get; set; }
        public bool Is_H_sociaty { get; set; }
        public bool Is_H_Culture { get; set; }
        public bool Is_H_Religion { get; set; }
    }
}
