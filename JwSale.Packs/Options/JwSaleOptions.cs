﻿using JwSale.Model.Enums;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Options
{


    public class JwSaleOptions
    {

        public SwaggerOptions Swagger { get; set; }


        public IList<JwSaleSqlServerOptions> JwSaleSqlServers { get; set; }


        public HangFireOptios HangFire { get; set; }

        public RedisOptions Redis { get; set; }

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


}
