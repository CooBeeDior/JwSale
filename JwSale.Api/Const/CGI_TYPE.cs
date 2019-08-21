using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Api.Const
{
    public static class CGI_TYPE
    {
        /// <summary>
        /// 创建微信实例
        /// </summary>
        public const string CGI_WECHAT_CREATE = "0";
        /// <summary>
        /// 修改微信实例
        /// </summary>
        public const string CGI_WECHAT_MODIFY = "1";
        /// <summary>
        /// 二维码登录
        /// </summary>
        public const string CGI_GETLOGINQRCODE = "502";
        /// <summary>
        /// 二维码检查
        /// </summary>
        public const string CGI_CHECKLOGINQRCODE = "5021";
        /// <summary>
        /// 短信登录
        /// </summary>
        public const string CGI_BINDOPMOBILEFORREG = "145";
        /// <summary>
        /// 账号登录
        /// </summary>
        public const string CGI_MANUALAUTH = "701";
        /// <summary>
        /// 二次登录
        /// </summary>
        public const string CGI_AUTOAUTH = "702";
        /// <summary>
        /// 退出登录
        /// </summary>
        public const string CGI_LOGOUT = "282";
        /// <summary>
        /// 心跳
        /// </summary>
        public const string CGI_HEARTBEAT = "518";
        /// <summary>
        /// 同步消息
        /// </summary>
        public const string CGI_NEWSYNC = "138";
        /// <summary>
        /// 重置同步消息标识
        /// </summary>
        public const string SET_NEWSYNCKEY = "1388";
        /// <summary>
        /// 发送消息文本
        /// </summary>
        public const string CGI_NEWSENDMSG = "522";
        /// <summary>
        /// 发送消息视频CDN
        /// </summary>
        public const string CGI_UPLOADVIDEO = "149";
        /// <summary>
        /// 发送消息图片
        /// </summary>
        public const string CGI_UPLOADMSGIMG = "110";
        /// <summary>
        /// 发送消息图片CDN
        /// </summary>
        public const string CGI_UPLOADMSGIMGCDN = "1101";
        /// <summary>
        /// 发送消息语音
        /// </summary>
        public const string CGI_UPLOADVOICE = "127";
        /// <summary>
        /// 发送消息应用
        /// </summary>
        public const string CGI_SENDAPPMSG = "222";
        /// <summary>
        /// 撤销消息
        /// </summary>
        public const string CGI_REVOKEMSG = "594";
        /// <summary>
        /// 获取高清图片
        /// </summary>
        public const string CGI_GETMSGIMG = "109";
        /// <summary>
        /// 获取外部设备登录确认获取
        /// </summary>
        public const string CGI_EXTDEVICE_LOGINCONFIRM_GET = "971";
        /// <summary>
        /// 获取外部设备登录确认同意
        /// </summary>
        public const string CGI_EXTDEVICE_LOGINCONFIRM_OK = "972";
        /// <summary>
        /// 获取A8key
        /// </summary>
        public const string CGI_A8KEY = "233";
        /// <summary>
        /// 获取MPA8key
        /// </summary>
        public const string CGI_MPA8KEY = "238";
        /// <summary>
        /// 获取收款码
        /// </summary>
        public const string CGI_F2FQRCODE = "1588";
        /// <summary>
        /// 获取固定收款码
        /// </summary>
        public const string CGI_TRANSFERSETF2FFEE = "1623";
        /// <summary>
        /// 获取公众号APPID
        /// </summary>
        public const string CGI_WXAATTRSYNC = "1151";
        /// <summary>
        /// 获取授权小程序
        /// </summary>
        public const string CGI_JSOPERATEWXDATA = "1133";
        /// <summary>
        /// 获取小程序登陆
        /// </summary>
        public const string CGI_JSLOGIN = "1029";
        /// <summary>
        /// 上传通讯录
        /// </summary>
        public const string CGI_UPLOADMCONTACT = "133";
        /// <summary>
        /// 下载通讯录
        /// </summary>
        public const string CGI_GETMFRIEND = "142";
        /// <summary>
        /// 验证原密码
        /// </summary>
        public const string CGI_NEWVERIFYPASSWD = "384";
        /// <summary>
        /// 修改新密码
        /// </summary>
        public const string CGI_NEWSETPASSWD = "383";
        /// <summary>
        /// 获取指定朋友圈
        /// </summary>
        public const string CGI_MMSNSUSERPAGE = "212";
        /// <summary>
        /// 获取朋友圈首页
        /// </summary>
        public const string CGI_MMSNSTIMELINE = "211";
        /// <summary>
        /// 朋友圈电点赞评论
        /// </summary>
        public const string CGI_MMSNSCOMMENT = "213";
        /// <summary>
        /// 朋友圈操作
        /// </summary>
        public const string CGI_MMSNSOBJECTOP = "218";
        /// <summary>
        /// 朋友圈图片上传
        /// </summary>
        public const string CGI_MMSNSUPLOAD = "207";
        /// <summary>
        /// 朋友圈发布
        /// </summary>
        public const string CGI_MMSNSPOST = "209";
        /// <summary>
        /// 搜索联系人
        /// </summary>
        public const string CGI_SEARCHCONTACT = "106";
        /// <summary>
        /// 获取联系人
        /// </summary>
        public const string CGI_GETCONTACT = "182";
        /// <summary>
        /// 添加/同意联系人
        /// </summary>
        public const string CGI_VERIFYUSER = "137";
        /// <summary>
        /// 删除联系人
        /// </summary>
        public const string CGI_DELCONTACT = "68104";
        /// <summary>
        /// 设置联系人备注
        /// </summary>
        public const string CGI_SETFRIENDREMARKS = "68102";
        /// <summary>
        /// 添加联系人标签
        /// </summary>
        public const string CGI_ADDCONTACTLABEL = "635";
        /// <summary>
        /// 删除联系人标签
        /// </summary>
        public const string CGI_DELCONTACTLABEL = "636";
        /// <summary>
        /// 修改联系人标签
        /// </summary>
        public const string CGI_MODIFYCONTACTLABELLIST = "638";
        /// <summary>
        /// 获取联系人标签
        /// </summary>
        public const string CGI_GETCONTACTLABELLIST = "639";
        /// <summary>
        /// 获取个人或群聊二维码
        /// </summary>
        public const string CGI_GETQRCODE = "168";
        /// <summary>
        /// 创建群聊
        /// </summary>
        public const string CGI_CREATECHATROOM = "119";
        /// <summary>
        /// 邀请40内加群聊
        /// </summary>
        public const string CGI_ADDCHATROOMMEMBER = "120";
        /// <summary>
        /// 邀请40外加群聊
        /// </summary>
        public const string CGI_INVITECHATROOMMEMBER = "610";
        /// <summary>
        /// 退出群聊
        /// </summary>
        public const string CGI_QUITCHATROOM = "68116";
        /// <summary>
        /// 设置群聊公告
        /// </summary>
        public const string CGI_SETCHATROOMANNOUNCEMENT = "1691";
        /// <summary>
        /// 转让群聊主人
        /// </summary>
        public const string CGI_TRANSFERCHATROOMOWNER = "990";
        /// <summary>
        /// 获取群员列表
        /// </summary> 
        public const string CGI_MEMBERDETAIL = "551";
        /// <summary>
        /// 删除群聊成员
        /// </summary>
        public const string CGI_DELCHATROOMMEMBER = "179";
        /// <summary>
        /// 上传设备步数
        /// </summary>
        public const string CGI_UPLOADDEVICESTEP = "1261";
        /// <summary>
        /// 设置微信号
        /// </summary>
        public const string CGI_GENERALSET = "177";
        /// <summary>
        /// 设置资料
        /// </summary>
        public const string CGI_OPLOG = "681";
        /// <summary>
        /// 设置设置头像
        /// </summary>
        public const string CGI_UPLOADHDHEADIMG = "157";
        /// <summary>
        /// 接收红包
        /// </summary>
        public const string CGI_RECEIVEWXHB = "1581";
        /// <summary>
        /// 打开红包
        /// </summary>
        public const string CGI_OPENWXHB = "1685";
        /// <summary>
        /// 接收转账
        /// </summary>
        public const string CGI_TRANSFEROPERATION = "1691";
        /// <summary>
        /// 附近人
        /// </summary>
        public const string CGI_LBSFIND = "148";
        /// <summary>
        /// 摇一摇提交
        /// </summary>
        public const string CGI_SHAKEREPORT = "161";
        /// <summary>
        /// 摇一摇获取
        /// </summary>
        public const string CGI_SHAKEREGET = "162";
        /// <summary>
        /// 添加紧急联系人
        /// </summary>
        public const string CGI_ADDTRUSTEDFRIENDS = "583";
        /// <summary>
        /// 获取紧急联系人
        /// </summary>
        public const string CGI_GETTRUSTEDFRIENDS = "869";
        /// <summary>
        /// 获取授权项
        /// </summary>
        public const string CGI_OAUTHAUTHORIZE = "1254";
        /// <summary>
        /// 获取真实授权
        /// </summary>
        public const string CGI_JSAPIPREVERIFY = "1093";
        /// <summary>
        /// 获取获取安全设备信息
        /// </summary>
        public const string CGI_TYPE_GETSAFETYINFO = "850";
        /// <summary>
        /// 获取删除安全设备
        /// </summary>
        public const string CGI_TYPE_DELSAFEDEVICE = "362";
    }
}
