using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestTaskApi.Mapper;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.Models;

namespace TestTaskApi
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
            services.AddMvc();
            services.AddAutoMapper();
            
            var mapper = AutoMapperConfig.CreateAndRegisterMapper();
            services.AddScoped<MapperConfiguration>(x => mapper);
            services.AddScoped<IMapper>(x => mapper.CreateMapper());
                        services.AddDbContext<TestTaskApiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            app.UseMvc();
        }
    }
}
