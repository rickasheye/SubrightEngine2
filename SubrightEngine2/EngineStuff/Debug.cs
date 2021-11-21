using System;
using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff
{
    public enum LogType
    {
        ERROR,
        WARNING,
        MESSAGE
    }

    public class Debug
    {
        public static List<string> logFile = new List<string>();

        public static void Log(string debugLog, LogType type, bool debugOverride)
        {
            if (Program.debug || debugOverride)
            {
                string file = "[" + type + "] - " + debugLog;
                logFile.Insert(0, file);
                Console.WriteLine(file); 
            }
        }

        public static void Log(string debugLog, bool debugOverride)
        {
            Log(debugLog, LogType.MESSAGE, debugOverride);
        }

        public static void Log(string debugLog)
        {
            Log(debugLog, true);
        }

        public static void LogError(string debugLog, bool debugOverride)
        {
            Log(debugLog, LogType.ERROR, debugOverride);
        }

        public static void LogError(string debugLog)
        {
            LogError(debugLog, true);
        }

        public static void LogWarning(string debugLog, bool debugOverride)
        {
            Log(debugLog, LogType.WARNING, debugOverride);
        }

        public static void LogWarning(string debugLog)
        {
            LogWarning(debugLog, true);
        }
    }
}