using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{
    /// <summary>
    /// 二维码
    /// </summary>
    [Table("QrCodeInfo")]
    public class QrCodeInfo : Entity
    {
        /// <summary>
        /// 二维码内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 二维码路径
        /// </summary>
        public string Path { get; set; }


        public string Remark { get; set; }

        /// <summary>
        /// 状态 0未扫码  1扫码成功  -1扫码失败
        /// </summary>
        public int Status { get; set; }
    }
}
