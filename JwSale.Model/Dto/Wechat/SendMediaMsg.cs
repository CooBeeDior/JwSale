using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
 
    public class SendMediaMsgRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收账号
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 提交数据 XML格式的Appmsg
        /// <?xml version=\"1.0\"?>\n<appmsg appid='' sdkver=''><title>{标题}</title><des>{描述}</des><action></action><type>6</type><content></content><url></url><lowurl></lowurl><appattach><totallen>{文件大小}</totallen><attachid>{文件attactid}</attachid><fileext>{文件扩展名}</fileext></appattach><extinfo></extinfo></appmsg>
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
