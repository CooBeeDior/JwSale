using JwSale.Api.Const;
using JwSale.Api.Http;
using JwSale.Model.Dto.Wechat;
using System.Threading.Tasks;

namespace JwSale.Api.Util
{
    public class WechatHelper
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="wechatCreate"></param>
        /// <returns></returns>
        public static Task<WechatAnalysisResponse> WechatCreate(WechatCreateRequest wechatCreate)
        {
            string url = GetUrl(CGI_TYPE.CGI_WECHAT_CREATE);
            return HttpHelper.PostAsync<WechatAnalysisResponse>(url, wechatCreate);
        }





        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="getLoginQrCode"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> GetLoginQrCode(GetLoginQrCodeRequest getLoginQrCode)
        {
            string url = GetUrl(CGI_TYPE.CGI_GETLOGINQRCODE);
            return HttpHelper.PostAsync<WechatResponseBase>(url, getLoginQrCode);
        }


        /// <summary>
        /// 检查二维码登陆状态
        /// </summary>
        /// <param name="checkLoginQrCode"></param>
        /// <returns></returns>

        public static Task<WechatResponseBase> CheckLoginQrCode(CheckLoginQrCodeRequest checkLoginQrCode)
        {

            string url = GetUrl(CGI_TYPE.CGI_CHECKLOGINQRCODE);
            return HttpHelper.PostAsync<WechatResponseBase>(url, checkLoginQrCode);
        }


        /// <summary>
        /// 登陆账号
        /// </summary>
        /// <param name="manualAuth"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> CheckLoginQrCode(ManualAuthRequest manualAuth)
        {

            string url = GetUrl(CGI_TYPE.CGI_CHECKLOGINQRCODE);
            return HttpHelper.PostAsync<WechatResponseBase>(url, manualAuth);
        }




        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="heartBeat"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> HeartBeat(HeartBeatRequest heartBeat)
        {
            string url = GetUrl(CGI_TYPE.CGI_HEARTBEAT);
            return HttpHelper.PostAsync<WechatResponseBase>(url, heartBeat);
        }


        /// <summary>
        /// 二次登陆
        /// </summary>
        /// <param name="autoAuth"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> AutoAuth(AutoAuthRequest autoAuth)
        {
            string url = GetUrl(CGI_TYPE.CGI_AUTOAUTH);
            return HttpHelper.PostAsync<WechatResponseBase>(url, autoAuth);
        }


        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <param name="logOut"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> Logut(LogoutRequest logOut)
        {
            string url = GetUrl(CGI_TYPE.CGI_LOGOUT);
            return HttpHelper.PostAsync<WechatResponseBase>(url, logOut);
        }


        /// <summary>
        /// 设置微信号
        /// </summary>
        /// <param name="generalSet"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> GeneralSet(GeneralSetRequest generalSet)
        {
            string url = GetUrl(CGI_TYPE.CGI_GENERALSET);
            return HttpHelper.PostAsync<WechatResponseBase>(url, generalSet);
        }



        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="uploadhdHeadImg"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> UploadhdHeadImg(UploadhdHeadImgRequest uploadhdHeadImg)
        {
            string url = GetUrl(CGI_TYPE.CGI_UPLOADHDHEADIMG);
            return HttpHelper.PostAsync<WechatResponseBase>(url, uploadhdHeadImg);
        }


        /// <summary>
        /// oplog操作
        /// </summary>
        /// <param name="opLog"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> OpLog(OpLogRequest opLog)
        {
            string url = GetUrl(CGI_TYPE.CGI_OPLOG);
            return HttpHelper.PostAsync<WechatResponseBase>(url, opLog);
        }


        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="setFriendRemarks"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> SetFriendRemarks(SetFriendRemarksRequest setFriendRemarks)
        {
            string url = GetUrl(CGI_TYPE.CGI_SETFRIENDREMARKS);
            return HttpHelper.PostAsync<WechatResponseBase>(url, setFriendRemarks);
        }

        /// <summary>
        /// 同步消息
        /// </summary>
        /// <param name="newSync"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> NewSync(NewSyncRequest newSync)
        {
            string url = GetUrl(CGI_TYPE.CGI_NEWSYNC);
            return HttpHelper.PostAsync<WechatResponseBase>(url, newSync);
        }

        /// <summary>
        /// 同步通讯录密钥
        /// </summary>
        /// <param name="newSyncKey"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> NewSyncKey(NewSyncKeyRequest newSyncKey)
        {
            string url = GetUrl(CGI_TYPE.SET_NEWSYNCKEY);
            return HttpHelper.PostAsync<WechatResponseBase>(url, newSyncKey);
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="newSendMsg"></param>
        /// <returns></returns>
        public static Task<WechatResponseBase> NewSendMsg(NewSendMsgRequest newSendMsg)
        {

            string url = GetUrl(CGI_TYPE.CGI_NEWSENDMSG);
            return HttpHelper.PostAsync<WechatResponseBase>(url, newSendMsg);
        }







        public static string GetUrl(string cgiType)
        {

            return $"{VXAPI.URL}{cgiType}?{VXAPI.KEY}";
        }

    }
}
