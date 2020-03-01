using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;

using portal.webapi.Models;
using portal.webapi.Repository;
using portal.webapi.Services;

namespace portal.webapi
{
    public class Startup
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        public Startup(IConfiguration configuration, ILogger<Startup> logger, ILoggerFactory  loggerFactory)
        {
            Configuration = configuration;
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            // The following will be picked up by Application Insights.
            //_logger.LogInformation("Logging from ConfigureServices.");
            services.Configure<WorkoutsDatabaseSettings>(
                    // Configuration.GetSection(nameof(WorkoutsDatabaseSettings)));
                    Configuration);

            services.AddSingleton<IWorkoutsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WorkoutsDatabaseSettings>>().Value);

            services.ConfigureAll<IMongoCollection<Workout>>(x =>
            {
                WorkoutsDatabaseSettings settings;
                IMongoDatabase db = GetMongoDatabase(Configuration, out settings);
                var collection = db.GetCollection<Workout>(settings.WorkoutsCollectionName);
            });

            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            
            services.AddSingleton<WorkoutService>(sp => new WorkoutService(new ProductionRepository(), Configuration, new WorkoutsDatabaseSettings()));
            // services.AddSingleton<WorkoutRepository>(sp => new WorkoutRepository(Configuration, new WorkoutsDatabaseSettings(), _loggerFactory));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add validations
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<Workout>, WorkoutValidator>();

            // CORS
            // TODO: Remove AllowAnyOrigin or change it to only be added in the development enviornment
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
            });
        }

        public IMongoDatabase GetMongoDatabase(IConfiguration config, out WorkoutsDatabaseSettings settings)
        {
            settings = new WorkoutsDatabaseSettings;
            if(config == null)
            {
                throw new InvalidOperationException("No IConfiguration found in ProductionRepository");
            }
            if(settings == null)
            {
                throw new InvalidOperationException("No WorkoutsDatabaseSettings found in ProductionRepository");
            }
            string settingsObjectName = settings.GetType().ToString().Split('.').Last();
            // Then bind it to the IWorkoutDatabaseSettings object
            config.GetSection(settingsObjectName).Bind(settings);
            // Init connection string and connect to db
            settings.ConnectionString = ModifyMongoConnectionString(settings, config);
            MongoClient _client = new MongoClient(settings.ConnectionString);
            return _client.GetDatabase(settings.DatabaseName);
        }
        public string ModifyMongoConnectionString(WorkoutsDatabaseSettings settings, IConfiguration config)
        {
            ConfigurationService service = new ConfigurationService(settings, config);
            return service.ConnectionStringBuilder();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentName.Contains("Development"))
            {
                _logger.LogInformation("Configuring for Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                _logger.LogInformation("Configuring for Production environment");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseMvc();

        }
    }
}
