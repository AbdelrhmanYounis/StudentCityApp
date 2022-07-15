using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Repos.Lookups;

namespace StudentCity.BL.Services.Lookups
{
    public class BehaviorService
    {
        private readonly SleepStyleRepository SleepStyleRepository;
        private readonly FoodStyleRepository FoodStyleRepository;
        private readonly StudyStyleRepository StudyStyleRepository;
        private readonly StudyWayRepository StudyWayRepository;
        private readonly HobbiesStyleRepository HobbiesStyleRepository;
        private readonly ActivitiesRepository ActivitiesRepository;
        private readonly CharacterRepository CharacterRepository;

        public BehaviorService(SleepStyleRepository sleepStyleRepository, FoodStyleRepository foodStyleRepository , StudyStyleRepository studyStyleRepository , StudyWayRepository studyWayRepository, HobbiesStyleRepository hobbiesStyleRepository , ActivitiesRepository activitiesRepository , CharacterRepository characterRepository)
        {
            SleepStyleRepository = sleepStyleRepository;
            FoodStyleRepository = foodStyleRepository;
            StudyStyleRepository = studyStyleRepository;
            StudyWayRepository = studyWayRepository;
            HobbiesStyleRepository = hobbiesStyleRepository;
            ActivitiesRepository = activitiesRepository;
            CharacterRepository = characterRepository;
        }
       
        public SleepStyle GetSleepStyle(SleepStyle model)
        {
            return SleepStyleRepository.GetAll().SingleOrDefault(x=> 
            x.Is_SL_Continuity==model.Is_SL_Continuity &&
            x.Is_SL_Deep==model.Is_SL_Deep &&
            x.Is_SL_Lightness==model.Is_SL_Lightness &&
            x.Is_SL_Night == model.Is_SL_Night &&
            x.Is_SL_Noisy == model.Is_SL_Noisy
            );
        }

        public FoodStyle GetFoodStyle(FoodStyle model)
        {
            return FoodStyleRepository.GetAll().SingleOrDefault(x=>
            x.Is_F_FastFood==model.Is_F_FastFood &&
            x.Is_F_Individual ==model.Is_F_Individual
            );
        }

        public StudyWay GetStudyWay(int id)
        {
           return StudyWayRepository.GetById(id);
        }

        public StudyStyle GetStudyStyle(StudyStyle model)
        {
            model.study_way = GetStudyWay(model.study_way.Id);
            return StudyStyleRepository.GetAll().SingleOrDefault(x=> 
            x.Is_ST_AttendingCollege ==model.Is_ST_AttendingCollege &&
            x.Is_ST_Continuity == model.Is_ST_Continuity &&
            x.Is_ST_Individual == model.Is_ST_Individual &&
            x.study_way.Id == model.study_way.Id
            );
        }
        public HobbiesStyle GetHobbiesStyle(HobbiesStyle model)
        {
            return HobbiesStyleRepository.GetAll().SingleOrDefault(x=>
            x.Is_H_Arts == model.Is_H_Arts &&
            x.Is_H_Culture == model.Is_H_Culture &&
            x.Is_H_Religion == model.Is_H_Religion &&
            x.Is_H_sociaty == model.Is_H_sociaty &&
            x.Is_H_Sport == model.Is_H_Sport
            );
        }
        public ActivitiesStyle GetActivitiesStyle(ActivitiesStyle model)
        {
            return ActivitiesRepository.GetAll().SingleOrDefault(x =>
            x.Is_AC_LoveGames == model.Is_AC_LoveGames &&
            x.Is_AC_LoveHargingOut == model.Is_AC_LoveHargingOut &&
            x.Is_AC_PrayingInMosque == model.Is_AC_PrayingInMosque &&
            x.Is_AC_QuranTimeSpecialization == model.Is_AC_PrayingInMosque 
            );
        }
        public CharacterStyle GetCharacterStyle(CharacterStyle model)
        {
            return CharacterRepository.GetAll().SingleOrDefault(x =>
            x.Is_CH_Ambitious == model.Is_CH_Ambitious &&
            x.Is_CH_Curcious == model.Is_CH_Curcious &&
            x.Is_CH_Social == model.Is_CH_Social
            );
        }
    }
}
