using System;
using System.Globalization;
using System.Text;
using System.Threading;
using StudentCity.Application;
using StudentCity.Application.AppService;
using StudentCity.BL;
using StudentCity.BL.Services.Lookups;
using StudentCity.BL.Services.Permissions;
using StudentCity.BL.Services.Utilities;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Repos.Lookups;
using StudentCity.DAL.Shared;
using StudentCityUI.Helpers;
using StudentCityUI.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace StudentCityUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(1);
               // options.Cookie.HttpOnly = true;
            });
            services.AddMvc();
            services.AddCors();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
            services.Configure<DbSettings>(options =>
            {
                try
                {
                    DbSettings dbSettings = new DbSettings();
                    /*
                     online 
                     */
                    // var mclient = new MongoClient(new MongoClientSettings()
                    //{
                    //    Server = MongoServerAddress.Parse(dbSettings.ServerAddress),
                    //   Credential = MongoCredential.CreateCredential(dbSettings.DataBaseName, dbSettings.Username, dbSettings.Password)
                    //});
                    /*
                   local 
                   */
                    var mclient = new MongoClient(dbSettings.ServerAddress);
                    //*****************
                    options.Database = mclient.GetDatabase(dbSettings.DataBaseName);

                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem connecting to the MongoDB database", ex);
                }
            });

            #region token

            JwtSettings settings = GetJwtSettings();
            //services.AddScoped<AuthorizeAccess>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {

                   options.TokenValidationParameters =
                       new TokenValidationParameters
                       {

                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(settings.Key)),

                           ValidateIssuer = true,
                           ValidIssuer = settings.Issuer,

                           ValidateAudience = true,
                           ValidAudience = settings.Audience,

                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.FromMinutes(
                               settings.MinutesToExpiration)


                       };
                   options.Authority = Configuration["JwtSettings:authority"];
                   options.RequireHttpsMetadata = false;
                   options.Configuration = new OpenIdConnectConfiguration();

               });


            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            #endregion


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ContactUs
            services.AddScoped<ContactUsRepository, ContactUsRepository>();
            services.AddScoped<ContactUsApp, ContactUsApp>();
            services.AddScoped<ContactUsService, ContactUsService>();
            //Request BehaviorService
            services.AddScoped<RequestRepository, RequestRepository>();
            services.AddScoped<RequestService, RequestService>();
            //Reject Student
            services.AddScoped<RejectedStudentRepository, RejectedStudentRepository>();
            services.AddScoped<RejectedStudentService, RejectedStudentService>();
            // Building 
            services.AddScoped<BuildingAppService, BuildingAppService>();
            services.AddScoped<BuildingService, BuildingService>();
            services.AddScoped<BuildingRepository, BuildingRepository>();
            // country 
            services.AddScoped<CountryAppService, CountryAppService>();
            services.AddScoped<CountryService, CountryService>();
            services.AddScoped<CountryRepository, CountryRepository>();
            // Governrate 
            services.AddScoped<GovernrateAppService, GovernrateAppService>();
            services.AddScoped<GovernrateService, GovernrateService>();
            services.AddScoped<GovernrateRepository, GovernrateRepository>();
            // city 
            services.AddScoped<CityAppService, CityAppService>();
            services.AddScoped<CityService, CityService>();
            services.AddScoped<CityRepository, CityRepository>();

            services.AddScoped<GradeService, GradeService>(); 
            services.AddScoped<GradeRepository, GradeRepository>(); 

                services.AddScoped<StudyWayService, StudyWayService>();
            services.AddScoped<StudyWayRepository, StudyWayRepository>();
           // College
            services.AddScoped<CollegeAppService, CollegeAppService>();
            services.AddScoped<CollegeService, CollegeService>();
            services.AddScoped<CollegeRepository, CollegeRepository>();

            services.AddScoped<FoodStyleRepository, FoodStyleRepository>();
            services.AddScoped<CharacterRepository, CharacterRepository>();
            services.AddScoped<ActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<HobbiesStyleRepository, HobbiesStyleRepository>();
            services.AddScoped<SleepStyleRepository, SleepStyleRepository>();
            services.AddScoped<StudyStyleRepository, StudyStyleRepository>(); 

                services.AddScoped<BehaviorService, BehaviorService>(); 
            // reset code
            services.AddScoped<ResetCodeRepository, ResetCodeRepository>();
            services.AddScoped<ResetCodeService, ResetCodeService>();
            services.AddScoped<ResetCodeAppService, ResetCodeAppService>();
               
            services.AddScoped<ProfileHandeling, ProfileHandeling>();
            services.AddScoped<RecommendStudentHandeling, RecommendStudentHandeling>();
            services.AddScoped<GroupHandeling, GroupHandeling>();
            services.AddScoped<InvitationHandeling, InvitationHandeling>();
            services.AddScoped<ReservingRoomHandeling, ReservingRoomHandeling>();

            // users
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<UserAppService, UserAppService>();

            // Students
            services.AddScoped<StudentRepository, StudentRepository>();
            services.AddScoped<StudentService, StudentService>();
          //  services.AddScoped<StudentAppService, StudentAppService>();


            ///// Registering Services
            services.AddSingleton(settings);
            services.AddTransient<SecurityManager>();
            services.AddTransient<Helper>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            // use cookies for languages
            app.Use(async (context, next) =>
            {
                var cookie = context.Request.Cookies["currentLanguage"];
                if (cookie != null)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie);
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                }
                await next();
            });
            app.UseCors("MyPolicy");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }

        public JwtSettings GetJwtSettings()
        {
            JwtSettings jwtSettings = new JwtSettings();


            jwtSettings.Key = Configuration["JwtSettings:key"];
            jwtSettings.Audience = Configuration["JwtSettings:audience"];
            jwtSettings.Issuer = Configuration["JwtSettings:issuer"];
            jwtSettings.MinutesToExpiration = Convert.ToInt32(Configuration["JwtSettings:minutesToExpiration"]);

            return jwtSettings;
        }



    }
}
