using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SendAppMsgRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 提交数据 XML格式的Appmsg
        /// <appmsg appid=\"appid\" sdkver=\"0\"><title>标题</title><des>描述</des><type> app类型 3：音乐  4：小app  5：大app</type><showtype>0</showtype><soundtype>0</soundtype><contentattr>0</contentattr><url>跳转Url</url><lowurl>{appMessage.Url}</lowurl><dataurl>数据Url</dataurl><lowdataurl>数据Url</lowdataurl> <thumburl>显示图片地址</thumburl></appmsg>
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
