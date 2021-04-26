using System;
using System.Collections.Generic;
using System.Web;
using log4net;

/*
The following levels are defined in order of increasing priority:

ALL
DEBUG
INFO
WARN
ERROR
FATAL
OFF

*/

namespace SUPER.BLL
{
    public class Log
    {
        //private static ILog log;

        public static ILog logger
        {
            get {
                if (HttpContext.Current.Application["LOG"] == null) InicializaLog();
                return (ILog)HttpContext.Current.Application["LOG"];
            }

        }

        public static void InicializaLog()
        {
            ILog log = LogManager.GetLogger("RBC2");
            log4net.Config.XmlConfigurator.Configure();

            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["LOG"] = log;
            HttpContext.Current.Application.UnLock();
        }
    }
}
