using System;
using System.Collections;
using System.Collections.Generic;

namespace StudentCity.Data.Models
{
    public static class TokenDto
    {
        private static DateTime DateTime ;
        private static string UserName ;
        private static int UserId;
        private static int StudentId;
       private static bool isAllowed = false;

       
        public static void GenerateToken(int userId,string userName,int studentId)
        {
            DateTime = DateTime.Now;
            isAllowed = true;
            UserName = userName;
            UserId = userId;
            StudentId= studentId; 
        }
        public static Token GetUserToken()
        {
            
            if (isAllowed && DateTime.DayOfYear == DateTime.Now.DayOfYear)
            {
                Token token = new Token
                {
                    UserId= TokenDto.UserId,
                    UserName= TokenDto.UserName,
                    StudentId= TokenDto.StudentId
                };
               
                return token;
            }
            return null;
        }
        public static bool RemoveUserToken()
        {
            DateTime = DateTime.Now.AddYears(1);
            isAllowed = false;
            UserName = "";
            UserId = 0;
            StudentId = 0;
            return true;
        }
    }

    public class Token
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }
    }
}
