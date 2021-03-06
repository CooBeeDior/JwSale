﻿using JwSale.Api.Util;
using JwSale.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JwSale.Api.Extensions
{
    public static class Extension
    {
        public static HttpResponseMessage ToHttpResponse(this string response, Dictionary<string, string> headers = null)
        {
            return HttpResponseMessageHelper.GetJsonOk(response, headers);
        }

        public static Task<HttpResponseMessage> ToHttpResponseAsync(this string response, Dictionary<string, string> headers = null)
        {
            return Task.FromResult(HttpResponseMessageHelper.GetJsonOk(response, headers));
        }
        public static HttpResponseMessage ToHttpResponse(this ResponseBase response, Dictionary<string, string> headers = null)
        {
            return HttpResponseMessageHelper.GetJsonOk(response, headers);
        }

        public static Task<HttpResponseMessage> ToHttpResponseAsync(this object response, Dictionary<string, string> headers = null)
        {
            return Task.FromResult(HttpResponseMessageHelper.GetJsonOk(response, headers));
        }

    }
}
