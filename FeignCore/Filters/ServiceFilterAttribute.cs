using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;
namespace FeignCore.Filters
{
    public class ServiceFilterAttribute : ApiActionAttribute, IApiActionAttribute
    {
        public string Name { get; }
        public ServiceFilterAttribute(string name)
        {
            this.Name = name;
        } 
        public override Task BeforeRequestAsync(ApiActionContext context)
        {
    
            return Task.CompletedTask;
        }
    }
}
