using JwSale.Model.Enums;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Options
{
    public class JwSaleOptions
    {
        /// <summary>
        /// 加密的tokenKey
        /// </summary>
        public string TokenKey { get; set; }

        /// <summary>
        /// swagger配置
        /// </summary>
        public SwaggerOptions Swagger { get; set; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        public IList<JwSaleSqlServerOptions> JwSaleSqlServers { get; set; }

        /// <summary>
        /// 定时任务hangfire配置
        /// </summary>
        public HangFireOptios HangFire { get; set; }

        /// <summary>
        /// redis缓存配置
        /// </summary>
        public RedisOptions Redis { get; set; }

        /// <summary>
        /// 异常错误日志配置
        /// </summary>
        public ExceptionlessOptions Exceptionless { get; set; }

        /// <summary>
        /// rabbitmq消息队列配置
        /// </summary>
        public RabbitmqOptions Rabbitmq { get; set; }

    }





    public class JwSaleSqlServerOptions
    {
        public string DbContextTypeName { get; set; }

        public string ConnectionString { get; set; }

        public DatabaseType DatabaseType { get; set; }

        public bool UseLazyLoadingProxies { get; set; }
    }




    public class SwaggerOptions
    {
        public Info Info { get; set; }

        public ApiKeyScheme ApiKeyScheme { get; set; }

        public IList<string> XmlCommentPaths { get; set; }

    }

    public class HangFireOptios
    {
        public bool Enabled { get; set; }

        public string ConnectionString { get; set; }
    }



    public class RedisOptions
    {
        public bool Enabled { get; set; }

        public string Configuration { get; set; }

        public string InstanceName { get; set; }
    }

    public class ExceptionlessOptions
    {
        public string ApiKey { get; set; }

        public string ServerUrl { get; set; }
    }

    public class RabbitmqOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostUrl { get; set; }

    }

}
