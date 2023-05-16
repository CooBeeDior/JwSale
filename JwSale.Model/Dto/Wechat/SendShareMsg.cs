using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SendShareMsgRequest:WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 提交数据 XML格式的Appmsg
        /// <appmsg  sdkver=\"0\"><title>{标题}</title><des>{描述}</des><type>{ app类型 3：音乐  4：小app  5：大app}</type><showtype>0</showtype><soundtype>0</soundtype><contentattr>0</contentattr><url>{跳转url}</url><lowurl>{跳转url}</lowurl><dataurl>{数据url}</dataurl><lowdataurl>{数据url}</lowdataurl> <thumburl>{图片url}</thumburl></appmsg>
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
