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


    public class Label
    {
        public string labelName { get; set; }

        public string labelId { get; set; }

    }




}
