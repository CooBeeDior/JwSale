using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizerCore
{ 
    public class MsSqlStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IServiceProvider _serviceProvider;
   
        public MsSqlStringLocalizerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            return new MsSqlStringLocalizer(resourceSource.FullName, _serviceProvider);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new MsSqlStringLocalizer($"{baseName}.{location}", _serviceProvider);
        }
    }
}
