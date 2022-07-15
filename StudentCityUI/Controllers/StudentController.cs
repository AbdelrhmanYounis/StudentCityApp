using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentCity.BL.Services.Utilities;
using StudentCity.BL.Dtos;
using StudentCityUI.Models;
using StudentCityUI.Dto;
using StudentCityUI.Helpers;
using StudentCity.Application.AppService;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Permissions;

namespace StudentCityUI.Controllers
{
   
    public class StudentController : Controller
    {
        public UserAppService UserAppService { get; }
        private readonly StudentService StudentService;
        private readonly ProfileHandeling ProfileHandeling;
        private readonly InvitationHandeling InvitationHandeling;
        private readonly GroupHandeling GroupHandeling;
        private readonly ReservingRoomHandeling ReservingRoomHandeling;
        private readonly RecommendStudentHandeling RecommendStudentHandeling; 

        public StudentController(UserAppService userAppService , ProfileHandeling profileHandeling , InvitationHandeling invitationHandeling , GroupHandeling groupHandeling , ReservingRoomHandeling reservingRoomHandeling , RecommendStudentHandeling recommendStudentHandeling , StudentService studentService)
        {
            UserAppService = userAppService;
            ProfileHandeling = profileHandeling;
            InvitationHandeling = invitationHandeling;
            GroupHandeling = groupHandeling;
            ReservingRoomHandeling = reservingRoomHandeling;
            RecommendStudentHandeling = recommendStudentHandeling;
            StudentService = studentService;
        }
        /**********************************RecommendedStudent********************************************/
        
        public IActionResult RecommendedStudent()
        {
            if (GetStudentId() != 0)
                return View(RecommendStudentHandeling.GetStudent(GetStudentId()));
            return RedirectToAction("NotAuthorized", "Home");
        }
        /*****************************************MyProfile**********************************/
       
        public IEnumerable<StudentBehaviorProfile> MyProfile()
        {
         if(GetStudentId() != 0)
            return ProfileHandeling.GetStudent(GetStudentId());
            return null;
        }
        /********************************MyProfile***************************************/
   
        public bool UnJion()
        {
            if (GetStudentId() != 0)
                return ProfileHandeling.UnJion(GetStudentId());
            return false;
        }
        /*******************************************keyAction********************************/
        public bool keyAction()
        {
            if (GetStudentId() != 0)
                return ProfileHandeling.keyAction(GetStudentId());
            return false;
        }
        /***************************************InviteStudent********************************/
        public int InviteStudent(int id)
        {
            if (GetStudentId() != 0)
                return InvitationHandeling.AddRequest(GetStudentId(), id);
            return 0;
            
        }

        /***********************************UnInviteStudent*****************************/       
        public int UnInviteStudent(int id, int key)
        {
            if (GetStudentId() != 0)
                return InvitationHandeling.DeleteRequest(GetStudentId(), id, key);
            return 0;
        }

        /*************************************InviteGroup********************************/
        
        public bool InviteGroup(int id)
        {
            if (GetStudentId() != 0)
                return InvitationHandeling.InviteGroup(GetStudentId(), id);
            return false;
        }

        /***********************************UnInviteGroup****************************/

        public bool UnInviteGroup(int id)
        {
            if (GetStudentId() != 0)
                return InvitationHandeling.UnInviteGroup(GetStudentId(), id);
            return false;
            
        }
        /***********************************MyInvitation************************************/
        public IActionResult MyInvitation()
        {
            if (GetStudentId() != 0)
                return View(InvitationHandeling.GetStudent(GetStudentId()));
            return RedirectToAction("NotAuthorized", "Home");
        }

        /*****************************AcceptStudent*******************************************/

        public bool AcceptStudent(int id)
        {
            if (GetStudentId() != 0)
                return GroupHandeling.JoinGroup(id, GetStudentId(), 1);
            return false;
        }
        /************************************AcceptInvitation******************************/

        public bool AcceptInvitation(int id, int key)
        {
            if (GetStudentId() != 0)
                return GroupHandeling.JoinGroup(id, GetStudentId(), key);
            return false;
        }

        /**************************RejectInvitation************************************/

        public int RejectInvitation(int id, int key)
        {
            if (GetStudentId() != 0)
                return InvitationHandeling.DeleteRequest(id, GetStudentId(), key);
            return 0;
        }

        /*************************RecommendedGroup**************************************/

        public IActionResult RecommendedGroup()
        {
            if (GetStudentId()  != 0)
                return View(GroupHandeling.GetStudent(GetStudentId()));
            return RedirectToAction("NotAuthorized", "Home");
        }

        /******************************Details******************************************/
        [HttpGet]
        public IActionResult detailsAction(int studentId)
        {
            return PartialView(StudentService.GetById(studentId));
        }


        /******************************Rooms******************************************/

        public IActionResult Rooms(int buildingId, int floorNum)
        {
            if (GetStudentId() != 0)
                return View(ReservingRoomHandeling.StudentInRoom(GetStudent().Building.Id, (floorNum==0)?1: floorNum));
            else
                return View(ReservingRoomHandeling.StudentInRoom(buildingId , (floorNum == 0) ? 1 : floorNum));
        }
        /******************************Rooms******************************************/
        [HttpGet]
        public IActionResult RoomsInSearch(int buildingId , int floorNum)
        {
            if (GetStudentId() != 0)
                return PartialView(ReservingRoomHandeling.StudentInRoom(GetStudent().Building.Id, (floorNum == 0) ? 1 : floorNum));
            else
                return PartialView(ReservingRoomHandeling.StudentInRoom(buildingId , (floorNum == 0) ? 1 : floorNum));
        }

        /*********************************ReserveRoom*****************************************/

        public bool ReserveRoom(int roomNum)
        {
            if (GetStudentId() != 0)
                return ReservingRoomHandeling.ReserveRoom(GetStudentId(), roomNum);
            return false;
        }
        /**********************GetStudent*****************************************/

        private Student GetStudent()
        {
            CookiesManager cookiesManager = new CookiesManager(HttpContext.Request, HttpContext.Response);
            var userInfo = cookiesManager.Get("token");
            if (userInfo != null)
            {
                TokenDto tokenDto = JsonConverter.Deserialize(userInfo);
                var std = StudentService.GetById(tokenDto.UserId);

                if (std != null &&  tokenDto.UserStudent)
                {
                    return std;
                }
            }
            return null;
        }
        /**********************GetStudentId*****************************************/

        private int GetStudentId()
        {
            var STD = GetStudent();
            return (STD != null ? STD.Id : 0);
        }
    }
}
