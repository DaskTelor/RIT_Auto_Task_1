using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal abstract class LoggerBase
    {
        public enum LogType
        {
            ERROR, INFO, WARN, DEBUG
        }
        protected static readonly string[] _logTypeStrings = new string[] { "ERROR", "INFO", "WARN", "DEBUG" };
        public abstract void Log(string str, LogType type);
    }
}
