using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class AddContactLabelRequest : WechatRequestBase
    {
        
        /// <summary>
        /// 标签列表 支持同时添加多个标签
        /// ["{\"labelName\":\"标签名称\",\"labelId\":\"2588\"}"]
        /// </summary>
        public IList<Label> list { get; set; }
    }

    /// <summary>
    /// 标签
    /// </summary>
    public class Label
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string labelName { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public string labelId { get; set; }

    }




}
