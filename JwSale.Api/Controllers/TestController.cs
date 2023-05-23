using FeignCore.Apis;
using JwSale.Api.Attributes;
using JwSale.Api.Events;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RabbitmqCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 测试接口使用
    /// </summary>
    [NoAuthRequired]
    public class TestController : JwSaleControllerBase
    {
        private readonly IRabbitmqPublisher<TestEvent> _rabbitmqPublisher;
        private readonly IFreeSql _freesql;
        private readonly IdleBus<IFreeSql> _idleBusFreeSql;
        private readonly IStringLocalizer<TestController> _stringLocalizer;
        // private readonly ISpiderHttpClientFactory _spiderHttpClientFactory;
        // private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserApi _userApi;
        public TestController(JwSaleDbContext context, IRabbitmqPublisher<TestEvent> rabbitmqPublisher,
            IFreeSql freesql, IdleBus<IFreeSql> idleBusFreeSql,
            IStringLocalizer<TestController> stringLocalizer,
        //ISpiderHttpClientFactory spiderHttpClientFactory, IHttpClientFactory httpClientFactory,
        IUserApi userApi) : base()
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
        /// <returns></returns>
        [HttpPost("api/test")]
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
        [HttpPost("api/testevent")]
        public ActionResult<ResponseBase> TestEvent()
        {
            ResponseBase response = new ResponseBase();

            _rabbitmqPublisher.Publish(new TestEvent("it is a test event"));
            return response;
        }
        /// <summary>
        /// FreeSql测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/testfreesql")]
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
        [HttpPost("api/testidlebusfreesql")]
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
        [HttpPost("api/testfeign")]
        public async Task<ActionResult<ResponseBase>> TestFeign()
        {
            ResponseBase response = new ResponseBase();
            var result = await _userApi.GetQrCode();
            response.Message = result?.ToJson();
            return response;
        }

        /// <summary>
        /// 本地化测试
        /// </summary>
        ///<example>例1：【 url：?culture=en-US&ui-culture=zh-CN】  例2：【cookie：键名称：.AspNetCore.Culture 值内容：c=en-US|uic=zh-CN 】 例3：【Header:Accept-Language:zh-CN,zh;q=0.9】 </example> 
        /// <returns></returns>
        [HttpPost("api/testlocalizedstring")]
        public ActionResult<ResponseBase> TestLocalizedString()
        {
            var ss = CultureInfo.CurrentCulture;
            var s11s = CultureInfo.CurrentUICulture;
            ResponseBase<IEnumerable<LocalizedString>> response = new ResponseBase<IEnumerable<LocalizedString>>();
            var result = _stringLocalizer.GetAllStrings();
            response.Data = result;
            return response;
        }


    }
}
