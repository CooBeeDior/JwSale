using JwSale.Packs.Pack;
using JwSale.Util.Attributes;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace JwSale.Packs.Packs
{

    [Pack("Common模块")]
    public class CommonPack : JwSalePack
    {
        protected override void UsePack(IApplicationBuilder app)
        {
            //var env = ServiceLocator.Instance.GetService<IHostingEnvironment>();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseDefaultFiles();
            app.UseStaticFiles();
 
            //app.UseHttpsRedirection();
        }
        
     
    }
}
