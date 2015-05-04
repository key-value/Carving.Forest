using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Carving.Infrastructrue
{
    public static class ConfigureUtility
    {
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        static ConfigureUtility()
        {
            bool.TryParse(ConfigurationManager.AppSettings["ApplicationType"], out ApplicationType);
        }

        public static bool ApplicationType = false;
    }
}
