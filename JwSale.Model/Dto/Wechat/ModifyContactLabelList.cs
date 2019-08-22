using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ModifyContactLabelList : WechatRequestBase
    {
        
        /// <summary>
        /// 获取获取标签包接口返回标签昵称跟ID的数据文本
        /// ["{\"userName\":\"\",\"labelIdlist\":\"6\"}"]
        /// </summary>
        public string list { get; set; }
    }
}
