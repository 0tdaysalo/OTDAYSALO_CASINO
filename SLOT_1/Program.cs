using System.Threading;

namespace SLOT_1
{
    static class Program
    {
        static void Main()
        {
            Game.game();

            Thread.Sleep(12000);
            //Console.ReadKey();
        }
    }
}