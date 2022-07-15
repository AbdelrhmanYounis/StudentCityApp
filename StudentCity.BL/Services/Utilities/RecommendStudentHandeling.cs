using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using StudentCity.BL.Services.Utilities;
using StudentCity.DAL.Repos;

namespace StudentCity.BL.Services.Utilities
{
    public class RecommendStudentHandeling 
    {
        private readonly StudentService StudentService;
        private readonly RequestService RequestService;

        public RecommendStudentHandeling(StudentService studentService, RequestService requestService)
        {
            StudentService = studentService;
            RequestService = requestService;
        }

        /*******************************************************************************************/

        public IEnumerable<StudentBehaviorProfile> GetStudent(int Id)
        {
            if (StudentService.GetById(Id).RoomNum == 0)
            {
                List<StudentBehaviorProfile> topStudent = new List<StudentBehaviorProfile>();
                StudentBehaviorProfile temp = new StudentBehaviorProfile { };
                SortingClass sortingClass = new SortingClass();
               
                /*
                int numStdNotGrouped = StudentService.GetAll().Where(x => x.GroupId == 0 && x.Id != Id && x.Building != null && x.Building.Id ==StudentService.GetById(Id).Building.Id).Count();*/
                double[,] CounteringArr = Countering(Id);
                /*
                double[,] topArr = sortingClass.sort(Countering(Id));// for heap sort
                */
                double[,] topArr = sortingClass.quickSort(CounteringArr, 0, CounteringArr.Length>11?12: CounteringArr.Length);//5 students from 0 to 4
                /*
                if (numStdNotGrouped > 5)//more than 5 students from 5 to allStudents -1
                    topArr = sortingClass.updateSort(topArr, 5, 5, numStdNotGrouped - 1);
                   */
                for (int q = 0; q < (CounteringArr.Length > 11 ? 12 : CounteringArr.Length); q++)
                {
                    temp.Student = StudentService.GetById(int.Parse((topArr[q, 0]).ToString()));

                    temp.Counter = Math.Round(topArr[q, 1] /CounterClass.calculateDivider(StudentService.GetById(Id)), 2);
                    temp.State = (RequestService.GetAll().Where(x => x.IdInviter == Id && x.IdInvitee == temp.Student.Id).Count() > 0) ? false : true;
                    temp.Details = false;

                    topStudent.Add(temp);

                    temp = new StudentBehaviorProfile { };
                }
                return topStudent;
            }
            return null;
        }


        /*******************************************************************************************/

        public double[,] Countering(int Id)
        {
            //+ students in his building only + roomNum = 0 + not include him
            var student_recommended = StudentService.GetById(Id);
            var StudentsList = StudentService.GetAll().Where(x => x.GroupId == 0 && x.Id!=Id && x.Building !=null && x.Building.Id== student_recommended.Building.Id).ToList();
           
            double[,] arr = new double[StudentsList.Count(),2];
            int indexer = 0;
            CounterClass counterClass = new CounterClass();
            if (student_recommended != null)
                foreach (var student in StudentsList)
                {
                    arr[indexer,0] = student.Id;
                    arr[indexer,1] = counterClass.counter(student_recommended, student); 
                   
                    indexer++;
                }
            return arr;
        }
    }
}
