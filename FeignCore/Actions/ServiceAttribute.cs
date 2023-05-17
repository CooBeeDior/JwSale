using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;
namespace FeignCore.Actions
{
    public class ServiceAttribute : ApiActionAttribute, IApiActionAttribute
    {
        public string Name { get; }
        public ServiceAttribute(string name)
        {
            this.Name = name;
        } 
        public override Task BeforeRequestAsync(ApiActionContext context)
        {
    
            return Task.CompletedTask;
        }
    }
}
