using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class UploadDeviceStepModelRequest : WechatBase
    {    /// <summary>
         /// 步数
         /// </summary>
        public string stepCount { get; set; }
    }
}
