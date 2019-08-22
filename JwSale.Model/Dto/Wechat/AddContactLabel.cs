using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class AddContactLabel : WechatRequestBase
    {
        
        /// <summary>
        /// 标签列表 支持同时添加多个标签
        /// ["{\"labelName\":\"标签名称\",\"labelId\":\"2588\"}"]
        /// </summary>
        public string list { get; set; }
    }
}
