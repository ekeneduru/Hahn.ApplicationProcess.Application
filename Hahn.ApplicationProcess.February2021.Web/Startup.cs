using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicationProcess.February2021.Domain.DBContext;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.Validators;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Data.Services;
using Hahn.ApplicationProcess.February2021.Data;
using Swashbuckle.AspNetCore;
using Swashbuckle.Examples;


namespace Hahn.ApplicationProcess.February2021.Web
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


            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddRazorPages();

            services.AddMvc(setup => {
              
                
            }).AddFluentValidation();
            services.AddTransient<IValidator<Asset>, AssetValidator>();
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IExternalService, ExternalService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hahn API",
                    Description = "This Hahn API",
                    TermsOfService = new Uri("https://Hahn.com/terms"),

                    Contact = new OpenApiContact
                    {
                        Name = "Ekene Duru",
                        Email = "ekene@hahn.com",
                        Url = new Uri("https://hahn.com/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under Hahn",
                        Url = new Uri("https://hahn.com/license"),
                    }
                });
             
              });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn API");
                
                //c.RoutePrefix = string.Empty;

            });
            app.UseCors(o => o.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            InitializeDb(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private async void InitializeDb(IApplicationBuilder app)
        {
            try
            {
                using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                    await db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
