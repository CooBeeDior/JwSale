using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Repository.Repositorys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{

    [ApiController]
    public class CustomerInfoController
    {
        private IJwSaleRepository<CustomerInfo> jwSaleRepository = null;
        public CustomerInfoController(IJwSaleRepository<CustomerInfo> jwSaleRepository)
        {
            this.jwSaleRepository = jwSaleRepository;
        }

        /// <summary>
        /// 获取所有客户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/CustomerIn/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            ResponseBase<IQueryable<CustomerInfo>> result = new ResponseBase<IQueryable<CustomerInfo>>();
            result.Data = jwSaleRepository.All();
            return await result.ToHttpResponseAsync();
        }



    }
}
