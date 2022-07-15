using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Permissions
{
    public class Building : BaseModel
    {
        public int B_Number { get; set; } //building number 5
        public int B_Floors_Num { get; set; }  //building have 6 floors
        public int F_Rooms_num { get; set; }  //floor have 20 rooms
        public int R_Students_num { get; set; }  //room have 4 Students
    }
}
