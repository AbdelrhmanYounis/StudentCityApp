using System.Collections.Generic;
using System.Text;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;
using StudentCity.DAL.Shared;
using StudentCity.DAL.Utilities;
using MongoDB.Bson;

namespace StudentCityUI
{
    public class MongoSeed
    {
        private readonly CountryRepository _countryRepository;
        private readonly UserRepository _userRepository;
        private readonly CityRepository _cityRepository;
        private readonly GovernrateRepository _governrateRepository;
        private readonly GradeRepository _gradeRepository;
        private readonly StudyWayRepository _StudyWayRepository;
        private readonly CollegeRepository _CollegeRepository;
        private readonly ActivitiesRepository _ActivitiesRepository;
        private readonly CharacterRepository _CharacterRepository;
        private readonly FoodStyleRepository _FoodStyleRepository;
        private readonly HobbiesStyleRepository _HobbiesStyleRepository;
        private readonly SleepStyleRepository _SleepStyleRepository;
        private readonly StudyStyleRepository _StudyStyleRepository;

        public MongoSeed(CountryRepository countryRepository,
            GovernrateRepository governrateRepository,
            CityRepository cityRepository,
            UserRepository userRepository,
            GradeRepository gradeRepository,
            StudyWayRepository studyWayRepository,
            CollegeRepository collegeRepository ,
            ActivitiesRepository activitiesRepository ,
            CharacterRepository characterRepository ,
            FoodStyleRepository foodStyleRepository ,
            HobbiesStyleRepository hobbiesStyleRepository,
            SleepStyleRepository sleepStyleRepository,
            StudyStyleRepository studyStyleRepository
            )
        {
            _countryRepository = countryRepository;
            _governrateRepository = governrateRepository;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
            _gradeRepository = gradeRepository;
            _StudyWayRepository = studyWayRepository;
            _CollegeRepository = collegeRepository;
            _ActivitiesRepository = activitiesRepository;
            _CharacterRepository = characterRepository ;
            _FoodStyleRepository = foodStyleRepository;
            _HobbiesStyleRepository = hobbiesStyleRepository;
            _SleepStyleRepository = sleepStyleRepository;
            _StudyStyleRepository = studyStyleRepository;
        }

        #region lookups 
        
        public void SeedCountries()
        {
            CountryModel countryModel = new CountryModel
            {
                Id = 1,
                NameAr = "مصر",
                NameEn = "Egypt"
            };
            _countryRepository.InsertOrUpdate(x => x.NameEn == "Egypt", countryModel);
        }
        public void SeedGovernrates()
        {
            var country = _countryRepository.GetById(1);
            Governrate governrate1 = new Governrate
            {
                Id = 1,
                NameAr = "البحيرة",
                NameEn = "buhyria",
                Country=country
            };
            _governrateRepository.InsertOrUpdate(x => x.NameEn == "buhyria", governrate1);
        }
        public void SeedCities()
        {
            var governrate = _governrateRepository.GetById(1);
            CityModel cityModel = new CityModel
            {
                Id = 1,
                NameAr = "شبراخيت",
                NameEn = "shubrakhit",
                Governrate = governrate
            };
            _cityRepository.InsertOrUpdate(x => x.NameEn == "shubrakhit", cityModel);
        }
        public void SeedGrades()
        {
            Grade Grade1 = new Grade
            {
                Id = 1,
                NameAr = "ممتاز",
                NameEn = "Excellent"
            };
            _gradeRepository.InsertOrUpdate(x => x.Id == 1, Grade1);
            Grade Grade2 = new Grade
            {
                Id = 2,
                NameAr = "جيد جدا",
                NameEn = "Very Good"
            };
            _gradeRepository.InsertOrUpdate(x => x.Id == 2, Grade2);
            Grade Grade3 = new Grade
            {
                Id = 3,
                NameAr = "جيد",
                NameEn = "Good"
            };
            _gradeRepository.InsertOrUpdate(x => x.Id == 3, Grade3);
            Grade Grade4 = new Grade
            {
                Id = 4,
                NameAr = "مقبول",
                NameEn = "Poor"
            };
            _gradeRepository.InsertOrUpdate(x => x.Id == 4, Grade4);
        }
        public void SeedStudyWays()
        {
            StudyWay StudyWay1 = new StudyWay
            {
                Id = 1,
                NameAr = "الاعتماد على القراءة فى المذاكرة",
                NameEn = "consider Reading in Study Way"
            };
            _StudyWayRepository.InsertOrUpdate(x => x.NameEn == "consider Reading in Study Way", StudyWay1);
            StudyWay StudyWay2 = new StudyWay
            {
                Id = 2,
                NameAr = "الاعتماد على الكتابة فى المذاكرة",
                NameEn = "consider Writing in Study Way"
            };
            _StudyWayRepository.InsertOrUpdate(x => x.NameEn == "consider Writing in Study Way", StudyWay2);
            StudyWay StudyWay3 = new StudyWay
            {
                Id = 3,
                NameAr = "الاعتماد على الرسومات فى المذاكرة",
                NameEn = "consider diagrams in Study Way"
            };
            _StudyWayRepository.InsertOrUpdate(x => x.NameEn == "consider diagrams in Study Way", StudyWay3);
            StudyWay StudyWay4 = new StudyWay
            {
                Id = 4,
                NameAr = "الاعتماد على شرح الفيديوهات فى المذاكرة",
                NameEn = "consider Videos in Study Way"
            };
            _StudyWayRepository.InsertOrUpdate(x => x.NameEn == "consider Videos in Study Way", StudyWay4);
        }
        public void SeedColleges()
        {
            _CollegeRepository.InsertOrUpdate(x => x.NameEn == "Computing and Information", new College
            {
                Id = 1,
                LevelNum = 4,
                NameAr = "حاسبات و معلومات",
                NameEn = "Computing and Information"
            });
            _CollegeRepository.InsertOrUpdate(x => x.NameEn == "Pharmacy", new College
            {
                Id = 2,
                LevelNum = 5,
                NameAr = "صيدلة",
                NameEn = "Pharmacy"
            });
        }
        public void SeedActivities()
        {
            _ActivitiesRepository.InsertOrUpdate(x => x.Id ==1, new ActivitiesStyle
            {
                Id = 1,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 2, new ActivitiesStyle
            {
                Id = 2,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 3, new ActivitiesStyle
            {
                Id = 3,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 4, new ActivitiesStyle
            {
                Id = 4,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 5, new ActivitiesStyle
            {
                Id = 5,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 6, new ActivitiesStyle
            {
                Id = 6,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 7, new ActivitiesStyle
            {
                Id = 7,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 8, new ActivitiesStyle
            {
                Id = 8,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = false
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 9, new ActivitiesStyle
            {
                Id = 9,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 10, new ActivitiesStyle
            {
                Id = 10,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 11, new ActivitiesStyle
            {
                Id = 11,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 12, new ActivitiesStyle
            {
                Id = 12,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = false,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 13, new ActivitiesStyle
            {
                Id = 13,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 14, new ActivitiesStyle
            {
                Id = 14,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = false,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 15, new ActivitiesStyle
            {
                Id = 15,
                Is_AC_LoveGames = false,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = true
            });
            _ActivitiesRepository.InsertOrUpdate(x => x.Id == 16, new ActivitiesStyle
            {
                Id = 16,
                Is_AC_LoveGames = true,
                Is_AC_PrayingInMosque = true,
                Is_AC_LoveHargingOut = true,
                Is_AC_QuranTimeSpecialization = true
            });
        }
        public void SeedCharacters()
        {
            CharacterStyle Character1 = new CharacterStyle
            {
                Id = 1,
                Is_CH_Ambitious=0,
                Is_CH_Curcious=0,
                Is_CH_Social=0
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 1, Character1);
            CharacterStyle Character2 = new CharacterStyle
            {
                Id = 2,
                Is_CH_Ambitious = 1,
                Is_CH_Curcious = 0,
                Is_CH_Social = 0
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 2, Character2);
            CharacterStyle Character3 = new CharacterStyle
            {
                Id = 3,
                Is_CH_Ambitious = 0,
                Is_CH_Curcious = 1,
                Is_CH_Social = 0
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 3, Character3);
            CharacterStyle Character4 = new CharacterStyle
            {
                Id = 4,
                Is_CH_Ambitious = 1,
                Is_CH_Curcious = 1,
                Is_CH_Social = 0
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 4, Character4);
            CharacterStyle Character5 = new CharacterStyle
            {
                Id = 5,
                Is_CH_Ambitious = 0,
                Is_CH_Curcious = 0,
                Is_CH_Social = 1
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 5, Character5);
            CharacterStyle Character6 = new CharacterStyle
            {
                Id = 6,
                Is_CH_Ambitious = 1,
                Is_CH_Curcious = 0,
                Is_CH_Social = 1
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 6, Character6);
            CharacterStyle Character7 = new CharacterStyle
            {
                Id = 7,
                Is_CH_Ambitious = 0,
                Is_CH_Curcious = 1,
                Is_CH_Social = 1
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 7, Character7);
            CharacterStyle Character8 = new CharacterStyle
            {
                Id = 8,
                Is_CH_Ambitious = 1,
                Is_CH_Curcious = 1,
                Is_CH_Social = 1
            };
            _CharacterRepository.InsertOrUpdate(x => x.Id == 8, Character8);

        }
        public void SeedFoods()
        {
            FoodStyle FoodStyle1 = new FoodStyle
            {
                Id = 1,
                Is_F_FastFood=0,
                Is_F_Individual=0
            };
            _FoodStyleRepository.InsertOrUpdate(x => x.Id ==1, FoodStyle1);
            FoodStyle FoodStyle2 = new FoodStyle
            {
                Id = 2,
                Is_F_FastFood = 1,
                Is_F_Individual = 0
            };
            _FoodStyleRepository.InsertOrUpdate(x => x.Id == 2, FoodStyle2);
            FoodStyle FoodStyle3 = new FoodStyle
            {
                Id = 3,
                Is_F_FastFood = 0,
                Is_F_Individual = 1
            };
            _FoodStyleRepository.InsertOrUpdate(x => x.Id == 3, FoodStyle3);
            FoodStyle FoodStyle4 = new FoodStyle
            {
                Id = 4,
                Is_F_FastFood = 1,
                Is_F_Individual = 1
            };
            _FoodStyleRepository.InsertOrUpdate(x => x.Id == 4, FoodStyle4);
        }
        public void SeedHobbies()
        {
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id==1,new HobbiesStyle
            {
                Id = 1,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 2, new HobbiesStyle
            {
                Id = 2,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 3, new HobbiesStyle
            {
                Id = 3,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 4, new HobbiesStyle
            {
                Id = 4,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 5, new HobbiesStyle
            {
                Id = 5,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 6, new HobbiesStyle
            {
                Id = 6,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 7, new HobbiesStyle
            {
                Id = 7,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 8, new HobbiesStyle
            {
                Id = 8,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 9, new HobbiesStyle
            {
                Id = 9,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 10, new HobbiesStyle
            {
                Id = 10,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 11, new HobbiesStyle
            {
                Id = 11,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 12, new HobbiesStyle
            {
                Id = 12,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 13, new HobbiesStyle
            {
                Id = 13,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 14, new HobbiesStyle
            {
                Id = 14,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 15, new HobbiesStyle
            {
                Id = 15,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 16, new HobbiesStyle
            {
                Id = 16,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = false
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 17, new HobbiesStyle
            {
                Id = 17,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 18, new HobbiesStyle
            {
                Id = 18,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 19, new HobbiesStyle
            {
                Id = 19,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 20, new HobbiesStyle
            {
                Id = 20,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 21, new HobbiesStyle
            {
                Id = 21,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 22, new HobbiesStyle
            {
                Id = 22,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 23, new HobbiesStyle
            {
                Id = 23,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 24, new HobbiesStyle
            {
                Id = 24,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 25, new HobbiesStyle
            {
                Id = 25,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = true,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 26, new HobbiesStyle
            {
                Id = 26,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = false,
                Is_H_sociaty = true,
                Is_H_Sport = true
            }); 
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 27, new HobbiesStyle
            {
                Id = 27,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = true,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 28, new HobbiesStyle
            {
                Id = 28,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = false,
                Is_H_sociaty = false,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 29, new HobbiesStyle
            {
                Id = 29,
                Is_H_Arts = false,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 30, new HobbiesStyle
            {
                Id = 30,
                Is_H_Arts = true,
                Is_H_Culture = false,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 31, new HobbiesStyle
            {
                Id = 31,
                Is_H_Arts = false,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = true
            });
            _HobbiesStyleRepository.InsertOrUpdate(x => x.Id == 32, new HobbiesStyle
            {
                Id = 32,
                Is_H_Arts = true,
                Is_H_Culture = true,
                Is_H_Religion = true,
                Is_H_sociaty = true,
                Is_H_Sport = true
            });
        }
        public void SeedSleeps()
        {
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 1, new SleepStyle
            {
                Id = 1,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 2, new SleepStyle
            {
                Id = 2,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 3, new SleepStyle
            {
                Id = 3,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 4, new SleepStyle
            {
                Id = 4,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 5, new SleepStyle
            {
                Id = 5,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 6, new SleepStyle
            {
                Id = 6,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 7, new SleepStyle
            {
                Id = 7,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 8, new SleepStyle
            {
                Id = 8,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 9, new SleepStyle
            {
                Id = 9,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 10, new SleepStyle
            {
                Id = 10,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 11, new SleepStyle
            {
                Id = 11,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 12, new SleepStyle
            {
                Id = 12,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 13, new SleepStyle
            {
                Id = 13,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 14, new SleepStyle
            {
                Id = 14,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 15, new SleepStyle
            {
                Id = 15,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 16, new SleepStyle
            {
                Id = 16,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 0
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 17, new SleepStyle
            {
                Id = 17,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 18, new SleepStyle
            {
                Id = 18,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 19, new SleepStyle
            {
                Id = 19,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 20, new SleepStyle
            {
                Id = 20,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 21, new SleepStyle
            {
                Id = 21,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 22, new SleepStyle
            {
                Id = 22,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 23, new SleepStyle
            {
                Id = 23,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 24, new SleepStyle
            {
                Id = 24,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 0,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 25, new SleepStyle
            {
                Id = 25,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 26, new SleepStyle
            {
                Id = 26,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 27, new SleepStyle
            {
                Id = 27,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 28, new SleepStyle
            {
                Id = 28,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 0,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 29, new SleepStyle
            {
                Id = 29,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 30, new SleepStyle
            {
                Id = 30,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 0,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 31, new SleepStyle
            {
                Id = 31,
                Is_SL_Continuity = 0,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
            _SleepStyleRepository.InsertOrUpdate(x => x.Id == 32, new SleepStyle
            {
                Id = 32,
                Is_SL_Continuity = 1,
                Is_SL_Deep = 1,
                Is_SL_Lightness = 1,
                Is_SL_Night = 1,
                Is_SL_Noisy = 1
            });
        }
        public void SeedStudys()
        {
            var study_way = _StudyWayRepository.GetById(1);

            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 1, new StudyStyle
            {
                Id = 2,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 2, new StudyStyle
            {
                Id = 2,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 3, new StudyStyle
            {
                Id = 3,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 4, new StudyStyle
            {
                Id = 4,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 5, new StudyStyle
            {
                Id = 5,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 6, new StudyStyle
            {
                Id = 6,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 7, new StudyStyle
            {
                Id = 7,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 8, new StudyStyle
            {
                Id = 8,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });

            study_way = _StudyWayRepository.GetById(2);

            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 9, new StudyStyle
            {
                Id = 9,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 10, new StudyStyle
            {
                Id = 10,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 11, new StudyStyle
            {
                Id = 11,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 12, new StudyStyle
            {
                Id = 12,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 13, new StudyStyle
            {
                Id = 13,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 14, new StudyStyle
            {
                Id = 14,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 15, new StudyStyle
            {
                Id = 15,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 16, new StudyStyle
            {
                Id = 16,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });

            study_way = _StudyWayRepository.GetById(3);

            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 17, new StudyStyle
            {
                Id = 17,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 18, new StudyStyle
            {
                Id = 18,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 19, new StudyStyle
            {
                Id = 19,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 20, new StudyStyle
            {
                Id = 20,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 21, new StudyStyle
            {
                Id = 21,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 22, new StudyStyle
            {
                Id = 22,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 23, new StudyStyle
            {
                Id = 23,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 24, new StudyStyle
            {
                Id = 24,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });

            study_way = _StudyWayRepository.GetById(4);

            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 25, new StudyStyle
            {
                Id = 25,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 26, new StudyStyle
            {
                Id = 26,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 27, new StudyStyle
            {
                Id = 27,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 28, new StudyStyle
            {
                Id = 28,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 0,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 29, new StudyStyle
            {
                Id = 29,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 30, new StudyStyle
            {
                Id = 30,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 0,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 31, new StudyStyle
            {
                Id = 31,
                Is_ST_AttendingCollege = 0,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });
            _StudyStyleRepository.InsertOrUpdate(x => x.Id == 32, new StudyStyle
            {
                Id = 32,
                Is_ST_AttendingCollege = 1,
                Is_ST_Continuity = 1,
                Is_ST_Individual = 1,
                study_way = study_way
            });
        }
        #endregion
        public void SeedDefaultCredintials()
        {
            var salt = SecurityHelper.Encrypt("admin@system.com");
            var adminUser = new UserModel
            {
                Id = 1,
                FirstName = "System",
                LastName = "Admin",
                HashedPassword = SecurityHelper.ComputeHash("P@ssw0rd", "SHA256", Encoding.UTF8.GetBytes(salt)),
                Email = "admin@system.com",
                Salt = salt,
                IsSystemAdmin = true
            };


            _userRepository.InsertOrUpdate(x => x.IsSystemAdmin == true, adminUser);

        }
    }


}
