using StudentCity.DAL.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCity.BL.Dtos
{
    public class StudentBehaviorProfile
    {
        public Student Student { get; set; }
        public bool Details { get; set; }
        public double Counter { get; set; }
        public bool State { get; set; }
    }
}
