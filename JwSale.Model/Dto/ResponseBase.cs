using JwSale.Model.Dto.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JwSale.Model.Dto
{ 
    public class ResponseBase
    {
        public bool Success { get; set; } = true;

        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        public string Message { get; set; } = "执行成功";

    }

    public class ResponseBase<T> : ResponseBase
    {
        public T Data { get; set; }
    }

    public class ResponseBase1<T1, T2> : ResponseBase<T1>
    {
        public T2 ExtensionData { get; set; }
    }

}
