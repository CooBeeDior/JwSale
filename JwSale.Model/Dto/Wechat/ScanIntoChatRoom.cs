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
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse baseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fullUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string a8Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int actionCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Jsapipermission jsapipermission { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GeneralControlBitSet generalControlBitSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shareUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int scopeCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> scopeList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string antispamTicket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ssid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DeepLinkBitSet deepLinkBitSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public JsapicontrolBytes jsapicontrolBytes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int httpHeaderCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> httpHeader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wording { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string headImg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cookie { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string menuWording { get; set; }
    }
}
