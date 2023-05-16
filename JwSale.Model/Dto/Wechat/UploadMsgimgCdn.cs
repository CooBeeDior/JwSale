using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadMsgImgCdnRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收人wxid
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 图片内容  图片XML
        /// <?xml version=\"1.0\"?>\n<msg>\n\t<img aeskey=\"\" encryver=\"0\" cdnthumbaeskey=\"\" cdnthumburl=\"\" cdnthumblength=\"\" cdnthumbheight=\"\" cdnthumbwidth=\"\" cdnmidheight=\"0\" cdnmidwidth=\"0\" cdnhdheight=\"0\" cdnhdwidth=\"0\" cdnmidimgurl=\"\" length=\"200958\" md5=\"\" hevc_mid_size=\"\" />\n</msg>\n
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
