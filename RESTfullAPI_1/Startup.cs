using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using RESTfullAPI_1.Entities;
using RESTfullAPI_1.Models;
using RESTfullAPI_1.Services;
using RESTfullAPI_1.Helpers;

namespace RESTfullAPI_1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddDbContext<LibraryContext>(o => o.UseSqlServer("Data Source=DESKTOP-HNVF6ET;Initial Catalog=MyTestDB;Integrated Security=True"));
            
            services.AddScoped<ILibraryRepository, LibraryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILibraryRepository rep)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            AutoMapper.Mapper.Initialize(m => m.CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.Name, p => p.MapFrom(l => $"{l.FirstName} {l.LastName}"))
            .ForMember(dat => dat.Age, p => p.MapFrom(t => t.DateOfBirth.GetCurrentAge()))
            .ForMember(genre=>genre.Genre,p=>p.MapFrom(t=>t.Genre))
            .ForMember(id=>id.Id,i=>i.MapFrom(t=>t.Id))
            );

            // app.UseMvc();
            app.UseMvcWithDefaultRoute();
           

        }
    }
}
