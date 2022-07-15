using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCity.BL.Services.Utilities
{
    public class ReservingRoomHandeling
    {
        private readonly StudentService StudentService;
        private readonly BuildingService BuildingService;

        public ReservingRoomHandeling(StudentService studentService, BuildingService buildingService)
        {
            StudentService = studentService;
            BuildingService = buildingService;
        }
        public briefBuilding StudentInRoom(int BuildingId, int floorNum)
        {
            //int f = 3;
            var build = BuildingService.GetById(BuildingId);
            if (build != null)
            {
                int flootCpt = build.B_Floors_Num;
                int roomCpt = build.F_Rooms_num;
                var student_db = StudentService.GetAll().Where(w => w.RoomNum != 0 && w.Building != null && w.Building.Id == build.Id && w.RoomNum / 100 == floorNum).OrderBy(x => x.RoomNum);
                briefRoom temp = new briefRoom();
                briefBuilding buildingData = new briefBuilding();
                buildingData.BuildingNum = build.B_Number;
                buildingData.BuildingId = BuildingId;
                buildingData.briefRoom = new List<briefRoom> { };
                int roomNum = (floorNum * 100) + 1;
                if (student_db.Count() == 0)
                {
                    for (int i = roomNum; i <= ((floorNum * 100) + roomCpt); i++)
                    {

                        if (i % 100 > roomCpt || i % 100 == 0)
                            continue;
                        temp.RoomNum = i;
                        buildingData.briefRoom.Add(temp);
                        temp = new briefRoom();
                    }
                }
                else
                {
                    int similiar = student_db.FirstOrDefault().RoomNum;
                    List<briefStudent> SDList = new List<briefStudent>();
                    briefStudent sd = new briefStudent();

                    foreach (var student in student_db)
                    {
                        if (similiar != student.RoomNum)
                        {

                            temp.briefStudent = SDList;

                            temp.RoomNum = similiar;
                            buildingData.briefRoom.Add(temp);
                            similiar = student.RoomNum;
                            temp = new briefRoom();
                            SDList = new List<briefStudent> { };
                        }

                        if ((roomNum < student.RoomNum - 1) || ((roomNum < student.RoomNum) && (roomNum == 101)))
                        {
                            for (int i = roomNum%100==1? roomNum : roomNum+1; i < student.RoomNum; i++)
                            {

                                if (i % 100 > roomCpt || i % 100 == 0)
                                    continue;
                                temp.RoomNum = i;
                                buildingData.briefRoom.Add(temp);
                                temp = new briefRoom();
                            }
                        }
                        roomNum = student.RoomNum;

                        sd.Name = student.user.FirstName + " " + student.user.LastName;
                        sd.Image = student.user.Image[0];
                        sd.College = student.College;
                        sd.City = student.city;

                        SDList.Add(sd);
                        sd = new briefStudent();

                    }

                    temp.briefStudent = SDList;
                    temp.RoomNum = similiar;
                    buildingData.briefRoom.Add(temp);
                    temp = new briefRoom();

                    for (int i = roomNum + 1; i <= ((floorNum * 100) + roomCpt); i++)
                    {
                        if (i % 100 > roomCpt || i % 100 == 0)
                            continue;
                        temp.RoomNum = i;
                        buildingData.briefRoom.Add(temp);
                        temp = new briefRoom();
                    }
                }
                return buildingData;
            }
            return null;
        }
        public bool ReserveRoom(int StudentLoginId,int roomNum)
        {
            var Student_db = StudentService.GetById(StudentLoginId);
            Student_db.RoomKey = (Student_db.GroupId != 0) ?
                                 StudentService.GetAll().Where(q => (q.GroupId == Student_db.GroupId) && (q.RoomKey != 0)).Count() : Student_db.RoomKey;
            if ( Student_db.RoomKey == 4 && Student_db!=null)
            {
                if(Student_db.GroupId !=0)
                {
                    foreach(var student in StudentService.GetAll().Where(q=>q.GroupId== Student_db.GroupId))
                    {
                        student.RoomNum = roomNum;
                        student.RoomKey = 0;
                        StudentService.Update(student);
                    }
                   
                }
                else if(StudentService.GetAll().Where(q => q.RoomNum == roomNum).Count() < 4)
                {
                    Student_db.RoomNum = roomNum;
                    Student_db.RoomKey = 0;
                    StudentService.Update(Student_db);
                }
                return true;
            }
            return false ;
        }
    }
}
