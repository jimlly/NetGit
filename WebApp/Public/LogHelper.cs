using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

public class LogHelper
{
    private static readonly ILog log = LogManager.GetLogger("FileDefault");

    public static void Debug(string debug)
    {
        log.Debug(debug);
    }

    public static void Error(string error)
    {
        log.Error(error);
    }

    public static void Info(string info)
    {
        log.Info(info);
    }

    public static void warn(string warn)
    {
        log.Warn(warn);
    }
}