using ProjectBoostLadder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace ProjectBoostLadder.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var quit = false;

            var line = string.Empty;

            while(!quit)
            {
                if (line == "Q" || line == "q")
                {
                    break;
                }

                System.Console.WriteLine("Press 'a' to insert a ladder entry or 'q' to quit.");

                line = System.Console.ReadLine();

                System.Console.Clear();

                if (line == "A" || line == "a")
                {
                    System.Console.WriteLine("Fill your name in and press ENTER.");
                    System.Console.WriteLine("Name: ");
                    var name = System.Console.ReadLine();
                    System.Console.WriteLine("Fill your time in and press ENTER.");
                    System.Console.WriteLine("Time in seconds: ");
                    float.TryParse(System.Console.ReadLine(), out float timeInSeconds);

                    var ladderService = new LadderClientApi(ConfigurationManager.AppSettings["LadderPostUrl"]);

                    var version = ConfigurationManager.AppSettings["LadderVersion"];

                    if (ladderService.TryPost(new Ladder.Entry { Name = name, TimeInSeconds = timeInSeconds }, version, out LadderClientApi.PostResponse response))
                    {
                        System.Console.WriteLine("The data was succesfully saved.");
                        System.Console.WriteLine($"You are position {response.Position} on the ladder.");
                        System.Console.WriteLine(response.Position);
                        System.Console.WriteLine(string.Empty);
                    }
                    else
                    {
                        System.Console.WriteLine("The data could not be saved.");
                        System.Console.WriteLine("Please try again latter.");
                    }
                }
            }
        }
    }
}
