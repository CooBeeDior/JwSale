using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using JwSale.Util.Extensions;

namespace JwSale.Api.Events
{
   
    public class BindWechatUserSucceedEventHandler : INotificationHandler<BindWechatUserSucceedEvent>
    {
        private readonly ILogger<BindWechatUserSucceedEventHandler> _logger;
        public BindWechatUserSucceedEventHandler(ILogger<BindWechatUserSucceedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(BindWechatUserSucceedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"执行BindWechatUserSucceedEventHandler：{notification.UserInfo?.ToJson()}{notification.WechatUserInfo?.ToJson()}");
            return Task.CompletedTask;
        }
    }
}
