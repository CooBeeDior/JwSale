using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Cache
{
    public class WxLoginCache
    {
        public string OpenId { get; set; }

        public string UnionId { get; set; }

        public string SessionKey { get; set; }


        public string PhoneNumber { get; set; }
    }
}
