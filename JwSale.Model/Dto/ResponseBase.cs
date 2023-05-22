using JwSale.Model.Dto.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JwSale.Model.Dto
{
    public class ResponseBase
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "执行成功";

    }



    public class PageResponseBase : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public long TotalCount { get; set; }

    }
    public class PageResponseBase<T> : PageResponseBase
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }



    public class ResponseBase<T> : ResponseBase
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    public class ResponseTokenBase<T> : ResponseBase<T>
    {
        public string Token { get; set; }
    }

    public class ResponsePage<T> : ResponseBase<T>
    {
        public ResponsePage() {
            Page = new Page();
        }
        public Page Page { get; set; }
    }


    public class Page
    {
        public long TotalCount { get; set; }

        public int TotalPage { get; set; }


    }
    public class ResponseBase<T1, T2> : ResponseBase<T1>
    {
        /// <summary>
        /// 扩展数据
        /// </summary>
        public T2 ExtensionData { get; set; }
    }
    public class ResponseTokenBase<T1, T2> : ResponseBase<T1, T2>
    {
        public string Token { get; set; }
    }

}

