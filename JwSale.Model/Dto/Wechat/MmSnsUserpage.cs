using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class MmSnsUserpageRequest : WechatRequestBase
    {
        /// <summary>
        /// 被获取账号
        /// </summary>
        public string wxid { get; set; }

        /// <summary>
        /// 最大ID 	首次0，二次填返回包最后一组消息ID实现翻页
        /// </summary>
        public string maxid { get; set; } = "0";
    }
}
