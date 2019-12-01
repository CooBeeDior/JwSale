using Exceptionless;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JwSale.Packs.Packs
{
    [Pack("Exceptionless模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public class ExceptionlessPack : JwSalePack
    {
        protected override void UsePack(IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<JwSaleOptions>>();
            app.UseExceptionless(new ExceptionlessClient(c =>
            {
                c.ApiKey = options.Value.Exceptionless.ApiKey;
                if (!string.IsNullOrEmpty(options.Value.Exceptionless.ServerUrl))
                {
                    c.ServerUrl = options.Value.Exceptionless.ServerUrl;
                }

            }));

        }
    }
}
