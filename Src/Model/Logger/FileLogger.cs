using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class FileLogger : LoggerBase
    {
        private readonly string _fileName;
        public FileLogger(string fileName = "logs.log") 
        {
            _fileName = fileName;
        }
        public override void Log(string str, LogType type)
        {
            File.AppendAllText( _fileName, "[" + DateTime.Now + "] " + _logTypeStrings[(int)type] + ": [" + str + "]" + Environment.NewLine);
        }
    }
}
