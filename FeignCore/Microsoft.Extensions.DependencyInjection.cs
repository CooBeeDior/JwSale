using FeignCore.Apis;
using System.Linq;
using System.Reflection;
using WebApiClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FeignExtensions
    {
        public static void AddFeign(this IServiceCollection services)
        {
            //ContextInvoker  
            //IApiParameterAttribute HttpContentAttribute
            //IApiReturnAttribute ApiReturnAttribute
            //IApiActionAttribute ApiActionAttribute
            //IApiActionFilter IApiActionFilterAttribute
            var assembly = Assembly.GetAssembly(typeof(FeignExtensions));
            var types = assembly.GetTypes().Where(o => typeof(IHttpApi).IsAssignableFrom(o) && o.IsInterface ).ToList();
            foreach (var item in types)
            {
                services.AddHttpApi(item);

            } 

        }
    }
     
}
