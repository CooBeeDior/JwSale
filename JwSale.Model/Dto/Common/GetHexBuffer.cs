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

        /// <summary>
        /// 当前索引
        /// </summary>
        public int Index { get; set; }
    }

    public class GetHexBufferListResponse
    {
        public int TotalLength { get; set; }

        public IList<GetHexBufferResponse> List { get; set; }

    }
}
