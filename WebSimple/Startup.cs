using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebSimple
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        //依赖注入 configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var myClass = new Class();
                Configuration.Bind(myClass); //配置信息绑定到类

                await context.Response.WriteAsync($"ClassNo:{myClass.ClassNo}");
                await context.Response.WriteAsync($"ClassDesc:{myClass.ClassDesc}");
                await context.Response.WriteAsync($"StudentsCount:{myClass.Students.Count}");

                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
