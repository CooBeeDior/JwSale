using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadDeviceStepRequest : WechatRequestBase
    {

        /// <summary>
        /// 当日凌晨
        /// </summary>
        public string fromTime { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public string toTime { get; set; }

        /// <summary>
        /// 步数
        /// </summary>
        public string stepCount { get; set; }
    }
}
