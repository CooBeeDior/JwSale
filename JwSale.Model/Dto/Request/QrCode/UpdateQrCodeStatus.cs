using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.QrCode
{
    /// <summary>
    /// 修改二维码状态
    /// </summary>
    public class UpdateQrCodeStatus : RequestBase
    {
        /// <summary>
        /// 状态 0:未扫码  1:扫码成功  -1:扫码失败
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 二维码链接
        /// </summary>
        [Required]
        public string Url { get; set; }
    }
}
