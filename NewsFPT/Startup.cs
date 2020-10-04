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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DAL.Models;
using NewsFPT.DAL.UnitOfWork;
using BLL.IService;
using BLL.Serivce;
using NewsFPT.DAL.UnitOfWorks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NewsFPT
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
            services.AddDbContext<NewsFPTContext>();
            services.AddControllers();

            // register swwager generrator
            services.AddSwaggerGen(c =>
           c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API for News FPT", Version = "v1" }
           //add authorzied
           //, c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
           //{
           //    Name = "Authorization",
           //    Type = SecuritySchemeType.ApiKey,
           //    BearerFormat = "JWT",
           //    In = ParameterLocation.Header,
           //    Scheme = "Bearer",
           //    Description = "Please insert JWT token into field"
           //}),
           //c.AddSecurityRequirement(new OpenApiSecurityRequirement
           //    {
           //         {
           //         new OpenApiSecurityScheme
           //         {
           //         Reference = new OpenApiReference
           //         {
           //             Type = ReferenceType.SecurityScheme,
           //             Id = "Bearer"
           //         },
           //         Scheme = "oauth2",
           //         Name = "Bearer",
           //         In = ParameterLocation.Header,
           //         },
           //         new string[] { }
           //         }
           //    }
           //))
           ));
            // register authentication jst
            //services.AddAuthentication(JwtBearerDefaults.);

            // register dependecy injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INewsTagService, NewsTagService>();
            services.AddScoped<IChannelService, ChannelService>();


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

            //configure swagger 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
