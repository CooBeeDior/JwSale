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
using System.Collections.Generic;
using System.Linq;

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
                    Title = "微信Api",
                    Description = "QQ：506599090",

                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）

                var xmlPath = Path.Combine(basePath, "JwSale.Api.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                xmlPath = Path.Combine(basePath, "JwSale.Model.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                options.AddSecurityDefinition("Bearer", jwSaleOptions?.Swagger?.ApiKeyScheme ?? new ApiKeyScheme
                {
                    Description = "请输入Token",
                    Name = "Authorization",
                    In = "Header",
                    Type = "apiKey",
                });

                //Json Token认证方式，此方式为全局添加
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "微信Api v1");
                options.DocumentTitle = "微信接口说明文档";

            });
        }


    }
}
