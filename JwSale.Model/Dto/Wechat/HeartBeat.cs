using JwSale.Model.Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class HeartBeatRequest : WechatRequestBase
    {
    }



    public class HeartBeatResponse
    {
        public BaseResponse baseResponse { get; set; }

        public int nextTime { get; set; }

        public int selector { get; set; }

    }


}
