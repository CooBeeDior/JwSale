using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class NewSendMsgRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 消息内容
        /// 文本：直接填内容
        /// 名片：<?xml version=\"1.0\"?>\n<msg bigheadimgurl=\"\" smallheadimgurl=\"\" username=\"{微信Id}\" nickname=\"{昵称}\" fullpy=\"\" shortpy=\"\" alias=\"{别名}\" imagestatus=\"0\" scene=\"17\" province=\"\" city=\"\" sign=\"\" sex=\"{性别}\" certflag=\"0\" certinfo=\"\" brandIconUrl=\"\" brandHomeUrl=\"\" brandSubscriptConfigUrl=\"\" brandFlags=\"0\" regionCode=\"CN\" />
        /// 位置：<?xml version=\"1.0\"?>\n<msg>\n\t<location x=\"{经度}\" y=\"{纬度}\" scale=\"16\" label=\"{显示名称}\" maptype=\"0\" poiname=\"[位置]{消息名称}\" poiid=\"\" />\n</msg>
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息类型 通常填1，消息类型：1文本，42名片xml，48位置xml
        /// </summary>
        public string message_type { get; set; }

        /// <summary>
        /// @某人 艾特对象：多号用,隔开 如：wxid_1,wxid_2 且 pSend_msg 必须包含@字符
        /// </summary>
        public string atuserlist { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }

  
    }
}
