using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using Microsoft.AspNetCore.Builder;
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
