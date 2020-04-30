using System;
using System.Threading.Tasks;

namespace Dungeon_Master
{
    class Program
    {
        static string ServerUrl = "http://codebattle2020final.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/ktz0dtyuzevsivigau9v?code=3085157858590332350";
        static void Main(string[] args)
        {
            Console.Title = "GachiMuchi Club";
            Console.WriteLine("Hello Leathermans");

            var bot = new BomberBot(ServerUrl);
            Task.Run(() => bot.Play());

            Console.WriteLine("Dungeon master enter the next door");

            Console.ReadKey();
            Console.WriteLine("Dungeon master leave");

            bot.InitiateExit();
        }
    }
}
