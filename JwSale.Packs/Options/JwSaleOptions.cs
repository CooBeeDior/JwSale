﻿using FreesqlCore;
using JwSale.Model.Enums;
using LocalizerAbstraction;
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


        /// <summary>
        /// FreeSql批量处理配置
        /// </summary>
        public FreeSqlBusOptions FreeSqlBus { get; set; }

        /// <summary>
        /// FreeSql配置
        /// </summary>
        public FreeSqlDbOptions FreeSql { get; set; }


        ///// <summary>
        ///// ElasticSearch配置
        ///// </summary>
        //public ElasticSearchOptions ElasticSearch { get; set; }

        ///// <summary>
        ///// Spider配置
        ///// </summary>
        //public SpiderOptions Spider { get; set; }



        ///// <summary>
        /////  MongoDb配置
        ///// </summary>
        //public MongoDbOptions MongoDb { get; set; }


        ///// <summary>
        ///// mongodb本地化配置
        ///// </summary>
        public MongodbLocalizerOptions MongodbLocalizer { get; set; }

        ///// <summary>
        ///// 限流配置
        ///// </summary>
        //public RateLimitingOptions RateLimiting { get; set; }

        /// <summary>
        /// 微信小程序配置
        /// </summary>
        public WxMiniProgramOptions WxMiniProgram { get; set; }

    }





    public class JwSaleSqlServerOptions
    {
        /// <summary>
        /// 数据库上下文名称
        /// </summary>
        public string DbContextTypeName { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }
        /// <summary>
        /// 是否启动延迟加载
        /// </summary>
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
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }
    }



    public class RedisOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 连接配置
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }
    }

    public class ExceptionlessOptions
    {
        public string ApiKey { get; set; }

        public string ServerUrl { get; set; }
    }

    public class RabbitmqOptions
    {
        /// <summary>
        /// 连接地址
        /// </summary>
        public string HostUrl { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }



    }

    public class WxMiniProgramOptions
    {
        /// <summary>
        /// 小程序名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 小程序 appId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 小程序 appSecret
        /// </summary>
        public string AppSecret { get; set; }
    }

}
