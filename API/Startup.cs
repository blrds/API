using API.Data;
using API.Data.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using API.Data.Contexts;
using Newtonsoft.Json.Serialization;

namespace API
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
            services.AddDbContext<AreaerContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddDbContext<ExperienceerContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddDbContext<SkillerContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddDbContext<SpecializationerContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddDbContext<VacancierContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddDbContext<VacancySkillerContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddDbContext<VacancySpecializationerContext>(opt => opt.UseMySql(
                Configuration.GetConnectionString("CommanderConnection"), new MySqlServerVersion(new Version("8.0.27"))));
            services.AddControllers().AddNewtonsoftJson(s=>
                s.SerializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAreaerRepo, SqlAreaerRepo>();
            services.AddScoped<IExperienceerRepo, SqlExperienceerRepo>();
            services.AddScoped<ISkillerRepo, SqlSkillerRepo>();
            services.AddScoped<ISpecializationerRepo, SqlSpecializationerRepo>();
            services.AddScoped<IVacancierRepo, SqlVacancierRepo>();
            services.AddScoped<IVacancySkillerRepo, SqlVacancySkillerRepo>();
            services.AddScoped<IVacancySpecializationerRepo, SqlVacancySpecializationerRepo>();
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
        }
    }
}
