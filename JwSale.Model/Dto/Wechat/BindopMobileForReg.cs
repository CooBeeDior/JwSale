using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class BindopMobileForRegRequest:WechatRequestBase
    {
        /// <summary>
        /// 验证码 获取验证码时该参数空，直到手机获取到验证码之后再填
        /// </summary>
        public string verifycode { get; set; }
    }
}
