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

        //функция которая подсчитывает средний возврат денег, учитывая баланс при определенной ставке (10 * n * count) раз
        public static float roi(int balance, int bet, int count)
        {
            //теоретическая возврат в сумме средний за 10 раз 
            float teor_roi = 0;

            for (int i = 0; i < 10; i++)
            {
                int n = 1_000_000, sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += Program.make_full_auto_spin_without_info(balance, bet, count);
                }
                //теоретическая возврат в сумме
                float teor_ret = (float)sum / n;                                  //в процентах
                Console.WriteLine($"{teor_ret} теоретический возврат в этот раз {(teor_ret / balance) * 100}%");
                teor_roi += (teor_ret / balance) * 100;
            }
            return teor_roi / 10;
        }

        //попытка нарисовать график (пока что не трогать)
        public static void graph()
        {
            for (int i = 1; i < 100; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Console.WriteLine($"это значение на координатах (y)... (x){j} {roi(j, 1, j)}");

                }
            }
        }

        //функция которая считает количество спинов, которое пользователь сделал до того момента пока баланс не стал меньше ставки, полученный выигрыш учитывается
        public static int check_count_spins(int balance, int bet)
        {
            int count = 0;
            while (balance > bet)
            {
                //Console.WriteLine(count);
                count++;
                Program.random_fill(Program.slot);
                int win = Program.new_pay_out(bet, Program.check_result_spin_char(Program.slot));
                balance = Program.give_money(bet, win, balance);
            }
            return count;
        }

        public static void test()
        {
        }

    }
}