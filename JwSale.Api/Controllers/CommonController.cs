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
        /// 获取16进制字符串
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("api/Common/GetHexBufferForm")]
        public ActionResult<ResponseBase> GetHexBuffer([FromForm]IFormFile file)
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
            var buffer = Convert.FromBase64String(getHexBuffer.Base64);
            getHexBufferResponse.HexStr = buffer.HexBufferToStr();
            getHexBufferResponse.Length = buffer.Length;
            response.Data = getHexBufferResponse;
            return response;
        }
    }
}
