using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCity.BL.Services.Utilities
{
    public class InvitationHandeling 
    {
        private readonly StudentService StudentService;
        private readonly RequestService RequestService;

        public InvitationHandeling(StudentService studentService, RequestService requestService)
        {
            StudentService = studentService;
            RequestService = requestService;
        }
        
        /************************************************************************************/
        public int AddRequest(int IdInviter, int IdInvitee)
        {
            var result = 0;

            var student1_db = StudentService.GetById(IdInviter);
            var student2_db = StudentService.GetById(IdInvitee);

            int student1_group= student1_db.GroupId;

            if(student1_group !=0)
                student1_group = StudentService.GetAll().Where(x => x.GroupId == (student1_group)).Count();

            if (student1_db != null && student2_db != null && student1_group<4)
            {
                var newRequest = new Request();

                newRequest.IdInviter = student1_db.Id;
                newRequest.IdInvitee = student2_db.Id;

                newRequest.IdInviterGroup = student1_db.GroupId;
                newRequest.IdInviteGroup = student2_db.GroupId;

                RequestService.Post(newRequest);
                result = 1;
            }
            return result;
        }

        /************************************************************************************/
        public int DeleteRequest(int idInviter, int idInvitee ,int key)
        {
            var result = 0;
            Request RequestResult = null;
            if (key == 1)
            {
                if (StudentService.GetById(idInvitee).GroupId == 0) // one to one invitation
                    RequestResult = RequestService.GetAll().SingleOrDefault(z => z.IdInviter == idInviter && z.IdInvitee == idInvitee);
                else // one to many invitation
                    RequestResult = RequestService.GetAll().SingleOrDefault(z => z.IdInviter == idInviter &&
                                     z.IdInviteGroup == (StudentService.GetById(idInvitee).GroupId));
            }
            else
            {
                if (StudentService.GetById(idInvitee).GroupId != 0)   // many to many invitation
                    RequestResult = RequestService.GetAll().SingleOrDefault(z =>
                                     z.IdInviterGroup == (StudentService.GetById(idInviter).GroupId) &&
                                     z.IdInviteGroup == (StudentService.GetById(idInvitee).GroupId));
                else // many to one invitation
                    RequestResult = RequestService.GetAll().SingleOrDefault(z =>
                                 z.IdInviterGroup == (StudentService.GetById(idInviter).GroupId) &&
                                 z.IdInvitee == idInvitee);
            }
                if (RequestResult != null)
            {
                RequestService.Delete(RequestResult.Id);
                result = 1;
            }
            return result;
        }

        /*******************************************************************************************/
        public bool InviteGroup(int IdJoiner, int IdGroup)
        {

            int group1_db = (StudentService.GetById(IdJoiner).GroupId == 0) ? 1 :
                                   StudentService.GetAll().Where(x => x.GroupId == (StudentService.GetById(IdJoiner).GroupId)).Count();
            int group2_db = StudentService.GetAll().Where(x => x.GroupId == IdGroup).Count();

            if ((group1_db + group2_db) < 5)
            {
                var newRequest = new Request();

                newRequest.IdInviter = IdJoiner;
                newRequest.IdInvitee = 0;

                newRequest.IdInviterGroup = StudentService.GetById(IdJoiner).GroupId;
                newRequest.IdInviteGroup = IdGroup;

                RequestService.Post(newRequest);
                return true;
            }
            return false;
        }
        /******************************************************************************************/

        public bool UnInviteGroup(int IdJoiner, int IdJoineeGroup)
        {
            var RequestResult = RequestService.GetAll().SingleOrDefault(z => z.IdInviter == IdJoiner && z.IdInviteGroup == IdJoineeGroup);
            if (RequestResult != null)
            {

                return RequestService.Delete(RequestResult.Id);
            }

            return false;
        }

            /******************************************************************************************/

            public List<List<StudentBehaviorProfile>> GetStudent(int id)
        {
            List<List<StudentBehaviorProfile>> returner = new List<List<StudentBehaviorProfile>>();
            List<StudentBehaviorProfile> topStudent = new List<StudentBehaviorProfile>();
            StudentBehaviorProfile temp = new StudentBehaviorProfile { };

            IEnumerable<Request> inviters =null;

            if (StudentService.GetById(id).GroupId ==0)
                inviters = RequestService.GetAll().Where(x => x.IdInvitee == id);
            else
                inviters = RequestService.GetAll().Where(x => x.IdInviteGroup == StudentService.GetById(id).GroupId);

            if (inviters.Count()>0)
            {
                int converter = inviters.FirstOrDefault().IdInviterGroup;

                foreach (var inviter in inviters)
                {
                    var group_db = StudentService.GetAll().Where(w => (w.Id == inviter.IdInviter) || (w.GroupId== inviter.IdInviterGroup && inviter.IdInviterGroup!=0));
                    //if (converter != temp.groupId)
                    //{
                    //    returner.Add(topStudent);
                    //    topStudent = new List<StudentBehaviorProfile>();
                    //    converter = temp.groupId;
                    //}
                    foreach (var Student_db in group_db)
                    {
                        if (converter != Student_db.GroupId)
                        {
                            returner.Add(topStudent);
                            topStudent = new List<StudentBehaviorProfile>();
                            converter = Student_db.GroupId;
                        }
                        temp.Student = Student_db;
                        temp.Details = false;
                        topStudent.Add(temp);

                        temp = new StudentBehaviorProfile { };
                    }
                   
                }
                returner.Add(topStudent);
            }
            return returner;
        }

        /******************************************************************************************/

       
    }
}
