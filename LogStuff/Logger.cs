using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenForo_Bot_AntiLeech.XenForoStuff;

namespace XenForo_Bot_AntiLeech.LogStuff
{
    internal class Logger
    {
        public static void log(string message, LogLevel logCategory)
        {
            switch (logCategory.Value)
            {
                case "Debug":
                    {
                        Console.Write("[XENFORO BOT V3] ");
                        Console.Write($"[{DateTime.Now.ToString("T")}] ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("[Debug]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" {message}");
                        break;
                    }
                case "Info":
                    {
                        Console.Write("[XENFORO BOT V3] ");
                        Console.Write($"[{DateTime.Now.ToString("T")}] ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("[INFO]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" {message}");
                        break;
                    }
                case "Warning":
                    {
                        Console.Write("[XENFORO BOT V3] ");
                        Console.Write($"[{DateTime.Now.ToString("T")}] ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[WARNING]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" {message}");
                        break;
                    }
                case "Error":
                    {
                        Console.Write("[XENFORO BOT V3] ");
                        Console.Write($"[{DateTime.Now.ToString("T")}] ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[ERROR]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" {message}");
                        break;
                    }
            }
        }

        public static void log(string message, IList<Thread> ThreadList, LogLevel logCategory)
        {
            switch (logCategory.Value)
            {
                case "Debug":
                    {
                        Console.Write("[XENFORO BOT V3] ");
                        Console.Write($"[{DateTime.Now.ToString("T")}] ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("[Debug]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(
                            $" {message} => {Environment.NewLine}                            [0] Name : [\"{ThreadList[0].Name}\"] Id : [\"{ThreadList[0].Id}\"] ");
                        for (int i = 1; i < ThreadList.Count; i++)
                        {
                            Console.WriteLine($"                            [{i}] Name : [\"{ThreadList[i].Name}\"] Id : [\"{ThreadList[i].Id}\"]  ");
                        }

                        break;
                    }
            }
        }

        public static void log(string message, IList<Leecher> Leecher, LogLevel logCategory, bool OnlyShowReported = false)
        {
            switch (logCategory.Value)
            {
                case "Debug":
                    {
                        Console.Write("[XENFORO BOT V3] ");
                        Console.Write($"[{DateTime.Now.ToString("T")}] ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("[Debug]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" {message} =>");
                        for (int i = 0; i < Leecher.Count; i++)
                        {
                            if (Leecher[i].Reported)
                            {
                                Console.WriteLine($"                            [{i}] Pseudo : [\"{Leecher[i].Pseudo}\"] Id : [\"{Leecher[i].Id}\"] Msg : [\"{Leecher[i].Message}\"] PercentBlacklistword : [\"{Leecher[i].PercentBlacklistword}\"%]");
                            }
                            else
                            {
                                if (!OnlyShowReported)
                                    Console.WriteLine($"                            [{i}] Pseudo : [\"{Leecher[i].Pseudo}\"] Id : [\"{Leecher[i].Id}\"] Msg : [\"{Leecher[i].Message}\"]");
                            }
                        }

                        break;
                    }
            }
        }
    }
}