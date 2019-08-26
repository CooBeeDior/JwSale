﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class NewSyncRequest : WechatRequestBase
    {
        /// <summary>
        /// 3同步新消息，5同步通讯录，262151新消息和通讯录一起同步
        /// </summary>
        public string selector { get; set; }
    }
    public class NewSyncResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
    }


    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public UserName userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string albumBGImgID { get; set; }
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
        public string bigHeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string smallHeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string myBrandList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public CustomizedInfo customizedInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chatRoomData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string headImgMd5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encryptUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iDCardNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string realName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobileHash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobileFullHash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AdditionalContactList additionalContactList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int chatroomVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int chatroomMaxCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int chatroomType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NewChatroomData newChatroomData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int deleteFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cardImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string labelIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Phonenumlistinfo Phonenumlistinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Weidianinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ChatroomInfoVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DeletecontactScene { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ChatroomstatuStatus { get; set; }
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
        public string weiboNickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NickName nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PyInitial pyInitial { get; set; }
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
        public long bitMask { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int bitVal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int imgFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Remark remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RemarkPYInitial remarkPYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RemarkQuanPin remarkQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int contactType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int roomInfoCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RoomInfoList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Extflag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DomainList_Item> domainList_ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int addContactScene { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 微信团队官方帐号
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int personalCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int hasWeiXinHdHeadImg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int verifyFlag { get; set; }
        /// <summary>
        /// 深圳市腾讯计算机系统有限公司
        /// </summary>
        public string verifyInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int source { get; set; }
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
        public string @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chatRoomOwner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int chatRoomNotify { get; set; }
    }


}
