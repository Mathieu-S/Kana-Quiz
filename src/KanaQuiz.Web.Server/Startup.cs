using System;
using KanaQuiz.Core.Repositories;
using KanaQuiz.Core.Services;
using KanaQuiz.Infrastructure;
using KanaQuiz.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KanaQuiz.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            // DbContexts
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("KANAQUIZ_DB")))
            {
                services.AddDbContext<KanaQuizContext>(option => option.UseInMemoryDatabase("KANAQUIZ_DB"));
            }
            else
            {
                services.AddDbContext<KanaQuizContext>(option => option.UseNpgsql(Environment.GetEnvironmentVariable("KANAQUIZ_DB")));
            }

            // Repositoies
            services.AddScoped<IKanaRepository, KanaRepository>();

            // Services
            services.AddScoped<QuizFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}