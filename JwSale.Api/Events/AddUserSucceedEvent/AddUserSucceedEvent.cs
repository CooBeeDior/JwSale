using JwSale.Model;
using MediatR;
using RabbitmqCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    /// <summary>
    /// 测试事件
    /// </summary>
    public class AddUserSucceedEvent : Event, INotification
    {
        public AddUserSucceedEvent()
        {

        }
        public AddUserSucceedEvent(UserInfo userInfo)
        {
            UserInfo = userInfo;
        }
        public UserInfo UserInfo { get; set; }

    }





}
