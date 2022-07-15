using System;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StudentCityUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var countryRepository = services.GetService<CountryRepository>();
                    var governrateRepository = services.GetService<GovernrateRepository>();
                    var cityRepository = services.GetService<CityRepository>();
                    var userRepository = services.GetService<UserRepository>();
                    var gradeRepository = services.GetService<GradeRepository>();
                    var studyWayRepository = services.GetService<StudyWayRepository>();
                    var collegeRepository = services.GetService<CollegeRepository>();
                    var sleepStyleRepository = services.GetService<SleepStyleRepository>();
                    var foodStyleRepository = services.GetService<FoodStyleRepository>();
                    var studyStyleRepository = services.GetService<StudyStyleRepository>();
                    var characterRepository = services.GetService<CharacterRepository>();
                    var activitiesRepository = services.GetService<ActivitiesRepository>();
                    var hobbiesStyleRepository = services.GetService<HobbiesStyleRepository>();
                    MongoSeed seed = new MongoSeed(countryRepository,
                        governrateRepository,cityRepository,
                        userRepository, gradeRepository, studyWayRepository, collegeRepository, activitiesRepository, characterRepository, foodStyleRepository, hobbiesStyleRepository, sleepStyleRepository,
                         studyStyleRepository 
                        );
                    seed.SeedCountries();
                    seed.SeedGovernrates();
                    seed.SeedCities();
                    seed.SeedDefaultCredintials();
                    seed.SeedSleeps();
                    
                    seed.SeedGrades();
                    seed.SeedStudyWays();
                    seed.SeedStudys();
                    seed.SeedColleges();
                    seed.SeedActivities();
                    seed.SeedCharacters();
                    seed.SeedFoods();
                    seed.SeedHobbies();
                    
                    
                    
                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
