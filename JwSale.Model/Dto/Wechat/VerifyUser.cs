using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class VerifyUserRequest : WechatRequestBase
    {
        /// <summary>
        /// 添加群友时,填wxid,非好友时填搜索返回的v1
        /// </summary>
        public string v1 { get; set; }
        /// <summary>
        /// V2
        /// </summary>
        public string v2 { get; set; }

        /// <summary>
        /// 验证内容
        /// </summary>
        public string verifyContent { get; set; }

        /// <summary>
        /// 2是添加好友,3是同意好友添加
        /// </summary>
        public string verifyUserOpCode { get; set; }

        /// <summary>
        /// 2=通过搜索邮箱，3=账号查找，5=通过朋友验证消息，
        /// 7=通过朋友验证消息(可回复)，12=通过QQ好友添加，
        /// 14=群聊添加，15=通过搜索手机号,16通过朋友验证消息，
        /// 17通过名片分享，22通过摇一摇打招呼方式，25通过漂流瓶，30通过二维码方式
        /// </summary>
        public string scene { get; set; }
    }
}
