using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.QrCode
{
    /// <summary>
    ///上传二维码
    /// </summary>
    public class UploadQrCode
    {
        [Required]
        public IList<string> QrCodes { get; set; }
    }
}
