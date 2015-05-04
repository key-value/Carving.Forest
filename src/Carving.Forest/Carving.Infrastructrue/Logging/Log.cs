using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LogReceiverService;

namespace Carving.Infrastructrue.Log
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    public static class Log
    {
        private static Logger logger = LogManager.GetLogger("Carving");
        /// <summary>
        /// 将给定的异常实例详细信息写入日志。
        /// </summary>
        /// <param name="ex">需要将详细信息写入日志的异常实例。</param>
        public static void SendError(Exception ex)
        {
            logger.Error(ex);
        }
        public static void SendError(string messages)
        {
            logger.Error(messages);
        }

        public static void SendInfo(string messages)
        {
            logger.Info(messages);
        }

        public static void SendDebug(string messages)
        {
            logger.Debug(messages);
        }
        public static void SendWarn(string messages)
        {
            logger.Warn(messages);
        }
        public static void SendTrace(string messages)
        {
            logger.Trace(messages);
        }
        public static void SendFatal(string messages)
        {
            logger.Fatal(messages);
        }


    }
}
