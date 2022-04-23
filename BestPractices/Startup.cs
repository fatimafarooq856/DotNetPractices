using BAL.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace BestPractices
{
    public class Startup
    {
        //public void Configure(IApplicationBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //}
        private IConfiguration configuration { get; }
        //private Settings settings = new Settings();
        //private Jwt jwt = new Jwt();
        public Startup(IWebHostEnvironment env)
        {
            //Static.WebHostEnvironment = env;
            configuration = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {

            #region Inject Services

            services.AddScoped<IUserInterface, User>();           

            #endregion

            //services.Configure<AuthCredential>(configuration.GetSection("AuthCredential"));
            //services.Configure<Jwt>(configuration.GetSection("Jwt"));
            //services.Configure<Settings>(configuration.GetSection("Settings"));
            //configuration.GetSection("Settings").Bind(settings, c => c.BindNonPublicProperties = true);
            //configuration.GetSection("Jwt").Bind(jwt, c => c.BindNonPublicProperties = true);
            //Static.Settings = settings;
            //Static.Jwt = jwt;
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(
            //      "CorsPolicy",
            //      builder => builder.WithOrigins(Static.Settings.CorsUrl)
            //      .AllowAnyMethod()
            //      .AllowAnyHeader()
            //      .AllowCredentials());
            //});
            //services.AddDbContext<KioskContext>(item => item.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddApiVersioning(x =>
            //{
            //    x.DefaultApiVersion = new ApiVersion(1, 0);
            //    x.AssumeDefaultVersionWhenUnspecified = true;
            //    x.ReportApiVersions = true;
            //});

            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddEntityFrameworkStores<KioskContext>()
            //    .AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 3;
            //});
            //LgAAAB_@_LCAAAAAAAAApzqDQyKfHRNLY1NbENyM80VFVRtrQ0NzYztLKwsDQ2MHIwNo6Njo830DY00tXVBgAGvJ4yLgAAAA_!__!_
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
               // c.SwaggerDoc("v1", new OpenApiInfo { Title = "Best practices API", Version = "1.0" });
                //c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                //{
                //    Description = "OAuth2",
                //    Name = "auth",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                //    {
                //      new OpenApiSecurityScheme
                //      {
                //        Reference = new OpenApiReference
                //          {
                //            Type = ReferenceType.SecurityScheme,
                //            Id = "OAuth2"
                //          },
                //          Scheme = "oauth2",
                //          Name = "auth",
                //          In = ParameterLocation.Header,

                //        },
                //        new List<string>()
                //      }
                //    });
            });
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //The AddAutoMapper method provided by the AutoMapper package which traverse the assembly and checks for the class which inherits from the Profile class of AutoMapper
            
            //var serviceProvider = services.BuildServiceProvider();
            //var accessor = serviceProvider.GetService<IHttpContextAccessor>();
            //var env = serviceProvider.GetService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            //Static.SetHttpContextAccessor(accessor, env);
            services.AddDistributedMemoryCache();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
               // c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kiosk API versio 1.0");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<OAuth>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
        }
    }
}
