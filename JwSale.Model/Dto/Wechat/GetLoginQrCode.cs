using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class GetLoginQrCodeRequest:WechatRequestBase
    {

    }
    public class GetLoginQrCodeMsg
    {
        public string Image { get; set; }
    }

}
