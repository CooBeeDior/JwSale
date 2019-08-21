using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.Wechat
{
    public class GetQrCodeResponse
    {
        public string Token { get; set; }

        public string Base64 { get; set; }
    }
}
