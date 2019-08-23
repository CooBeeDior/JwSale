using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ModifyContactLabelListRequest : WechatRequestBase
    {
        /// <summary>
        /// 设置的微信Id
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 获取获取标签包接口返回标签昵称跟ID的数据文本
        /// </summary>
        public string labelIdlist { get; set; }
    }

 
}
