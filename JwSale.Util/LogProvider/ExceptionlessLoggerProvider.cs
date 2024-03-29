﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
namespace JwSale.Util.Logs
{
    public class ExceptionlessLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
        /// </summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        { 
            return new ExceptionlessLogger();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
