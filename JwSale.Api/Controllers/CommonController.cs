using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Http;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Common;
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
    public class CommonController : JwSaleControllerBase
    {
        public CommonController(JwSaleDbContext context) : base(context)
        {

        }

        /// <summary>
        /// 获取16进制字符串(Form表单)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("api/Common/GetHexBufferFormToArray")]
        public ActionResult<ResponseBase> GetHexBufferFormToArray([FromForm]IFormFile file)
        {
            ResponseBase<GetHexBufferListResponse> response = new ResponseBase<GetHexBufferListResponse>();
            GetHexBufferListResponse getHexBufferListResponse = new GetHexBufferListResponse();
            IList<GetHexBufferResponse> list = new List<GetHexBufferResponse>();
            if (file == null)
            {
                response.Success = false;
                response.Message = "文件不能为空";
            }
            else
            {
                var sm = file.OpenReadStream();
                var buffer = sm.ToBuffer();            
                int currentIndex = 0;
                int size = 50000;
                while (buffer.Length > currentIndex)
                {
                    GetHexBufferResponse getHexBufferResponse = new GetHexBufferResponse();
                    getHexBufferResponse.Index = currentIndex;
                    int currentSize = size;
                    if (buffer.Length - currentIndex < size)
                    {
                        currentSize = buffer.Length - currentIndex;
                    }

                 
                    getHexBufferResponse.HexStr = buffer.Skip(currentIndex).Take(currentSize).ToArray().HexBufferToStr();
                    getHexBufferResponse.Length = currentSize;
                    list.Add(getHexBufferResponse);
                    currentIndex += currentSize;
                }
                getHexBufferListResponse.List = list;
                getHexBufferListResponse.TotalLength = buffer.Length;
            }

            response.Data = getHexBufferListResponse;
            return response;
        }


        /// <summary>
        /// 获取16进制字符串
        /// </summary>
        /// <param name="getHexBuffer"></param>
        /// <returns></returns>
        [HttpPost("api/Common/GetHexBufferToArray")]
        public ActionResult<ResponseBase> GetHexBufferToArray(GetHexBufferRequest getHexBuffer)
        {
            ResponseBase<GetHexBufferListResponse> response = new ResponseBase<GetHexBufferListResponse>();
            GetHexBufferListResponse getHexBufferListResponse = new GetHexBufferListResponse();
            string base64 = null;
            var base64Arr = getHexBuffer.Base64.Split(',');
            if (base64Arr.Length == 2)
            {
                base64 = base64Arr[1];
            }
            else
            {
                base64 = getHexBuffer.Base64;
            }
            var buffer = Convert.FromBase64String(base64);
            IList<GetHexBufferResponse> list = new List<GetHexBufferResponse>();
            int currentIndex = 0;
            int size = 50000;
            while (buffer.Length > currentIndex)
            {
                GetHexBufferResponse getHexBufferResponse = new GetHexBufferResponse();
                getHexBufferResponse.Index = currentIndex;
                int currentSize = size;
                if (buffer.Length - currentIndex < size)
                {
                    currentSize = buffer.Length - currentIndex;
                } 
                getHexBufferResponse.HexStr = buffer.Skip(currentIndex).Take(currentSize).ToArray().HexBufferToStr();
                getHexBufferResponse.Length = currentSize;
                list.Add(getHexBufferResponse);
                currentIndex += currentSize;
            }
            getHexBufferListResponse.List = list;
            getHexBufferListResponse.TotalLength = buffer.Length;
            response.Data = getHexBufferListResponse;
            return response;
        }
        /// <summary>
        /// 获取16进制字符串(Form表单)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("api/Common/GetHexBufferForm")]
        public ActionResult<ResponseBase> GetHexBufferForm([FromForm]IFormFile file)
        {
            ResponseBase<GetHexBufferResponse> response = new ResponseBase<GetHexBufferResponse>();
            if (file == null)
            {
                response.Success = false;
                response.Message = "文件不能为空";
            }
            else
            {
                GetHexBufferResponse getHexBufferResponse = new GetHexBufferResponse();
                var sm = file.OpenReadStream();
                var buffer = sm.ToBuffer();
                getHexBufferResponse.HexStr = buffer.HexBufferToStr();
                getHexBufferResponse.Length = buffer.Length;
                response.Data = getHexBufferResponse;
            }
            return response;
        }

        /// <summary>
        /// 获取16进制字符串
        /// </summary>
        /// <param name="getHexBuffer"></param>
        /// <returns></returns>
        [HttpPost("api/Common/GetHexBuffer")]
        public ActionResult<ResponseBase> GetHexBuffer(GetHexBufferRequest getHexBuffer)
        {
            ResponseBase<GetHexBufferResponse> response = new ResponseBase<GetHexBufferResponse>();
            GetHexBufferResponse getHexBufferResponse = new GetHexBufferResponse();
            string base64 = null;
            var base64Arr = getHexBuffer.Base64.Split(',');
            if (base64Arr.Length == 2)
            {
                base64 = base64Arr[1];
            }
            else
            {
                base64 = getHexBuffer.Base64;
            }
            var buffer = Convert.FromBase64String(base64);
            getHexBufferResponse.HexStr = buffer.HexBufferToStr();
            getHexBufferResponse.Length = buffer.Length;
            response.Data = getHexBufferResponse;
            return response;
        }

        /// <summary>
        /// 阅读文章（先调用GetA8Key）
        /// </summary>
        /// <param name="readArticle"></param>
        /// <returns></returns>
        [HttpPost("api/Common/ReadArticle")]
        public async Task<ActionResult<ResponseBase>> ReadArticle(ReadArticleRequest readArticle)
        {
            ResponseBase<object, IList<string>> response = new ResponseBase<object, IList<string>>();

            var httpHeader = readArticle.HttpHeader.ToDictionary();
            var result = await HttpHelper.GetAsync(readArticle.FullUrl, httpHeader);

            #region 正则匹配
            Regex reg = new Regex("devicetype=(.+)");
            Match match = reg.Match(readArticle.FullUrl);
            string devicetype = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("version=(.+)");
            match = reg.Match(readArticle.FullUrl);
            string version = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("pass_ticket=(.+)");
            match = reg.Match(readArticle.FullUrl);
            string pass_ticket = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("lang=(.+)");
            match = reg.Match(readArticle.FullUrl);
            string lang = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("window.appmsg_token = \"(.+)\";");
            match = reg.Match(result);
            string appmsg_token = match.Groups[1].Value;

            reg = new Regex("var msg_title = \"(.+)\";");
            match = reg.Match(result);
            string msg_title = match.Groups[1].Value;


            reg = new Regex("var comment_id = \"(.+)\"");
            match = reg.Match(result);
            string comment_id = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));


            reg = new Regex("var biz = \"(.+)\"");
            match = reg.Match(result);
            string biz = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));


            reg = new Regex("var mid = \"(.+)\"");
            match = reg.Match(result);
            string mid = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));


            reg = new Regex("var sn = \"(.+)\"");
            match = reg.Match(result);
            string sn = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));

            reg = new Regex("var ct = \"(.+)\"");
            match = reg.Match(result);
            string ct = match.Groups[1].Value;

            reg = new Regex("var user_name = \"(.+)\"");
            match = reg.Match(result);
            string user_name = match.Groups[1].Value;

            //if (!string.IsNullOrEmpty(user_name))
            //{
            //    VerifyUser(wxId, MMPro.MM.VerifyUserOpCode.MM_VERIFYUSER_ADDCONTACT, "", "", user_name, 0);
            //}

            reg = new Regex("var appmsg_type = \"(.+)\"");
            match = reg.Match(result);
            string appmsg_type = match.Groups[1].Value;

            string appMsgUrl = $"https://mp.weixin.qq.com/mp/getappmsgext?f=json&mock=&fasttmplajax=1&f=json&wx_header=1&pass_ticket={pass_ticket}";
            var random = new Random().Next(100000000, 999999999);
            string postData = $"r=0.36105416{random}&__biz={biz}&appmsg_type={appmsg_type}&mid={mid}&sn={sn}&idx=1&scene=0&title={msg_title}&ct={ct}&abtest_cookie=&devicetype={devicetype}&version={version}&is_need_ticket=0&is_need_ad=0&comment_id={comment_id}&is_need_reward=0&both_ad=0&reward_uin_count=0&send_time=&msg_daily_idx=0&is_original=0&is_only_read=1&req_id=&pass_ticket={pass_ticket}&is_temp_url=0&item_show_type=0&tmp_version=1&more_read_type=0&appmsg_like_type=2";
            //appMsgUrl = $"{appMsgUrl}&{postData}";
            #endregion

            httpHeader.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 12_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148");// MicroMessenger/7.0.4(0x17000428) NetType/4G Language/zh_CN");
            var readResult = await HttpHelper.PostAsync<object>(appMsgUrl, postData, "utf-8", "application/x-www-form-urlencoded", httpHeader);

            response.Data = readResult;

            IList<string> list = new List<string>();
            list.Add(msg_title);
            list.Add(user_name);
            response.ExtensionData = list;



            return response;
        }


        /// <summary>
        /// 点赞文章（先调用GetA8Key）
        /// </summary>
        /// <param name="LikeArticle"></param>
        /// <returns></returns>
        [HttpPost("api/Common/LikeArticle")]
        public async Task<ActionResult<ResponseBase>> LikeArticle(LikeArticleRequest LikeArticle)
        {
            ResponseBase<object, IList<string>> response = new ResponseBase<object, IList<string>>();

            var httpHeader = LikeArticle.HttpHeader.ToDictionary();
            var result = await HttpHelper.GetAsync(LikeArticle.FullUrl, httpHeader);

            #region 正则匹配
            Regex reg = new Regex("devicetype=(.+)");
            Match match = reg.Match(LikeArticle.FullUrl);
            string devicetype = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("version=(.+)");
            match = reg.Match(LikeArticle.FullUrl);
            string version = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("pass_ticket=(.+)");
            match = reg.Match(LikeArticle.FullUrl);
            string pass_ticket = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("lang=(.+)");
            match = reg.Match(LikeArticle.FullUrl);
            string lang = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('&'));

            reg = new Regex("window.appmsg_token = \"(.+)\";");
            match = reg.Match(result);
            string appmsg_token = match.Groups[1].Value;

            reg = new Regex("var msg_title = \"(.+)\";");
            match = reg.Match(result);
            string msg_title = match.Groups[1].Value;


            reg = new Regex("var comment_id = \"(.+)\"");
            match = reg.Match(result);
            string comment_id = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));


            reg = new Regex("var biz = \"(.+)\"");
            match = reg.Match(result);
            string biz = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));


            reg = new Regex("var mid = \"(.+)\"");
            match = reg.Match(result);
            string mid = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));


            reg = new Regex("var sn = \"(.+)\"");
            match = reg.Match(result);
            string sn = match.Groups[1].Value.Substring(0, match.Groups[1].Value.IndexOf('\"'));

            reg = new Regex("var ct = \"(.+)\"");
            match = reg.Match(result);
            string ct = match.Groups[1].Value;

            reg = new Regex("var user_name = \"(.+)\"");
            match = reg.Match(result);
            string user_name = match.Groups[1].Value;

            string appmsgid = null;
            reg = new Regex("var appmsgid = '(.*)' \\|\\| '(.*)'\\|\\| \"(.*)\"");
            match = reg.Match(result);
            if (match.Groups.Count >= 2 && !string.IsNullOrEmpty(match.Groups[1].Value))
            {
                appmsgid = match.Groups[1].Value;
            }
            else if (match.Groups.Count >= 3 && !string.IsNullOrEmpty(match.Groups[2].Value))
            {
                appmsgid = match.Groups[2].Value;
            }
            else if (match.Groups.Count >= 4 && !string.IsNullOrEmpty(match.Groups[3].Value))
            {
                appmsgid = match.Groups[3].Value;
            }



            //if (!string.IsNullOrEmpty(user_name))
            //{
            //    VerifyUser(wxId, MMPro.MM.VerifyUserOpCode.MM_VERIFYUSER_ADDCONTACT, "", "", user_name, 0);
            //}

            reg = new Regex("var appmsg_type = \"(.+)\"");
            match = reg.Match(result);
            string appmsg_type = match.Groups[1].Value;


            #endregion

            string appMsgUrl = $"https://mp.weixin.qq.com/mp/appmsg_like?__biz={biz}&mid={mid}&idx=1&like=1&f=json&appmsgid={appmsgid}&itemidx=1&fasttmplajax=1&f=json&wx_header=1&pass_ticket={pass_ticket}";
            var random = new Random().Next(100000000, 999999999);
            string postData = $"is_temp_url=0&scene=90&subscene=93&appmsg_like_type=2&item_show_type=0&client_version=1700042d&comment=&prompted=1&style=2&action_type=1&passparam=&request_id={(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000}&device_type=iOS12.3.1";

            var readResult = await HttpHelper.PostAsync<object>(appMsgUrl, postData, "utf-8", "application/x-www-form-urlencoded", httpHeader);

            response.Data = readResult;

            IList<string> list = new List<string>();
            list.Add(msg_title);
            list.Add(user_name);
            response.ExtensionData = list;



            return response;
        }

        /// <summary>
        /// 授权登录（先调用GetMpA8Key）
        /// </summary>
        /// <param name="authorizationLogin"></param>
        /// <returns></returns>
        [HttpPost("api/Common/AuthorizationLogin")]
        public async Task<ActionResult<ResponseBase>> AuthorizationLogin(AuthorizationLogin authorizationLogin)
        {
            ResponseBase response = new ResponseBase();

            var htmlResult = await HttpHelper.GetAsync(authorizationLogin.FullUrl);
            string replyUrl = "https://open.weixin.qq.com/connect/confirm_reply";
            var nameCollection = authorizationLogin.FullUrl.ParseUrl();
            string postdata = HttpUtility.UrlDecode($"pass_ticket={nameCollection["pass_ticket"]}&key={nameCollection["key"]}&uin={nameCollection["uin"]}&uuid={nameCollection["uuid"]}&snsapi_login=on&allow=allow");

            var result = await HttpHelper.PostAsync(replyUrl, postdata, "utf-8", "application/x-www-form-urlencoded");
            if (!result.Contains("应用登录"))
            {
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "授权登录失败";
            } 
            return response;
        }

    }
}
