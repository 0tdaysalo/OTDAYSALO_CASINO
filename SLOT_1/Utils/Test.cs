using System;
using System.Diagnostics;

namespace SLOT_1
{
    public class Test
    {
        //подсчет кол-ва мс, прошедших по истечению части кода, в аргументы надо передать функцию
        public static void check_time(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //вызов переданной функции
            action();

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

            ////можно использовать и таким образом
            //check_time(() =>{ какой то код });
        }

        //аналогичная check_time, но с возможностью передачи туда функции с возвращаемым значением
        public static T check_time<T>(Func<T> func)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            
            T result = func();

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

            return result; //результат выполнения (если он есть) 
        }

        //подсчет среднего возврата от ставки
        public static float roi(int balance, int bet, int count)
        {   
            //учитывается баланс при определенной ставке (10 * n * count) раз
            //теоретическая возврат в сумме средний за 10 раз 
            float teor_roi = 0;

            for (int i = 0; i < 10; i++)
            {
                int n = 1_000_000, sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += make_full_auto_spin_without_info(balance, bet, count);
                }
                //теоретическая возврат в сумме
                float teor_ret = (float)sum / n;                                  //в процентах
                Console.WriteLine($"{teor_ret} теоретический возврат в этот раз {(teor_ret / balance) * 100}%");
                teor_roi += (teor_ret / balance) * 100;
            }
            return teor_roi / 10;
        }

        //кол-во спинов, пока balance < bet или balance = 0
        public static int check_count_spins(int balance, int bet)
        {
            //полученный выигрыш учитывается
            int count = 0;
            while (balance > bet)
            {
                count++;
                Program.random_fill(Program.slot);
                int win = Program.pay_out(bet, Program.get_win_set(Program.slot));
                balance = Program.give_win(bet, win, balance);
            }
            return count;
        }

        //авто-запуск слотов для определенного баланса, без вывода инфо
        public static int make_full_auto_spin_without_info(int balance, int bet, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (balance < bet)
                {
                    Console.WriteLine("недостаточно средств! игра приостановлена");
                    Console.WriteLine();
                    break;
                }

                Program.random_fill(Program.slot);

                int win = Program.pay_out(bet, Program.get_win_set(Program.slot));

                balance = Program.give_win(bet, win, balance);
            }

            return balance;
        }

        //попытка создания графика (пока что не трогать)
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

        //тест на корректную обработку данных перед запуском программы
        public static void test()
        {
        }

    }
}