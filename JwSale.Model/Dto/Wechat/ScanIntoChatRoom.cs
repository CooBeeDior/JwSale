using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ScanIntoChatRoomRequest : WechatRequestBase
    {
        /// <summary>
        /// 二维码转文字后的地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 固定空
        /// </summary>
        public string cookie { get; set; }
    }


    public class ScanIntoChatRoomResponse
    {
        public BaseResponse baseResponse { get; set; }

        public string fullUrl { get; set; }

        public string a8Key { get; set; }


        public string title { get; set; }

        public string content { get; set; }



        public string headImg { get; set; }



    }
}
