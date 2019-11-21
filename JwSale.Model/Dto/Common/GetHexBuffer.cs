using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
    public class GetHexBufferRequest
    {
        /// <summary>
        /// Base64字符串
        /// </summary>
        public string Base64 { get; set; }
    }

    public class GetHexBufferResponse
    {
        /// <summary>
        /// 16进制字符串
        /// </summary>
        public string HexStr { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
    }
}
