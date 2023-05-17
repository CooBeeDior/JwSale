using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class WxLoginResponse
    {
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// UnionId
        /// </summary>
        public string UnionId { get; set; }



    }
}
