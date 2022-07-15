using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Shared;
using System;

namespace StudentCity.DAL.Models.Permissions
{
    public class Student :BaseModel
    {
        public  UserModel user { get; set; }

        public SleepStyle Sleep_Style { get; set; }
       
        public FoodStyle Food_Style { get; set; }
      
        public  StudyStyle Study_Style { get; set; }
       
        public  ActivitiesStyle Activities_Style { get; set; }
       
        public  HobbiesStyle Hobbies_Style { get; set; }
        
        public  CharacterStyle Character_Style { get; set; }
        
        public  CityModel city { get; set; }
        
        public  College College { get; set; }

        public  Building Building { get; set; }
        public DateTime LastMeal { get; set; }
        public Priority Priority { get; set; }
        public int GroupId { get; set; }

        public int RoomNum { get; set; }

        public int RoomKey { get; set; }
    }
    public class Priority
    {
        public int College { get; set; }
        public int Address { get; set; }
        public int Sleep { get; set; }
        public int Study { get; set; }
        public int Food { get; set; }
        public int Character { get; set; }
        public int Hobby { get; set; }
    }
    public enum PriorityType
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
