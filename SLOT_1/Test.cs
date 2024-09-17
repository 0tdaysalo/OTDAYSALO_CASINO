using System;
using System.Diagnostics;


namespace SLOT_1_optimize_version
{
    public class Test
    {
        //функция которая подсчитывает количество миллисекунд, прошедших по истечению какого либо кода
        public static void check_time()
        {
            //подразумевается что будет использоваться для сравнения времени выполнения разных функций
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //
            /* сюда нужно передать какую то функцию или код */
            //

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
