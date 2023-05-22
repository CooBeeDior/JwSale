using FeignCore.Filters;
using System.Threading;
using WebApiClient;
using WebApiClient.Attributes;
namespace FeignCore.Apis
{
    [HttpHost("http://api.coobeedior.com")]
    [ServiceFilter("wechat")]
    [LogFilter]
    public interface IUserApi : IHttpApi
    {
        [HttpPost("/api/Login/GetQrCode")]
        [JsonReturn]
        ITask<object> GetQrCode(CancellationToken token = default);

    }
}
