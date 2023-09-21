using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal abstract class LoggerBase
    {
        protected LoggerBase() { }
        public abstract void Log(string str);
    }
}
