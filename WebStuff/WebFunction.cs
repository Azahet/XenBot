using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using XenForo_Bot_AntiLeech.LogStuff;
using XenForo_Bot_AntiLeech.XenForoStuff;

namespace XenForo_Bot_AntiLeech.WebStuff
{
    public class WebFunction : WebRequest
    {
        #region ConnectStuuf

        public static bool Connect()
        {
            Get("login");
            var HTMLPost = Post("login/login", new Dictionary<string, string> { { "_xfToken", "" }, { "cookie_check", "1" }, { "login", BotConfig.Pseudo }, { "Password", BotConfig.Password }, { "redirect", "https://instant-hack.io/" }, { "register", "0" } });

            HTMLParser ConnectParse = new HTMLParser(HTMLPost);
            var token = ConnectParse.FindAttributes(@"//input[@name='_xfToken']", "value");
            if (token == "")
            {
                Logger.log("Impossible de trouver le token", LogLevel.Error);
                return false;
            }

            BotConfig.Token = token;
            return true;
        }

        #endregion ConnectStuuf

        #region ThreadStuff

        private static IList<Thread> ThreadList = new List<Thread>();

        public static IList<Thread> GetThreadHot()
        {
            var HTMLThreadHot = Get("advstats/threads-hot");
            HTMLParser ThreadHotParse = new HTMLParser(HTMLThreadHot);
            Parallel.ForEach(ThreadHotParse.FindNodes(@"//a[@class='PreviewTooltip']"), Nodes =>
            {
                ThreadList.Add(new Thread()
                {
                    Name = Nodes.InnerText,
                    Id = int.Parse(Nodes.Attributes["href"].Value.Replace("/unread", "").Replace("/", "").Split('.')[1])
                });
            });
            return ThreadList;
        }

        #endregion ThreadStuff

        #region PostStuff

        public static IList<Leecher> GetLeechers(string threadId)
        {
            var l = new List<Leecher>();
            var CurrentThreadHTML = Get($"threads/{threadId}");
            var CurrentThreadParse = new HTMLParser(CurrentThreadHTML);

            Parallel.For(1, int.Parse(CurrentThreadParse.FindNodes("//nav/a[@class='']").First().InnerText), i =>
            {
                var PagedHTML = Get($"threads/{threadId}/page-{i}");
                var PagedParse = new HTMLParser(PagedHTML);
                foreach (var Nodes in PagedParse.FindNodes(@"//li[@class='message      ']"))
                {
                    var CurrentPostParse = new HTMLParser(Nodes.InnerHtml);
                    if (Blacklist.CheckBlacklistWord(CurrentPostParse.FindNodes(@"//article").First().InnerText))
                    {
                        try
                        {
                            l.Add(new Leecher()
                            {
                                Pseudo = CurrentPostParse.FindNodes(@"//a[@class='username']").First().InnerText,
                                Id = CurrentPostParse.FindAttributes(@"//a[@class='username']", "href")
                                        .Replace("/", "").Split('.')[1],
                                Message = int.Parse(CurrentPostParse
                                    .FindNodes(@"//dl[@class='pairsJustified xbMessages']").First().InnerText),
                                Reported = false
                            });
                        }
                        catch (Exception)
                        {
                            var Id = CurrentPostParse.FindAttributes(@"//a[@class='username']", "href").Contains("@")
                                ? CurrentPostParse.FindAttributes(@"//a[@class='username']", "href").Replace("/", "")
                                : CurrentPostParse.FindAttributes(@"//a[@class='username']", "href").Replace("/", "")
                                    .Split('.')[1];
                            Logger.log($"Impossible de récupérer l'utilisateur {Id}", LogLevel.Warning);
                        }
                    }
                }
            });
            return l;
        }

        #endregion PostStuff

        #region ProfilStuff

        public static IList<Leecher> AnalyseProfile(IList<Leecher> ListLeacher)
        {
            Parallel.ForEach(ListLeacher, lt =>
            {
                var CurrentRecentHTML = Get($"members/{lt.Id}/recent-content");
                var CurrentRecentParse = new HTMLParser(CurrentRecentHTML);
                var blacklistWord = 0;
                var totalrecentpost = 0;
                try
                {
                    foreach (var Nodes in CurrentRecentParse.FindNodes(@"//blockquote[@class='snippet']"))
                    {
                        if (Blacklist.CheckBlacklistWord(Nodes.InnerText))
                        {
                            blacklistWord++;
                        }
                        totalrecentpost++;
                    }
                    ListLeacher.First(d => d.Id == lt.Id).PercentBlacklistword = 100 * blacklistWord / totalrecentpost;
                    if ((100 * blacklistWord / totalrecentpost) > BotConfig.MaxPercentBlacklistWord)
                    {
                        ListLeacher.First(d => d.Id == lt.Id).Reported = true;
                    }
                }
                catch (Exception)
                {
                    Logger.log($"Impossible d'analyser le profile de {lt.Pseudo}({lt.Id})", LogLevel.Warning);
                }
            });
            return ListLeacher;
        }

        #endregion ProfilStuff
    }
}