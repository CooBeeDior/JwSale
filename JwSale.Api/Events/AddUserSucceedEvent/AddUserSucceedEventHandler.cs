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
    public class AddUserSucceedEventHandler : INotificationHandler<AddUserSucceedEvent>
    {
        private readonly ILogger<AddUserSucceedEventHandler> _logger;
        public AddUserSucceedEventHandler(ILogger<AddUserSucceedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(AddUserSucceedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"执行AddUserEventHandler ：{notification.UserInfo?.ToJson()}");
            return Task.CompletedTask;
        }
    }

}
