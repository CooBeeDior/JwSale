using JwSale.Packs.Options;
using JwSale.Util.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Packs
{
    [Pack("JwSaleOptions模块")]
    public class JwSaleOptionsPack : OptionsBasePack<JwSaleOptions>
    {


    }
}
