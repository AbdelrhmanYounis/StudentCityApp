using StudentCity.DAL.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCity.BL.Services.Utilities
{
    public class CounterClass
    {
        /*******************************************************************************************/

        public int ActivitiesStyleCountering(Student std1, Student std2)
        {
            if (std1.Activities_Style.Id == std2.Activities_Style.Id)
                return 4;
            else
            {
                int c = 0;
                if (std1.Activities_Style.Is_AC_LoveGames == std2.Activities_Style.Is_AC_LoveGames)
                    c++;
                if (std1.Activities_Style.Is_AC_LoveHargingOut == std2.Activities_Style.Is_AC_LoveHargingOut)
                    c++;
                if (std1.Activities_Style.Is_AC_PrayingInMosque == std2.Activities_Style.Is_AC_PrayingInMosque)
                    c++;
                if (std1.Activities_Style.Is_AC_QuranTimeSpecialization == std2.Activities_Style.Is_AC_QuranTimeSpecialization)
                    c++;
                return c;
            }
        }

        /*******************************************************************************************/

        public int CharacterStyleCountering(Student std1,Student std2)
        {
            if (std1.Character_Style.Id == std2.Character_Style.Id)
                return 3;
            else
            {
                int c = 0;
                if (std1.Character_Style.Is_CH_Ambitious == std2.Character_Style.Is_CH_Ambitious)
                    c++;
                if (std1.Character_Style.Is_CH_Social == std2.Character_Style.Is_CH_Social)
                    c++;
                if (std1.Character_Style.Is_CH_Curcious == std2.Character_Style.Is_CH_Curcious)
                    c++;

                return c;
            }
        }

        /*******************************************************************************************/

        public int CollegeCountering(Student std1,Student std2)
        {

            return std1.College.Id == std2.College.Id ? 1 : 0;
        }

        /*******************************************************************************************/

        public int LevelCountering(Student std1,Student std2)
        {

            return std1.College.Level == std2.College.Level ? 1 : 0;
        }

        /*******************************************************************************************/

        public int FoodStyleCountering(Student std1,Student std2)
        {
            if (std1.Food_Style.Id == std2.Food_Style.Id)
                return 2;
            else
            {
                int c = 0;
                if (std1.Food_Style.Is_F_FastFood == std2.Food_Style.Is_F_FastFood)
                    c++;
                if (std1.Food_Style.Is_F_Individual == std2.Food_Style.Is_F_Individual)
                    c++;

                return c;
            }
        }

        /*******************************************************************************************/

        public int AddressCountering(Student std1,Student std2)
        {
            if (std1.city.Id == std2.city.Id)
                return 3;
            else if (std1.city.Governrate.Id == std2.city.Governrate.Id)
                return 2;
            else if (std1.city.Governrate.Country.Id == std2.city.Governrate.Country.Id)
                return 1;
            else
                return 0;
        }

        /*******************************************************************************************/

        public int GradeCountering(Student std1,Student std2)
        {

            return std1.College.Grade.Id == std2.College.Grade.Id ? 1 : 0;
        }

        /*******************************************************************************************/

        public int HobbiesStyleCountering(Student std1,Student std2)
        {
            if (std1.Hobbies_Style.Id == std2.Hobbies_Style.Id)
                return 5;
            else
            {
                int c = 0;
                if (std1.Hobbies_Style.Is_H_Arts == std2.Hobbies_Style.Is_H_Arts)
                    c++;
                if (std1.Hobbies_Style.Is_H_Culture == std2.Hobbies_Style.Is_H_Culture)
                    c++;
                if (std1.Hobbies_Style.Is_H_Religion == std2.Hobbies_Style.Is_H_Religion)
                    c++;
                if (std1.Hobbies_Style.Is_H_sociaty == std2.Hobbies_Style.Is_H_sociaty)
                    c++;
                if (std1.Hobbies_Style.Is_H_Sport == std2.Hobbies_Style.Is_H_Sport)
                    c++;

                return c;
            }
        }

        /*******************************************************************************************/

        public int SleepStyleCountering(Student std1,Student std2)
        {
            if (std1.Sleep_Style.Id == std2.Sleep_Style.Id)
                return 5;
            else
            {
                int c = 0;
                if (std1.Sleep_Style.Is_SL_Continuity == std2.Sleep_Style.Is_SL_Continuity)
                    c++;
                if (std1.Sleep_Style.Is_SL_Deep == std2.Sleep_Style.Is_SL_Deep)
                    c++;
                if (std1.Sleep_Style.Is_SL_Lightness == std2.Sleep_Style.Is_SL_Lightness)
                    c++;
                if (std1.Sleep_Style.Is_SL_Night == std2.Sleep_Style.Is_SL_Night)
                    c++;
                if (std1.Sleep_Style.Is_SL_Noisy == std2.Sleep_Style.Is_SL_Noisy)
                    c++;

                return c;
            }
        }

        /*******************************************************************************************/

        public int StudyStyleCountering(Student std1,Student std2)
        {
            if (std1.Study_Style.Id == std2.Study_Style.Id)
                return 4;
            else
            {
                int c = 0;
                if (std1.Study_Style.Is_ST_AttendingCollege == std2.Study_Style.Is_ST_AttendingCollege)
                    c++;
                if (std1.Study_Style.Is_ST_Continuity == std2.Study_Style.Is_ST_Continuity)
                    c++;
                if (std1.Study_Style.Is_ST_Individual == std2.Study_Style.Is_ST_Individual)
                    c++;
                if (std1.Study_Style.study_way.Id == std2.Study_Style.study_way.Id)
                    c++;

                return c;
            }
        }

        /*******************************************************************************************/
        public int  counter(Student std1,Student std2)
        {
            int value = 0;
                value += 
                ((ActivitiesStyleCountering(std1, std2)+HobbiesStyleCountering(std1, std2)) * std1.Priority.Hobby) +
                                      ( CharacterStyleCountering(std1, std2)* std1.Priority.Character) +
                                       ((CollegeCountering(std1, std2)+ GradeCountering(std1, std2)+ LevelCountering(std1, std2)) * std1.Priority.College) +
                                      ( FoodStyleCountering(std1, std2)* std1.Priority.Food) +
                                      ( AddressCountering(std1, std2)* std1.Priority.Address) +
                                      ( SleepStyleCountering(std1, std2)* std1.Priority.Sleep) +
                                      ( StudyStyleCountering(std1, std2)* std1.Priority.Study);
            return value;
        }
        /*******************************************************************************************/

        public static int calculateDivider(Student std1)
        {
            int value = 0;
            value +=
            (9 * std1.Priority.Hobby) +
                                  (3* std1.Priority.Character) +
                                   (3* std1.Priority.College) +
                                  (2 * std1.Priority.Food) +
                                  (3 * std1.Priority.Address) +
                                  (5* std1.Priority.Sleep) +
                                  (4* std1.Priority.Study);
            return value;
        }

    }
}
