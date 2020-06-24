using System;
using System.IO;
using Hanabi.Flow.API.Extensions;
using Hanabi.Flow.Common.Helpers;
using Hanabi.Flow.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hanabi.Flow.API
{
    public class Startup
    {
        // 类中定义常量,方便后续编辑
        private const string _apiVersion = "V1.0";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
            
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new AppSettings(Configuration));
            services.AddScoped<MyContext>();
            // ConfigureServices函数内添加代码
            services.AddSwaggerGen(setup =>
            {
                // 设置Swagger文档的名称，描述信息等
                setup.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "后端API说明文档",
                    Description = "具体描述见各API详情",
                    Contact = new OpenApiContact { Name = "HANABI", Email = "Narancia86@outlook.com", Url = new Uri("https://colasaikou.com/") },
                    License = new OpenApiLicense { Name = "HANABI", Url = new Uri("https://colasaikou.com/") }
                });

                // 设置API的排序规则
                setup.OrderActionsBy(description => description.RelativePath);

                // 设置接口注释信息
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Hanabi.Flow.API.xml");
                setup.IncludeXmlComments(xmlPath, true);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext myContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", _apiVersion);

                // 在launchSettings.json把launchUrl设置为空,表示程序启动时访问根域名,并且在这里把Swagger页面路由前缀设置为空,即可在API根域名访问Swagger界面
                c.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDataGenerator(myContext);
        }
    }
}
