using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            _logger.LogInformation("Logging from ConfigureServices.");
            services.Configure<WorkoutsDatabaseSettings>(
                    // Configuration.GetSection(nameof(WorkoutsDatabaseSettings)));
                    Configuration);

            services.AddSingleton<IWorkoutsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WorkoutsDatabaseSettings>>().Value);
            
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
                        builder.AllowAnyOrigin();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("Configuring for Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseMvc();

        }
    }
}
