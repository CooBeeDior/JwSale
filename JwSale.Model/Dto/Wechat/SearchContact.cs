using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SearchContactRequest : WechatRequestBase
    {
        /// <summary>
        /// 搜索帐号 :非好友搜索,通常通过该接口进行好友添加,不支持(公众号,wxid)支持(QQ,手机号,微信号)
        /// </summary>
        public string user { get; set; }

  
    }


    public class SearchContactResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse baseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SnsUserInfo snsUserInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string myBrandList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CustomizedInfo customizedInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int contactCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> contact { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string albumBgimgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bigHeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ResBuf resBuf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string antispamTicket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kfworkerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int matchType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string popupInfoMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int openImcontactCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string smallHeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumStyle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weiboFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserName userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NickName nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PyInitial pYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QuanPin quanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ImgBuf imgBuf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int personalCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int verifyFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string verifyInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weibo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string verifyContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weiboNickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> openImcontactList { get; set; }
    }
}
