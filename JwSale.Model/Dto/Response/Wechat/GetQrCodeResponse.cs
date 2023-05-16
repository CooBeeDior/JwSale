using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.Wechat
{
    public class GetQrCodeResponse
    {   

        public string Base64 { get; set; }

        public string TempToken { get; set; }
    }
}
