using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadVideoRequest:WechatRequestBase
    {
        /// <summary>
        /// 接受wxid
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 视频XML
        /// <?xml version=\"1.0\"?>\n<msg>\n\t<videomsg aeskey=\"\" cdnthumbaeskey=\"\" cdnvideourl=\"\" cdnthumburl=\"\" length=\"\" playlength=\"\" cdnthumblength=\"\" cdnthumbwidth=\"\" cdnthumbheight=\"\" fromusername=\"\" md5=\"\" newmd5=\"\" isad=\"0\" />\n</msg>
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 每次发消息自增1，发送成功会返回该值 
        /// </summary>
        public string clientmsgid { get; set; }

    }
}
