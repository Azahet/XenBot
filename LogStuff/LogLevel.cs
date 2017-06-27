using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenForo_Bot_AntiLeech.LogStuff

{
    internal class LogLevel
    {
        private LogLevel(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public static LogLevel Debug { get { return new LogLevel("Debug"); } }
        public static LogLevel Info { get { return new LogLevel("Info"); } }
        public static LogLevel Warning { get { return new LogLevel("Warning"); } }
        public static LogLevel Error { get { return new LogLevel("Error"); } }
    }
}