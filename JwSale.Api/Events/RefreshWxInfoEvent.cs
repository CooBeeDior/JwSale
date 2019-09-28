using JwSale.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    public class RefreshWxInfoEvent : INotification
    {
        public string Token { get; set; }
        public string WxId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Device { get; set; }
        public bool IsRefresh { get; set; }


        public UserInfo UserInfo { get; set; }

    }





}
