using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TripTrackerServices.Model;
using Swashbuckle.AspNetCore.Swagger;
using TripTrackerServices.Data;
using Microsoft.EntityFrameworkCore;

namespace TripTracker_Services
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
            //services.AddTransient<Repository>();
            services.AddMvc();
            services.AddDbContext<TripContext>(options => options.UseSqlite("Data Source =efftrips.db"));

            services.AddSwaggerGen(c =>

            {
                c.SwaggerDoc("v5", new Info
                {
                    Version = "v5",
                    Title = "Trip Tracker",
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            if(env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v5/swagger.json", "Trip Tracker v1"));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            TripContext.SeedData(app.ApplicationServices);
        }
    }
}
