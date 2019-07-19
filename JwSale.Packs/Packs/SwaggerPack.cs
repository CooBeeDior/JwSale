using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace JwSale.Packs.Packs
{

    [Pack("Swagger模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public class SwaggerPack : JwSalePack
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            var configuration = services.GetSingletonInstance<IConfiguration>();
            var jwSaleOptions = configuration.Get<JwSaleOptions>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", jwSaleOptions?.Swagger?.Info ?? new Info
                {
                    Version = "v1",
                    Title = "扫码Api",
                    Description = "扫码Api",

                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "JwSale.Api.xml");
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("Bearer", jwSaleOptions?.Swagger?.ApiKeyScheme ?? new ApiKeyScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            return services;
        }
        protected override void UsePack(IApplicationBuilder app)
        {

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "扫码Api v1");
                options.DocumentTitle = "扫码Api"; 

            });
        }


    }
}
