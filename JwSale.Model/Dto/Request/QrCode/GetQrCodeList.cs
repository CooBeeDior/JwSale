using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.QrCode
{
    /// <summary>
    /// 获取二维码列表
    /// </summary>
    public class GetQrCodeList : RequestPageBase
    {

        /// <summary>
        ///状态 null:所有，0:未扫码  1:扫码成功  -1:扫码失败
        /// </summary>
        public int? Status { get; set; }


    }
}
