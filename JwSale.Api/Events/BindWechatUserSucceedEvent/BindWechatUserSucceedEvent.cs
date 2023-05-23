using JwSale.Model;
using JwSale.Model.DbModel;
using MediatR;
using RabbitmqCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    /// <summary>
    /// 绑定微信小程序成功
    /// </summary>
    public class BindWechatUserSucceedEvent : Event, INotification
    {
        public BindWechatUserSucceedEvent()
        {

        }
        public BindWechatUserSucceedEvent(UserInfo userInfo, WechatUserInfo wechatUserInfo)
        {
            UserInfo = userInfo;
            WechatUserInfo = wechatUserInfo;
        }
        public UserInfo UserInfo { get; set; }

        public WechatUserInfo WechatUserInfo { get; set; }

    }
}
