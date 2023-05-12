using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.BAL.Providers;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.DAL.EF;
using Igmite.Lighthouse.EmailServices;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Services.HostServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services

{
    public class Startup
    {
        private readonly IHostingEnvironment environment;
        public IConfiguration configuration { get; }

        public Startup(IConfiguration _configuration, IHostingEnvironment _environment)
        {
            this.configuration = _configuration;
            this.environment = _environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                Constants.SQLConnectionString = Convert.ToString(this.configuration["ConnectionStrings:IgmiteDbEntities"]);
                Constants.Version = Convert.ToString(this.configuration["Settings:Version"]);
                Constants.SqlDatabaseName = Convert.ToString(this.configuration["Settings:SqlDatabaseName"]);
                Constants.SecretKey = Convert.ToString(this.configuration["Settings:SecretKey"]);
                Constants.TestToEmail = Convert.ToString(this.configuration["Settings:TestToEmail"]);
                Constants.TestToMobile = Convert.ToString(this.configuration["Settings:TestToMobile"]);
                Constants.DefaultAppPwd = Convert.ToString(this.configuration["Settings:DefaultAppPwd"]);
                Constants.IsDeveloperMode = Convert.ToBoolean(this.configuration["Settings:IsDeveloperMode"]);
                Constants.LazyLoadingAllowed = Convert.ToBoolean(this.configuration["Settings:LazyLoadingAllowed"]);
                Constants.HttpMethods = Convert.ToString(this.configuration["Settings:HttpMethods"]);
                Constants.HttpHeaders = Convert.ToString(this.configuration["Settings:HttpHeaders"]);
                Constants.HttpMaxAge = Convert.ToDouble(this.configuration["Settings:HttpMaxAge"]);
                Constants.CorsServiceUrl = Convert.ToString(this.configuration["Settings:CorsServiceUrl"]);
                Constants.CorsWebsiteUrl = Convert.ToString(this.configuration["Settings:CorsWebsiteUrl"]);
                Constants.AssetsPath = Convert.ToString(this.configuration["Settings:AssetsPath"]);
                Constants.DocumentPath = Convert.ToString(this.configuration["Settings:DocumentPath"]);
                Constants.ServiceIPAddress = Convert.ToString(this.configuration["Settings:ServiceIPAddress"]);
                Constants.ServiceIPPort = Convert.ToString(this.configuration["Settings:ServiceIPPort"]);
                Constants.PageSize = Convert.ToInt32(this.configuration["Settings:PageSize"]);
                Constants.SupportEmail = Convert.ToString(this.configuration["Settings:SupportEmail"]);
                Constants.BackDatedReportingDays = Convert.ToInt32(this.configuration["Settings:BackDatedReportingDays"]);
                Constants.DashboardCronExpression = Convert.ToString(this.configuration["Settings:DashboardCronExpression"]);
                Constants.NotReportingVTCronExpression = Convert.ToString(this.configuration["Settings:NotReportingVTCronExpression"]);
                Constants.WeeklyAttendanceCronExpression = Convert.ToString(this.configuration["Settings:WeeklyAttendanceCronExpression"]);
                Constants.WatermarkText = Convert.ToString(this.configuration["Settings:WatermarkText"]);
                Constants.CompressImageSize = Convert.ToString(this.configuration["Settings:CompressImageSize"]);

                Constants.RootPath = this.environment.ContentRootPath;

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                services.AddMvc().AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.DateFormatString = Constants.DateTimeFormatAPIOnly;

                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    opt.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
                    opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    opt.SerializerSettings.Formatting = Formatting.Indented;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                });

                services.AddMvc(opt =>
                {
                    //opt.OutputFormatters.Add(new PascalCaseJsonProfileFormatter());
                    opt.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                });

                services.AddAuthentication(authOption =>
                {
                    authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Constants.CorsServiceUrl,
                        ValidAudience = Constants.CorsServiceUrl,

                        // Specify the key used to sign the token:
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        },

                        OnTokenValidated = context =>
                         {
                             // Add the access_token as a claim, as we may actually need it
                             var accessToken = context.SecurityToken as JwtSecurityToken;
                             if (accessToken != null)
                             {
                                 ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                                 if (identity != null)
                                 {
                                     identity.AddClaim(new Claim("access_token", accessToken.RawData));
                                 }
                             }

                             return Task.CompletedTask;
                         }
                    };
                });

                // Enable Cors Option
                services.AddCors(corsOptions =>
                {
                    corsOptions.AddPolicy("LighthouseCors", builder =>
                    {
                        builder.WithOrigins(Constants.CorsWebsiteUrl.Split(','));
                        builder.WithMethods(Constants.HttpMethods.Split(','));
                        builder.WithHeaders(Constants.HttpHeaders.Split(','));
                        builder.SetPreflightMaxAge(TimeSpan.FromSeconds(Constants.HttpMaxAge));
                        builder.AllowCredentials();
                    });
                });

                services.AddSingleton<HtmlEncoder>(
                    HtmlEncoder.Create(allowedRanges: new[] {
                    UnicodeRanges.BasicLatin,
                    UnicodeRanges.CjkUnifiedIdeographs
                    }));

                services.AddDbContext<IgmiteDbContext>(options => options.UseMySql(Constants.SQLConnectionString));

                if (Constants.IsDeveloperMode)
                {
                    //services.AddSwaggerDocumentation();
                    services.AddSwaggerGen(swagger =>
                    {
                        //This is to generate the Default UI of Swagger Documentation
                        swagger.SwaggerDoc("LighthouseServices", new OpenApiInfo
                        {
                            Title = "Lighthouse Services",
                            Description = "LighthouseServices",
                            Version = Constants.Version
                        });

                        // To Enable authorization using Swagger (JWT)
                        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        });

                        //swagger.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        //    {
                        //        new OpenApiSecurityScheme
                        //        {
                        //            Reference = new OpenApiReference
                        //            {
                        //                Type = ReferenceType.SecurityScheme,
                        //                Id = "Bearer"
                        //            }
                        //        },
                        //        new string[] {}
                        //    }
                        //});

                        //var filePath = Path.Combine(AppContext.BaseDirectory, "LighthouseControllers.xml");
                        //s.IncludeXmlComments(filePath);
                    });
                }

                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.KnownProxies.Add(IPAddress.Parse(Constants.ServiceIPAddress));
                });

                var keysFolder = Path.Combine(this.environment.ContentRootPath, "Keys");
                services.AddDataProtection()
                    .SetApplicationName("Lighthouse-Services")
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    })
                    .SetDefaultKeyLifetime(TimeSpan.FromDays(1825)); // 5 Years

                services.AddHttpContextAccessor();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                //startup.cs
                //services.Configure<IdentityOptions>(options =>
                //{
                //    // Password settings
                //    options.Password.RequireDigit = true;
                //    options.Password.RequiredLength = 8;
                //    options.Password.RequireNonAlphanumeric = true;
                //    options.Password.RequireUppercase = true;
                //    options.Password.RequireLowercase = true;
                //    options.Password.RequiredUniqueChars = 6;

                //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    options.Lockout.MaxFailedAccessAttempts = 3;

                //    options.SignIn.RequireConfirmedEmail = true;

                //    options.User.RequireUniqueEmail = true;
                //});

                ////startup.cs
                //services.ConfigureApplicationCookie(options =>
                //{
                //    options.Cookie.HttpOnly = true;
                //    options.Cookie.Expiration = TimeSpan.FromHours(1);
                //    options.SlidingExpiration = true;
                //});

                // Specify email configuration in ASP.NET Core
                var emailConfig = this.configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
                services.AddSingleton(emailConfig);
                services.Configure<FormOptions>(o =>
                {
                    o.ValueLengthLimit = int.MaxValue;
                    o.MultipartBodyLengthLimit = int.MaxValue;
                    o.MemoryBufferThreshold = int.MaxValue;
                });

                services.AddScoped<IEmailSender, EmailSender>();

                if (!string.Equals(environment.EnvironmentName, "Development"))
                {
                    services.AddHostedService<IgmiteHostedService>();
                }

                // All user accounts on the machine can decrypt the keys
                //services.AddDataProtection().ProtectKeysWithDpapi(protectToLocalMachine: true);

                // using Microsoft.AspNetCore.DataProtection;
                //services.AddDataProtection()
                //    .PersistKeysToFileSystem(new DirectoryInfo("etc/keys/"))
                //    .SetApplicationName("Lighthouse 2.0");

                //            services.AddDataProtection()
                //.PersistKeysToFileSystem(new DirectoryInfo("etc/keys"))
                //.SetApplicationName("Lighthouse 2.0")
                //.SetDefaultKeyLifetime(TimeSpan.FromDays(365));

                //services.AddDataProtection().DisableAutomaticKeyGeneration();

                //services.AddDataProtection().SetDefaultKeyLifetime(TimeSpan.FromDays(14));
                // use 14-day lifetime instead of 90-day lifetime

                #region Registered manager & repository of the lighthouse

                services.AddTransient<IAcademicYearManager, AcademicYearManager>();
                services.AddTransient<IAcademicYearRepository, AcademicYearRepository>();
                services.AddTransient<IAccountTransactionManager, AccountTransactionManager>();
                services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>();
                services.AddTransient<IAccountManager, AccountManager>();
                services.AddTransient<IAuthManager, AuthManager>();
                services.AddTransient<IAccountRepository, AccountRepository>();
                services.AddTransient<ICommonManager, CommonManager>();
                services.AddTransient<ICommonRepository, CommonRepository>();
                services.AddTransient<ICountryManager, CountryManager>();
                services.AddTransient<ICountryRepository, CountryRepository>();
                services.AddTransient<ICourseMaterialManager, CourseMaterialManager>();
                services.AddTransient<ICourseMaterialRepository, CourseMaterialRepository>();
                services.AddTransient<ICourseModuleManager, CourseModuleManager>();
                services.AddTransient<ICourseModuleRepository, CourseModuleRepository>();
                services.AddTransient<IDataTypeManager, DataTypeManager>();
                services.AddTransient<IDataTypeRepository, DataTypeRepository>();
                services.AddTransient<IDataValueManager, DataValueManager>();
                services.AddTransient<IDataValueRepository, DataValueRepository>();
                services.AddTransient<IDistrictManager, DistrictManager>();
                services.AddTransient<IDistrictRepository, DistrictRepository>();
                services.AddTransient<IDivisionManager, DivisionManager>();
                services.AddTransient<IDivisionRepository, DivisionRepository>();
                services.AddTransient<IEmployeeManager, EmployeeManager>();
                services.AddTransient<IEmployeeRepository, EmployeeRepository>();
                services.AddTransient<IEmployerManager, EmployerManager>();
                services.AddTransient<IEmployerRepository, EmployerRepository>();
                services.AddTransient<IErrorLogManager, ErrorLogManager>();
                services.AddTransient<IErrorLogRepository, ErrorLogRepository>();
                services.AddTransient<IForgotPasswordHistoryManager, ForgotPasswordHistoryManager>();
                services.AddTransient<IForgotPasswordHistoryRepository, ForgotPasswordHistoryRepository>();
                services.AddTransient<IHeadMasterManager, HeadMasterManager>();
                services.AddTransient<IHeadMasterRepository, HeadMasterRepository>();
                services.AddTransient<IHMIssueReportingManager, HMIssueReportingManager>();
                services.AddTransient<IHMIssueReportingRepository, HMIssueReportingRepository>();
                services.AddTransient<IJobRoleManager, JobRoleManager>();
                services.AddTransient<IJobRoleRepository, JobRoleRepository>();
                services.AddTransient<IPhaseManager, PhaseManager>();
                services.AddTransient<IPhaseRepository, PhaseRepository>();
                services.AddTransient<IRoleManager, RoleManager>();
                services.AddTransient<IRoleRepository, RoleRepository>();
                services.AddTransient<IRoleTransactionManager, RoleTransactionManager>();
                services.AddTransient<IRoleTransactionRepository, RoleTransactionRepository>();
                services.AddTransient<ISchoolManager, SchoolManager>();
                services.AddTransient<ISchoolRepository, SchoolRepository>();
                services.AddTransient<ISchoolCategoryManager, SchoolCategoryManager>();
                services.AddTransient<ISchoolCategoryRepository, SchoolCategoryRepository>();
                services.AddTransient<ISchoolClassManager, SchoolClassManager>();
                services.AddTransient<ISchoolClassRepository, SchoolClassRepository>();
                services.AddTransient<ISchoolVEInchargeManager, SchoolVEInchargeManager>();
                services.AddTransient<ISchoolVEInchargeRepository, SchoolVEInchargeRepository>();
                services.AddTransient<ISchoolVTPSectorManager, SchoolVTPSectorManager>();
                services.AddTransient<ISchoolVTPSectorRepository, SchoolVTPSectorRepository>();
                services.AddTransient<ISectionManager, SectionManager>();
                services.AddTransient<ISectionRepository, SectionRepository>();
                services.AddTransient<ISectorManager, SectorManager>();
                services.AddTransient<ISectorRepository, SectorRepository>();
                services.AddTransient<ISectorJobRoleManager, SectorJobRoleManager>();
                services.AddTransient<ISectorJobRoleRepository, SectorJobRoleRepository>();
                services.AddTransient<ISiteHeaderManager, SiteHeaderManager>();
                services.AddTransient<ISiteHeaderRepository, SiteHeaderRepository>();
                services.AddTransient<ISiteSubHeaderManager, SiteSubHeaderManager>();
                services.AddTransient<ISiteSubHeaderRepository, SiteSubHeaderRepository>();
                services.AddTransient<IStateManager, StateManager>();
                services.AddTransient<IStateRepository, StateRepository>();
                services.AddTransient<IStudentClassDetailManager, StudentClassDetailManager>();
                services.AddTransient<IStudentClassDetailRepository, StudentClassDetailRepository>();
                services.AddTransient<IStudentClassManager, StudentClassManager>();
                services.AddTransient<IStudentClassRepository, StudentClassRepository>();
                services.AddTransient<ITermsConditionManager, TermsConditionManager>();
                services.AddTransient<ITermsConditionRepository, TermsConditionRepository>();
                services.AddTransient<IToolEquipmentManager, ToolEquipmentManager>();
                services.AddTransient<IToolEquipmentRepository, ToolEquipmentRepository>();
                services.AddTransient<ITransactionManager, TransactionManager>();
                services.AddTransient<ITransactionRepository, TransactionRepository>();
                services.AddTransient<IUserOTPDetailManager, UserOTPDetailManager>();
                services.AddTransient<IUserOTPDetailRepository, UserOTPDetailRepository>();
                services.AddTransient<IVCDailyReportingManager, VCDailyReportingManager>();
                services.AddTransient<IVCDailyReportingRepository, VCDailyReportingRepository>();
                services.AddTransient<IDRPDailyReportingManager, DRPDailyReportingManager>();
                services.AddTransient<IDRPDailyReportingRepository, DRPDailyReportingRepository>();
                services.AddTransient<IVCIssueReportingManager, VCIssueReportingManager>();
                services.AddTransient<IVCIssueReportingRepository, VCIssueReportingRepository>();
                services.AddTransient<IVCSchoolSectorManager, VCSchoolSectorManager>();
                services.AddTransient<IVCSchoolSectorRepository, VCSchoolSectorRepository>();
                services.AddTransient<IVCSchoolVisitManager, VCSchoolVisitManager>();
                services.AddTransient<IVCSchoolVisitRepository, VCSchoolVisitRepository>();
                services.AddTransient<IVCSchoolVisitReportingManager, VCSchoolVisitReportingManager>();
                services.AddTransient<IVCSchoolVisitReportingRepository, VCSchoolVisitReportingRepository>();
                services.AddTransient<IVocationalCoordinatorManager, VocationalCoordinatorManager>();
                services.AddTransient<IVocationalCoordinatorRepository, VocationalCoordinatorRepository>();
                services.AddTransient<IVocationalTrainerManager, VocationalTrainerManager>();
                services.AddTransient<IVocationalTrainerRepository, VocationalTrainerRepository>();
                services.AddTransient<IVocationalTrainingProviderManager, VocationalTrainingProviderManager>();
                services.AddTransient<IVocationalTrainingProviderRepository, VocationalTrainingProviderRepository>();
                services.AddTransient<IVTClassManager, VTClassManager>();
                services.AddTransient<IVTClassRepository, VTClassRepository>();
                services.AddTransient<IVTDailyReportingManager, VTDailyReportingManager>();
                services.AddTransient<IVTDailyReportingRepository, VTDailyReportingRepository>();
                services.AddTransient<IVTFieldIndustryVisitConductedManager, VTFieldIndustryVisitConductedManager>();
                services.AddTransient<IVTFieldIndustryVisitConductedRepository, VTFieldIndustryVisitConductedRepository>();
                services.AddTransient<IVTGuestLectureConductedManager, VTGuestLectureConductedManager>();
                services.AddTransient<IVTGuestLectureConductedRepository, VTGuestLectureConductedRepository>();
                services.AddTransient<IVTIssueReportingManager, VTIssueReportingManager>();
                services.AddTransient<IVTIssueReportingRepository, VTIssueReportingRepository>();
                services.AddTransient<IVTMonthlyTeachingPlanManager, VTMonthlyTeachingPlanManager>();
                services.AddTransient<IVTMonthlyTeachingPlanRepository, VTMonthlyTeachingPlanRepository>();
                services.AddTransient<IVTPMonthlyBillSubmissionStatusManager, VTPMonthlyBillSubmissionStatusManager>();
                services.AddTransient<IVTPMonthlyBillSubmissionStatusRepository, VTPMonthlyBillSubmissionStatusRepository>();
                services.AddTransient<IVTPracticalAssessmentManager, VTPracticalAssessmentManager>();
                services.AddTransient<IVTPracticalAssessmentRepository, VTPracticalAssessmentRepository>();
                services.AddTransient<IVTPSectorManager, VTPSectorManager>();
                services.AddTransient<IVTPSectorRepository, VTPSectorRepository>();
                services.AddTransient<IVTSchoolSectorManager, VTSchoolSectorManager>();
                services.AddTransient<IVTSchoolSectorRepository, VTSchoolSectorRepository>();
                services.AddTransient<IVTStatusOfInductionInserviceTrainingManager, VTStatusOfInductionInserviceTrainingManager>();
                services.AddTransient<IVTStatusOfInductionInserviceTrainingRepository, VTStatusOfInductionInserviceTrainingRepository>();
                services.AddTransient<IVTStudentAssessmentManager, VTStudentAssessmentManager>();
                services.AddTransient<IVTStudentAssessmentRepository, VTStudentAssessmentRepository>();
                services.AddTransient<IVTStudentPlacementDetailManager, VTStudentPlacementDetailManager>();
                services.AddTransient<IVTStudentPlacementDetailRepository, VTStudentPlacementDetailRepository>();
                services.AddTransient<IVTStudentResultOtherSubjectManager, VTStudentResultOtherSubjectManager>();
                services.AddTransient<IVTStudentResultOtherSubjectRepository, VTStudentResultOtherSubjectRepository>();
                services.AddTransient<IVTStudentVEResultManager, VTStudentVEResultManager>();
                services.AddTransient<IVTStudentVEResultRepository, VTStudentVEResultRepository>();
                services.AddTransient<IMessageTemplateManager, MessageTemplateManager>();
                services.AddTransient<IMessageTemplateRepository, MessageTemplateRepository>();

                services.AddTransient<IReportManager, ReportManager>();
                services.AddTransient<IReportRepository, ReportRepository>();
                services.AddTransient<IDashboardManager, DashboardManager>();
                services.AddTransient<IDashboardRepository, DashboardRepository>();

                services.AddTransient<IBroadcastMessageManager, BroadcastMessageManager>();
                services.AddTransient<IBroadcastMessageRepository, BroadcastMessageRepository>();
                services.AddTransient<IIssueMappingManager, IssueMappingManager>();
                services.AddTransient<IIssueMappingRepository, IssueMappingRepository>();
                services.AddTransient<IBlockManager, BlockManager>();
                services.AddTransient<IBlockRepository, BlockRepository>();
                services.AddTransient<IClusterManager, ClusterManager>();
                services.AddTransient<IClusterRepository, ClusterRepository>();
                services.AddTransient<IComplaintRegistrationManager, ComplaintRegistrationManager>();
                services.AddTransient<IComplaintRegistrationRepository, ComplaintRegistrationRepository>();
                services.AddTransient<IStudentsForExitFormManager, StudentsForExitFormManager>();
                services.AddTransient<IStudentsForExitFormRepository, StudentsForExitFormRepository>();
                services.AddTransient<IExitSurveyDetailsManager, ExitSurveyDetailsManager>();
                services.AddTransient<IExitSurveyDetailsRepository, ExitSurveyDetailsRepository>();
                services.AddTransient<IAcademicRolloverManager, AcademicRolloverManager>();
                services.AddTransient<IAcademicRolloverRepository, AcademicRolloverRepository>();

                #endregion Registered manager & repository of the lighthouse
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.ErrorManager.Instance.WriteErrorLogsInFile(ex);

                throw ex;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Added Lighthouse Service Exception Handling
            //app.ConfigureLighthouseServiceExceptionMiddleware();

            //Added Lighthouse Service Explorer
            if (Constants.IsDeveloperMode)
            {
                //app.UseSwaggerDocumentation();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    //http://localhost:61246/swagger/index.html
                    options.SwaggerEndpoint("/swagger/LighthouseServices/swagger.json", string.Format("Lighthouse Services - {0}", Constants.Version));
                    options.DocExpansion(DocExpansion.None);
                });
            }

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !System.IO.Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            // Authorization and Global ErrorHandler Middleware
            app.UseAuthentication(); // Who you are
            //app.UseAuthorization(); // What you can do

            app.UseMiddleware<JwtMiddleware>();
            //app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Reports")), RequestPath = "/Reports"
            //});

            app.UseCors("LighthouseCors");
            app.UseMvc();
        }
    }
}