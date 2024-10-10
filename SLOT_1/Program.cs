using System;
using System.Threading;

namespace SLOT_1
{
    public static class Program
    {
        public static readonly Random rand = new Random();

        public static void Main()
        {
            Game.game();

            Thread.Sleep(12000);

            //Console.ReadKey();
        }
    }
}