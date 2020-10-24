using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ResourceSkillMatching.Data;
using ResourceSkillMatching.Data.Interfaces;
using ResourceSkillMatching.Services;

namespace ResourceSkillMatching
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResourceSkillMatchingAPI", Version = "v1" });
            });
            services.AddControllers();
            services.AddSingleton(p => new DbContext(Configuration.GetConnectionString("SetupDBConnectionString"), Configuration));
            services.AddScoped<IResourceSkillMatchingService, ResourceSkillMatchingService>();
            services.AddScoped<IResourceSkillMatchingRepository, ResourceSkillMatchingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResourceSkillMatchingAPI");
            });
        }
    }
}
