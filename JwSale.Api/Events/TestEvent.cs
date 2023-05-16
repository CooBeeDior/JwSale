using JwSale.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    /// <summary>
    /// 测试事件
    /// </summary>
    public class TestEvent : INotification
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
