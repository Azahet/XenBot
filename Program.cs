using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenForo_Bot_AntiLeech.LogStuff;
using XenForo_Bot_AntiLeech.WebStuff;
using XenForo_Bot_AntiLeech.XenForoStuff;

namespace XenForo_Bot_AntiLeech
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WindowWidth = 160;

            while (!WebFunction.Connect())
            {
                Logger.log("Tentative de connection...", LogLevel.Error);
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                }).GetAwaiter().GetResult();
            }
            Logger.log("Connecté !", LogLevel.Info);
            Logger.log("Recherche des threads Hot", LogLevel.Info);
            var th = WebFunction.GetThreadHot();
            Logger.log("Threads Hot trouvé", th, LogLevel.Debug);

            List<Leecher> Leecher = new List<Leecher>();
            Parallel.ForEach(th, ds =>
            {
                Leecher.AddRange(WebFunction.GetLeechers($"{ds.Id}"));
            });
            Logger.log("Leecher détecté", Leecher, LogLevel.Debug);

            Logger.log("Reported Leecher", WebFunction.AnalyseProfile(Leecher), LogLevel.Debug, true);
            Console.ReadLine();
        }
    }
}