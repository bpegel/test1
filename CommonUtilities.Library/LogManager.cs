using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.Library
{
    public enum LogSeverity
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Own naming conventions.")]
        debug,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Own naming conventions.")]
        info,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Own naming conventions.")]
        warning,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Own naming conventions.")]
        error
    }

    public static class LogManager
    {
        private static class SingletonLog
        {
            static volatile ILog _log;
            static object _syncRoot = new object();
            static bool _isInitialized = false;

            public static ILog Instance
            {
                get
                {
                    if (!_isInitialized)
                    {
                        lock (_syncRoot)
                        {
                            if (!_isInitialized)
                            {
                                log4net.Config.XmlConfigurator.Configure();
                                _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                                _isInitialized = true;
                            }
                        }
                    }
                    return _log;
                }
            }
        }

        /// <summary>
        //     Log a message object with the Error level including the stack trace of the System.Exception passed as a parameter
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="ex">The exception to log, including its stack trace</param>
        public static void Log(object message, Exception ex)
        {
            SingletonLog.Instance.Error(message, ex);
        }
        /// <summary>
        /// Log a message object with the severity level
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="severity">Severity level of the message</param>
        public static void Log(string message, LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.debug:
                    SingletonLog.Instance.Debug(message);
                    break;
                case LogSeverity.error:
                    SingletonLog.Instance.Error(message);
                    break;
                case LogSeverity.info:
                    SingletonLog.Instance.Info(message);
                    break;
                case LogSeverity.warning:
                    SingletonLog.Instance.Warn(message);
                    break;
            }
        }
        /// <summary>
        /// Checks if this logger is enabled for the Debug level.
        /// </summary>
        public static bool IsDebugEnabled { get { return SingletonLog.Instance.IsDebugEnabled; } }

        /// <summary>
        /// Checks if this logger is enabled for the Error OR Fatal level.
        /// </summary>
        public static bool IsErrorEnabled { get { return SingletonLog.Instance.IsErrorEnabled || SingletonLog.Instance.IsFatalEnabled; } }

        /// <summary>
        ///  Checks if this logger is enabled for the Info level.
        /// </summary>
        public static bool IsInfoEnabled { get { return SingletonLog.Instance.IsInfoEnabled; } }

        /// <summary>
        ///  Checks if this logger is enabled for the Warn level.
        /// </summary>
        public static bool IsWarnEnabled { get { return SingletonLog.Instance.IsWarnEnabled; } }
    }
}
