using StudentCity.DAL.Shared;

namespace StudentCity.DAL.Models
{
    public class ContactUs : BaseModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
