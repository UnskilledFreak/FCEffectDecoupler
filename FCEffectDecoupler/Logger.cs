using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;

namespace FCEffectDecoupler
{
    internal static class Logger
    {
        internal static IPALogger log { private get; set; }

        internal static void Log(string message, LogLevel severity = LogLevel.Info) => log.Log(severity, message);
        
        internal static void Info(string message) => log.Log(LogLevel.Info, message);

        internal static void Debug(string message) => log.Log(LogLevel.Debug, message);
        
        internal static void Error(string message) => log.Log(LogLevel.Error, message);
    }
}