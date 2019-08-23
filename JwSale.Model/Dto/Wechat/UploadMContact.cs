using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadMContactRequest : WechatRequestBase
    {
        /// <summary>
        /// 上传列表
        /// </summary>
        public IList<MobileInfo> list { get; set; }
    }

    public class MobileInfo
    {

        public string Mobile { get; set; }
    }
}
