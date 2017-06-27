using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenForo_Bot_AntiLeech.XenForoStuff;

namespace XenForo_Bot_AntiLeech.WebStuff
{
    internal class BotConfig
    {
        public static string Pseudo = "";
        public static string Password = "";
        public static string Token;
        public static int MaxPercentBlacklistWord = 70;

        public static List<string> BlackListWord = new List<string>()
        {
            "Merci",
            "merci",
            "partage",
            "Partage"
        };

        public static List<Leecher> Leechers = new List<Leecher>();
    }
}