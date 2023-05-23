using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace JwSale.Api.Events
{
    public class TestEventHandler : INotificationHandler<TestEvent>
    {
        private readonly ILogger<TestEventHandler> _logger;
        public TestEventHandler(ILogger<TestEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(TestEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"执行TestEventHandler ：{notification.Message}");
            return Task.CompletedTask;
        }
    }

}
