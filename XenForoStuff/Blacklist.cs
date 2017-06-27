using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenForo_Bot_AntiLeech.WebStuff;

namespace XenForo_Bot_AntiLeech.XenForoStuff
{
    public class Blacklist
    {
        public static bool CheckBlacklistWord(string text)
        {
            foreach (var BlackListWord in BotConfig.BlackListWord)
            {
                if (text.Contains(BlackListWord))
                    return true;
            }
            return false;
        }
    }
}