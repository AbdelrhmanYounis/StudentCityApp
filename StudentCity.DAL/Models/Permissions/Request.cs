using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models.Permissions
{
    public class Request :BaseModel
    {
        public int IdInviter { get; set; }

        public int IdInvitee { get; set; }

        public int IdInviterGroup { get; set; }

        public int IdInviteGroup { get; set; }

        public string Message { get; set; }
    }
}
