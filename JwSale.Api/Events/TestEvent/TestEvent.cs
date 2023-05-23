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
    public class TestEvent : Event, INotification
    {
        public TestEvent()
        {

        }
        public TestEvent(string message)
        {
            Message = message;
        }
        public string Message { get; set; }

    }





}
