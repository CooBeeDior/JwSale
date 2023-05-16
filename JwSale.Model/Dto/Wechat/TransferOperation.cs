using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class TransferOperationRequest : WechatRequestBase
    {
         
        /// <summary>
        /// begintransfertime
        /// </summary>
        public string begintransfertime { get; set; }
        /// <summary>
        /// transferid
        /// </summary>
        public string transferid { get; set; }

        /// <summary>
        /// transcationid
        /// </summary>
        public string transcationid { get; set; }
    }
}
