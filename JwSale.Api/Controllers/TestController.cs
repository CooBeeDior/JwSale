using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Http;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Common;
using JwSale.Packs.Manager;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace JwSale.Api.Controllers
{
    [NoAuthRequired]
    public class TestController : JwSaleControllerBase
    {
        private readonly IRabbitmqPublisher _rabbitmqPublisher;
        private readonly IFreeSql _freesql;
        private readonly IdleBus<IFreeSql> _idleBusFreeSql;
        // private readonly ISpiderHttpClientFactory _spiderHttpClientFactory;
        // private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IUserApi _userApi;
        public TestController(JwSaleDbContext context, IRabbitmqPublisher rabbitmqPublisher, IFreeSql freesql, IdleBus<IFreeSql> idleBusFreeSql
            //ISpiderHttpClientFactory spiderHttpClientFactory, IHttpClientFactory httpClientFactory,
            //IUserApi userApi
            ) : base(context)
        {
            _rabbitmqPublisher = rabbitmqPublisher;
            _freesql = freesql;
            _idleBusFreeSql = idleBusFreeSql;
            //_spiderHttpClientFactory = spiderHttpClientFactory;
            //_httpClientFactory = httpClientFactory;
            //_userApi = userApi;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("api/Test")]
        public ActionResult<ResponseBase> Test()
        {
            ResponseBase response = new ResponseBase();

            response.Message = "it is a test;";
            return response;
        }

        /// <summary>
        /// 测试队列
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/TestEvent")]
        public ActionResult<ResponseBase> TestEvent()
        {
            ResponseBase response = new ResponseBase();

            _rabbitmqPublisher.Publish("it is a test event");
            return response;
        }

        [HttpPost("api/TestFreeSql")]
        public ActionResult<ResponseBase> TestFreeSql()
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            var userInfo = _freesql.Select<UserInfo>().ToOne();
            response.Data = userInfo;
            return response;
        }

        [HttpPost("api/TestIdleBusFreeSql")]
        public ActionResult<ResponseBase> TestIdleBusFreeSql()
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            var freeSqls = _idleBusFreeSql.GetAll();
            var freeSql = freeSqls.FirstOrDefault();
            var userInfo = _freesql.Select<UserInfo>().ToOne();
            response.Data = userInfo;
            return response;
        }

    }
}
