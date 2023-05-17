using FeignCore.Apis;
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
using Microsoft.Extensions.Localization;
using RabbitmqCore;
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
        private readonly IStringLocalizer<TestController> _stringLocalizer;
        // private readonly ISpiderHttpClientFactory _spiderHttpClientFactory;
        // private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserApi _userApi;
        public TestController(JwSaleDbContext context, IRabbitmqPublisher rabbitmqPublisher,
            IFreeSql freesql, IdleBus<IFreeSql> idleBusFreeSql,
            IStringLocalizer<TestController> stringLocalizer,
        //ISpiderHttpClientFactory spiderHttpClientFactory, IHttpClientFactory httpClientFactory,
        IUserApi userApi
            ) : base(context)
        {
            _rabbitmqPublisher = rabbitmqPublisher;
            _freesql = freesql;
            _idleBusFreeSql = idleBusFreeSql;
            _stringLocalizer = stringLocalizer;
            //_spiderHttpClientFactory = spiderHttpClientFactory;
            //_httpClientFactory = httpClientFactory;
            _userApi = userApi;
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
        /// <summary>
        /// FreeSql测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/TestFreeSql")]
        public ActionResult<ResponseBase> TestFreeSql()
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            var userInfo = _freesql.Select<UserInfo>().ToOne();
            response.Data = userInfo;
            return response;
        }

        /// <summary>
        /// IdleBusFreeSql测试
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Feign测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/TestFeign")]
        public async Task<ActionResult<ResponseBase>> TestFeign()
        {
            ResponseBase response = new ResponseBase();
            var result = await _userApi.GetQrCode();
            response.Message = result?.ToJson();
            return response;
        }

        /// <summary>
        /// 本地化测试
        /// url：
        ///?culture=zh-CN&ui-culture=zh-CN
        ///?culture = zh-CN
        ///?ui-culture=zh-CN  
        ///cookie：
        ///键名称：.AspNetCore.Culture
        ///值内容：
        ///c=zh-CN|uic=zh-CN
        ///c = zh - CN
        ///uic=zh-CN  
        ///Header:
        ///Accept-Language:zh-CN,zh;q=0.9
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/TestLocalizedString")]
        public ActionResult<ResponseBase> TestLocalizedString()
        {
            ResponseBase<IEnumerable<LocalizedString>> response = new ResponseBase<IEnumerable<LocalizedString>>();
            var result = _stringLocalizer.GetAllStrings();
            response.Data = result;
            return response;
        }


    }
}
