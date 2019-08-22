using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ManualAuthRequest : WechatRequestBase
    {
    }



    public class ManualAuthResponse
    {
        public string wxid { get; set; }

        public string nickName { get; set; }

        public string bindUin { get; set; }

        public string bindMail { get; set; }

        public string bindMobile { get; set; }

        public string Alias { get; set; }

        public int status { get; set; }

        public int pluginFlag { get; set; }

        public int registerType { get; set; }

        public string deviceInfoXml { get; set; }

        public int safeDevice { get; set; }

        public string officialNamePinyin { get; set; }
        public string officialNameZh { get; set; }
        public int pushMailStatus { get; set; }
        public string fsUrl { get; set; }


    }



}
