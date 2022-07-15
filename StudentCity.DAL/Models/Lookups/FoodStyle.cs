using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Lookups
{
    public class FoodStyle :BaseModel
    {
        public int Is_F_Individual { get; set; }
        public int Is_F_FastFood { get; set; }
    }
}
