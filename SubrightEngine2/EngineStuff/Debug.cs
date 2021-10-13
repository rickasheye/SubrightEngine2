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
        
        public static void Log(string debugLog, LogType type)
        {
            string file = "[" + type + "] - " + debugLog;
            logFile.Insert(0, file);
            Console.WriteLine(file);
        }

        public static void Log(string debugLog)
        {
            Log(debugLog, LogType.MESSAGE);
        }

        public static void LogError(string debugLog)
        {
            Log(debugLog, LogType.ERROR);
        }

        public static void LogWarning(string debugLog)
        {
            Log(debugLog, LogType.WARNING);
        }
    }
}