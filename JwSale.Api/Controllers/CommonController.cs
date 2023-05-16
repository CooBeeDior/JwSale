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
        /// 获测试
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("api/Test")]
        public ActionResult<ResponseBase> Test([FromForm] IFormFile file)
        {
            ResponseBase response = new ResponseBase();


            return response;
        }





    }
}
