# XenBot - A c# AntiLeech Bot

Project goals:
* Suggest an example with base classes for the interaction between c # and xenforo
* Stay true to the C# spirit, through idiomatic language usage
* Outstanding - New functions can easily be added

# Requirements

* .NET 4.5.2
* [RestSharp](http://restsharp.org/) simple REST and HTTP API Client for .NET
* [html-agility-pack](http://html-agility-pack.net/) An agile HTML parser that builds a read / write DOM and supports plain XPATH or XSLT 

# What features does it have?

For the moment, only a few, if you want a really strong ready-to-use Obfuscator have a look at NETGuard.io

* Connect
* Analyse thread with blacklist word
* Analyse recent posts for each profile with blacklist word
![allfunction](https://i.imgur.com/Pv28Fcs.png)

# How configure it ?
```c#
internal class BotConfig
    {
    //Xenforo credentials
        public static string Pseudo = "";       
        public static string Password = "";
        public static string Token;
        
    //Percent with Blacklist word to be reported
        public static int MaxPercentBlacklistWord = 70;

    //Blacklist Word
        public static List<string> BlackListWord = new List<string>()
        {
            "Merci",
            "merci",
            "partage",
            "Partage"
        };

        public static List<Leecher> Leechers = new List<Leecher>();
    }
```
License
=====
XenBot is licensed under the very permissive MIT license. Any submodules or dependencies may be under different licenses.
