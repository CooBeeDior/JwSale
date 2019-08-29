using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Common
{
    public class GetHexBufferRequest
    {
        public string Base64 { get; set; }
    }

    public class GetHexBufferResponse
    {
        public string HexStr { get; set; }

        public int Length { get; set; }
    }
}
