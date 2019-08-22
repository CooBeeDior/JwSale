using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class CheckLoginQrCodeRequest : WechatRequestBase
    {
    }


    public class CheckLoginQrCodeResponse 
    {
        //public string uuid { get; set; }

        public int state { get; set; }

        public string wxid { get; set; }

        public string wxnewpass { get; set; }

        public string headImgUrl { get; set; }

        public int pushLoginUrlexpiredTime { get; set; }

        public string nickName { get; set; }

        public int EffectiveTime { get; set; }

        public string t10 { get; set; }

        public string device { get; set; }
    }

}
