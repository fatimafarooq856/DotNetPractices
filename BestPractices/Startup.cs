
using App.Common.Campatibility.Configurations;
using App.Common.Campatibility.Filters;
using App.Common.Compability.Middlewares;
using App.Utils.Entities;
using BAL;
using BAL.UserLoginInfo;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

namespace BestPractices
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json")
                 // .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Removes default console logger
            services.AddLogging(configuration => configuration.ClearProviders());
            // Configure options
            services.Configure<AppApiOptions>(_configuration);
            var apiOptions = new AppApiOptions();
            _configuration.Bind(apiOptions);
            #region Inject Services
            services.AddScoped<IUserInterface, UserService>();
            services.AddScoped<IProductInterface, ProductService>();
            services.AddScoped<IUsersLoginInfoService, UsersLoginInfoService>();
            #endregion
            services.AddDbContext<Context>(item => item.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), x => x.UseNodaTime()));
            services.AddIdentity<User, ApplicationRole>()
                  .AddEntityFrameworkStores<Context>()
                  .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });           
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                
            });
            services.AddControllers(configuration =>
            {
                configuration.Filters.Add(typeof(HttpExceptionFilter));
                configuration.Filters.Add(typeof(ValidateModelAttribute));
            })
                .ConfigureAppJsonOptions();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //The AddAutoMapper method provided by the AutoMapper package which traverse the assembly and checks for the class which inherits from the Profile class of AutoMapper

            var serviceProvider = services.BuildServiceProvider();
            var accessor = serviceProvider.GetService<IHttpContextAccessor>();
            var env = serviceProvider.GetService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            //Static.SetHttpContextAccessor(accessor, env);
            services.AddDistributedMemoryCache();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<AppApiOptions> options,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            loggerFactory.AddSerilog();

            //ApiConfigurationSwaggerHelper.Configure(app);
            app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"); });
            // ConfigureConnections(app, options.Value);
            if (options.Value.UseHttps)
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithOrigins("http://localhost:4200", "http://localhost:3000");
                    builder.AllowCredentials();
                });
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            ////
            app.UseMiddleware<LogRequestMiddleware>();

            //if (env.IsDevelopment())
            //{
            //	app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    

            hostApplicationLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            
        }
    }
}
