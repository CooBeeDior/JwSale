using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JwSale.Util.Logs
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();
        private const string DefaultLog4NetFileName = "log4net.config";
        private readonly ILoggerRepository _loggerRepository;

        /// <summary>
        /// 初始化一个<see cref="Log4NetLoggerProvider"/>类型的新实例
        /// </summary>
        public Log4NetLoggerProvider() : this(DefaultLog4NetFileName)
        { }

        /// <summary>
        /// 初始化一个<see cref="Log4NetLoggerProvider"/>类型的新实例
        /// </summary>
        public Log4NetLoggerProvider(string log4NetConfigFile)
        {
            string file = log4NetConfigFile ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultLog4NetFileName);
            Assembly assembly = Assembly.GetEntryAssembly() ?? GetCallingAssemblyFromStartup();
            _loggerRepository = LogManager.CreateRepository(assembly, typeof(Hierarchy));
            XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo(file));



        }

        /// <summary>
        /// 创建一个 <see cref="T:Microsoft.Extensions.Logging.ILogger" /> 的新实例
        /// </summary>
        /// <param name="categoryName">记录器生成的消息的类别名称。</param>
        /// <returns>日志实例</returns>
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, key => new Log4NetLogger(_loggerRepository.Name, key));
        }

        private static Assembly GetCallingAssemblyFromStartup()
        {
            var stackTrace = new System.Diagnostics.StackTrace(2);
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);
                var type = frame.GetMethod()?.DeclaringType;

                if (string.Equals(type?.Name, "Startup", StringComparison.OrdinalIgnoreCase))
                {
                    return type?.Assembly;
                }
            }
            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            _loggers.Clear();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
