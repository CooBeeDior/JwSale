using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using JwSale.Util.Extensions;

namespace JwSale.Util.Http
{
    public class HttpHelper
    {
        public async Task<T> Post(string url, object data) where T : new()
        {
            var client = CreateHttpClient();
            var content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url, content);
            var result = await res.Content.ReadAsStringAsync();

        }





        private HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }
    }
}
