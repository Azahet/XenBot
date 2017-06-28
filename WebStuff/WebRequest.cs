#region using

using System.Collections.Generic;
using System.Net;
using RestSharp;

#endregion using

namespace XenForo_Bot_AntiLeech.WebStuff
{
    public class WebRequest
    {
        private static readonly RestClient Client = new RestClient(BotConfig.ForumsUrl);
        private static readonly CookieContainer Cookie = new CookieContainer();

        public static string Post(string RelativeUrl, Dictionary<string, string> Parametres)
        {
            Client.CookieContainer = Cookie;
            Client.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";

            var PostRestRequest = new RestRequest(RelativeUrl, Method.POST);
            foreach (var CurrentParams in Parametres)
                PostRestRequest.AddParameter(CurrentParams.Key, CurrentParams.Value);
            var PostRestResponse = (RestResponse)Client.Execute(PostRestRequest);
            return PostRestResponse.Content;
        }

        public static string Get(string RelativeUrl)
        {
            Client.CookieContainer = Cookie;
            Client.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
            var GetRestRequest = new RestRequest(RelativeUrl, Method.GET);
            var GetRestResponse = (RestResponse)Client.Execute(GetRestRequest);
            return GetRestResponse.Content;
        }
    }
}
