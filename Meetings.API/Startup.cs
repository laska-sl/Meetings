using System;

using AutoMapper;

using MediatR;

using Meetings.CQRS.Handlers;
using Meetings.Data.Data;
using Meetings.Data.Helpers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Meetings.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(GetMeetingsHandler).Assembly);

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Meetings API",
                    Description = "A simple meetings service",
                    Contact = new OpenApiContact
                    {
                        Name = "Vyacheslav Lasukov",
                        Email = "lasukovv902@gmail.com",
                        Url = new Uri("https://github.com/laska-sl")
                    }
                });

                c.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetings API");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
