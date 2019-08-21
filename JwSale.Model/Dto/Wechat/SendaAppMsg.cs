using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SendaAppMsgRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 提交数据 XML格式的Appmsg
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
