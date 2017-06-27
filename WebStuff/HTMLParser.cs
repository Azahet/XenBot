using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace XenForo_Bot_AntiLeech.WebStuff
{
    internal class HTMLParser
    {
        private HtmlDocument doc = new HtmlDocument();

        public HTMLParser(string html)
        {
            doc.LoadHtml(html);
        }

        public string FindAttributes(string Node, string Attribute)
        {
            return doc.DocumentNode
                .SelectNodes(Node)
                .First()
                .Attributes[Attribute].Value;
        }

        public HtmlNodeCollection FindNodes(string Node)
        {
            return doc.DocumentNode
                .SelectNodes(Node);
        }
    }
}