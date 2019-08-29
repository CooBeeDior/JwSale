using JwSale.Api.Filters;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Common;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    [NoPermissionRequired]
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
    }
}
