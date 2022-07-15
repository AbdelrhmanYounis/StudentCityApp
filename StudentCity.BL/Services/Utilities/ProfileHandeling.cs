using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCity.BL.Services.Utilities
{
    public class ProfileHandeling
    {
        private readonly StudentService StudentService;

        public ProfileHandeling(StudentService studentService)
        {

            StudentService = studentService;
        }
        /************************************************************************************/
        public bool keyAction(int StudentLoginId)
        {
            var Student_db = StudentService.GetById(StudentLoginId);
            if(Student_db !=null && Student_db.RoomNum == 0)
            {
                Student_db.RoomKey = (Student_db.RoomKey == 0) ?1:0;
                return StudentService.Update(Student_db);
            }
            return false;
        }
        /************************************************************************************/
        public bool UnJion(int StudentLoginId)
        {
            var Student_db = StudentService.GetById(StudentLoginId);
            if (Student_db != null && Student_db.RoomNum==0)
            {
                var studentsInGroup= StudentService.GetAll().Where(x => x.GroupId == Student_db.GroupId);
                if (studentsInGroup.Count() > 2)
                {
                    Student_db.GroupId = 0;
                    StudentService.Update(Student_db);
                }
                else
                {
                    foreach(var student in studentsInGroup)
                    {
                        student.GroupId = 0;
                        StudentService.Update(student);
                    }
                }
                return true;
            }
            return false;
        }
        /************************************************************************************/
        public IEnumerable<StudentBehaviorProfile> GetStudent(int id)
        {
            
            var Student_db = StudentService.GetById(id);
            if (Student_db != null)
            {
                var group_db = (Student_db.GroupId > 0) ? StudentService.GetAll().Where(x => x.GroupId == Student_db.GroupId) :
                                                          StudentService.GetAll().Where(x => x.Id == id);

                StudentBehaviorProfile[] topStudent = new StudentBehaviorProfile[group_db.Count()];
                int counter = 1;

                foreach (var student in group_db)
                {
                    
                    StudentBehaviorProfile sbp = new StudentBehaviorProfile
                    {
                       Student=student
                    };
                    if (student.Id == id) //loginer profile
                        topStudent[0] = sbp;

                    else       //loginer group
                    {
                        topStudent[counter] = sbp;
                        counter++;
                    }
                }
                return topStudent;
            }
            return null;
        }
    }
}
