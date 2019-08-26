using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{

    public class BaseResponse
    {
        public int ret { get; set; }
        public ErrMsg errMsg { get; set; }
    }
    public class ErrMsg
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class UserName
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class SnsUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int snsFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string snsBgimgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string snsBgobjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int snsFlagEx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int snsPrivacyRecent { get; set; }
    }

    public class CustomizedInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int brandFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string externalInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brandInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brandIconURL { get; set; }
    }

    public class LinkedinContactItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string linkedinName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linkedinMemberID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linkedinoptionalUrl { get; set; }
    }

    public class AdditionalContactList
    {
        /// <summary>
        /// 
        /// </summary>
        public LinkedinContactItem linkedinContactItem { get; set; }
    }

    public class NewChatroomData
    {
        /// <summary>
        /// 
        /// </summary>
        public int memberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ChatRoomMemberItem> chatRoomMember { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int infoMask { get; set; }
    }


 


    public class Phonenumlistinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> phoneNumList { get; set; }
    }

    public class NickName
    {
        /// <summary>
        /// 阿星2号
        /// </summary>
        public string str { get; set; }
    }

    public class PyInitial
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }
  
    public class QuanPin
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class ImgBuf
    {
        /// <summary>
        /// 
        /// </summary>
        public int iLen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buffer { get; set; }
    }

    public class Remark
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class RemarkPYInitial
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class RemarkQuanPin
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class DomainList_Item
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class ContactListItem
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
        public CustomizedInfo customizedInfo { get; set; }
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
        public int hasWeiXinHdHeadImg { get; set; }
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

    public class VerifyUserValidTicketListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string antispamticket { get; set; }
    }

    public class ChatRoomMemberItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string displayName { get; set; }
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
        public int chatroomMemberFlag { get; set; }
    }


    public class ResBuf
    {
        /// <summary>
        /// 
        /// </summary>
        public int iLen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buffer { get; set; }
    }

    public class Jsapipermission
    {
        /// <summary>
        /// 
        /// </summary>
        public int BitValue1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BitValue2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BitValue3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BitValue4 { get; set; }
    }

    public class GeneralControlBitSet
    {
        /// <summary>
        /// 
        /// </summary>
        public int BitValue1 { get; set; }
    }

    public class DeepLinkBitSet
    {
        /// <summary>
        /// 
        /// </summary>
        public int BitValue1 { get; set; }
    }

    public class JsapicontrolBytes
    {
        /// <summary>
        /// 
        /// </summary>
        public int iLen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buffer { get; set; }
    }

    public class Topic
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

    public class MemberName
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }


    public class MemberListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public MemberName memberName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int memberStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NickName nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PyInitial pyinitial { get; set; }
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
        public Remark remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RemarkPYInitial remarkPyinitial { get; set; }
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
        public string province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 往下走走
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
        public string country { get; set; }
    }

    public class ChatRoomName
    {
        /// <summary>
        /// 
        /// </summary>
        public string str { get; set; }
    }

}
