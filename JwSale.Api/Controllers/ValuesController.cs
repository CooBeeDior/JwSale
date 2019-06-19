using JwSale.Model;
using JwSale.Repository.Context;
using JwSale.Repository.Filters;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Net;
using System.Threading;

namespace JwSale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public ValuesController(IJwSaleRepository<RoleInfo> jwSaleRepository, JwSaleDbContext context)
        {
            //IServiceCollection serviceCollection = null;
            //   var aaaa = serviceCollection.GetServices(typeof(IUnitOfWork<>));
            var sss = jwSaleRepository.Filter(o => o.Name != "").FirstOrDefault();
            sss.Name = "fafwafaffffffffffffff";
            jwSaleRepository.Update(sss);

            string url1 = string.Empty;
            string url2 = string.Empty;
            using (MiniProfiler.Current.Step("Get方法"))
            {
                using (MiniProfiler.Current.Step("准备数据"))
                {
                    using (MiniProfiler.Current.CustomTiming("SQL", "SELECT * FROM Config"))
                    {
                        // 模拟一个SQL查询
                        Thread.Sleep(500);

                        url1 = "https://www.baidu.com";
                        url2 = "https://www.sina.com.cn/";
                    }
                }


                using (MiniProfiler.Current.Step("使用从数据库中查询的数据，进行Http请求"))
                {
                    using (MiniProfiler.Current.CustomTiming("HTTP", "GET " + url1))
                    {
                        var client = new WebClient();
                        var reply = client.DownloadString(url1);
                    }

                    using (MiniProfiler.Current.CustomTiming("HTTP", "GET " + url2))
                    {
                        var client = new WebClient();
                        var reply = client.DownloadString(url2);
                    }
                }
            }





        }

        //public ValuesController(CC cc, IJwSaleRepository<RoleInfo> ddd, IUnitOfWork<JwSaleDbContext> adad)
        //{


        //      var ada645d = cc.GetName();

        //    var adada = ddd.All();



        //}

        [HttpGet]
        [TypeFilter(typeof(UnitOfWorkFilter))]
        public ActionResult<string> Get()
        {
            return "123";
            //return jwSaleDbContext.RoleInfos.ToList().ToJson();
        }

        /// <summary>
        /// 这是一个带参数的get请求
        /// </summary>
        /// <remarks>
        /// 例子:
        /// Get api/Values/1
        /// </remarks>
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns>   
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
