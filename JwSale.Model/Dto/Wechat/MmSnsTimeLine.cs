using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class MmSnsTimeLineRequest : WechatRequestBase
    {
        /// <summary>
        /// 朋友圈id 	首次0，二次填返回包最后一组【必填整数】
        /// </summary>
        public string maxid { get; set; }
    }
}
