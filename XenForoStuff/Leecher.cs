using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenForo_Bot_AntiLeech.XenForoStuff
{
    public class Leecher
    {
        public string Pseudo { get; set; }
        public string Id { get; set; }
        public int Message { get; set; }
        public int PercentBlacklistword { get; set; }
        public bool Reported { get; set; }
    }
}