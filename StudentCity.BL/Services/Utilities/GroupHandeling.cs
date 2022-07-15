
using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCity.BL.Services.Utilities
{
    public class GroupHandeling 
    {
        private readonly StudentService StudentService;
        private readonly RequestService RequestService;
        public GroupHandeling(StudentService studentService, RequestService requestService)
        {
            StudentService = studentService;
            RequestService = requestService;
        }

        /*********************************************************************************************************/

        public bool JoinGroup(int IdInviter ,int IdInvitee, int key)
        {
            var IdInvitee_db = StudentService.GetById(IdInvitee);
            var IdInviter_db= StudentService.GetById(IdInviter);
            IEnumerable<Request> request_db = null;
            if (IdInvitee_db != null && IdInviter_db != null)
            {
                if (key == 1 && IdInvitee_db.GroupId == 0) //create group
                {
                    request_db = RequestService.GetAll().Where(q => q.IdInvitee == IdInvitee || q.IdInvitee == IdInviter || q.IdInviter == IdInvitee || q.IdInviter == IdInviter);
                    foreach (var requesting in request_db)
                    {
                        RequestService.Delete(requesting.Id);
                    }
                    IdInviter_db.GroupId = IdInvitee_db.GroupId = StudentService.GetAll().Max(x => x.GroupId) + 1;
                    StudentService.Update(IdInvitee_db);
                    return StudentService.Update(IdInviter_db);
                }

                else
                {
                     var IdInviteeGroup = (IdInvitee_db.GroupId != 0)?StudentService.GetAll().Where(x => x.GroupId == IdInvitee_db.GroupId):
                                                                      StudentService.GetAll().Where(x => x.Id == IdInvitee);
                     var IdInviterGroup = (key != 1) ? StudentService.GetAll().Where(x => x.GroupId == IdInviter_db.GroupId) :
                                                       StudentService.GetAll().Where(x => x.Id == IdInviter);
                   
                    if ((IdInviterGroup.Count() + IdInviteeGroup.Count()) < 5)
                    {

                        request_db = (IdInvitee_db.GroupId != 0 && key == 1)? RequestService.GetAll().Where(q => q.IdInviteGroup == IdInvitee_db.GroupId || q.IdInviter == IdInviter):
                                     (IdInvitee_db.GroupId != 0 && key != 1)? RequestService.GetAll().Where(q => q.IdInviteGroup == IdInvitee_db.GroupId || q.IdInviterGroup == IdInviter_db.GroupId):
                                                                              RequestService.GetAll().Where(q => q.IdInvitee == IdInvitee || q.IdInviterGroup == IdInviter_db.GroupId);
                        foreach (var requesting in request_db)
                        {
                            RequestService.Delete(requesting.Id);

                        }
                        if (key != 1)     //join to inviter group
                            foreach (var student in IdInviteeGroup)
                            {

                                student.GroupId = IdInviter_db.GroupId;
                                StudentService.Update(student);

                            }
                        else  //join to invitee group
                            IdInviter_db.GroupId = IdInvitee_db.GroupId;
                    }

                    return StudentService.Update(IdInviter_db);
                }

            }
            return false;
        }
        /**********************************************************************/
        public List<List<StudentBehaviorProfile>> GetStudent(int id)
        {
            List<List<StudentBehaviorProfile>> returner = new List<List<StudentBehaviorProfile>>();
            List<StudentBehaviorProfile> topStudent = new List<StudentBehaviorProfile>();
            StudentBehaviorProfile temp = new StudentBehaviorProfile { };
            SortingClass sortingClass = new SortingClass();
            try
            {
                double[,] CounteringReturn = Countering(id);
                int groupSize = CounteringReturn.GetLength(0);
                // for heap sort
                double[,] topArr = sortingClass.sort(CounteringReturn);
                // for quick sort
                //double[,] topArr = sortingClass.quickSort(CounteringReturn, 0, (groupSize > 4) ? 4 : groupSize - 1);
                if (groupSize > 4)
                    topArr = sortingClass.updateSort(topArr, 5, 5, groupSize - 1);

                for (int q = 0, c; q < topArr.GetLength(0);q++)
                {
                    var group_list = StudentService.GetAll().Where(w => w.GroupId == topArr[q, 0]);
                    c = 0;
                    double countainer = 0.0;
                    foreach (var Student_db in group_list)
                    {
                        
                        temp.Student = Student_db;

                        countainer += Math.Round(topArr[q, c] / CounterClass.calculateDivider(StudentService.GetById(id)), 2);
                        temp.Details = false;
                        temp.State = (RequestService.GetAll().Where(x => x.IdInviter == id && x.IdInviteGroup == Student_db.GroupId).Count() > 0) ? true : false;
                        topStudent.Add(temp);

                        temp = new StudentBehaviorProfile { };
                        c++;
                    }
                    topStudent.FirstOrDefault().Counter = countainer;
                    returner.Add(topStudent);
                    countainer = 0.0;
                    topStudent = new List<StudentBehaviorProfile>();
                }
                return returner;
            }
            catch
            {
                return returner;
            }
        }

        /*******************************************************************************************/

        public double[,] Countering(int Id)
        {
            var student_recommended = StudentService.GetById(Id);
            try
            {
                var NumOfGroup = student_recommended.Building.R_Students_num;
                var StudentsList = StudentService.GetAll().Where(x => x.GroupId != 0
                && (x.GroupId != student_recommended.GroupId)
                && (StudentService.GetAll().Where(w => w.GroupId == x.GroupId).Count() != NumOfGroup)
                                                          ).OrderBy(x => x.GroupId).ToList();

                double[,] arr = new double[StudentsList.Select(m => m.GroupId).Distinct().Count(), 2];
                int indexer = 0;
                int studentGroupId = 0;
                int value = 0;
                int numInGroup = 0;

                if (student_recommended != null && StudentsList.Count() > 0)
                {
                    int check = StudentsList.FirstOrDefault().GroupId;
                    CounterClass counterClass = new CounterClass();
                    foreach (var student in StudentsList)
                    {
                        studentGroupId = student.GroupId;
                        if (check != studentGroupId)
                        {

                            arr[indexer, 0] = check;
                            arr[indexer, 1] = (value * 1.0) / (numInGroup * 1.0);

                            check = studentGroupId;
                            numInGroup = value = 0;
                            indexer++;
                        }
                        value += counterClass.counter(student_recommended, student);
                        numInGroup++;
                    }

                    arr[indexer, 0] = studentGroupId;
                    arr[indexer, 1] = (value * 1.0) / (numInGroup * 1.0);
                }
                return arr;
            }
            catch {

                return null;
            }
        }
    }
}
