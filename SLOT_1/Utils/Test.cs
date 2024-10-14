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
        public static float roi(uint balance, uint bet, uint count_spins)
        {
            //функция нужна для случая смены:
            // - комбинаций
            // - выплат символов
            // - длины слота
            //чтобы оценить выгодность внесённых изменений, потому что казино всегда должно быть в плюсе


            //учитывается баланс при определенной ставке (10 * n * count_spins) раз
            //теоретическая возврат в сумме средний за 10 раз 
            float teor_roi = 0;

            for (uint i = 0; i < 10; i++)
            {
                uint n = 1_000_000, sum = 0;
                for (uint j = 0; j < n; j++)
                {
                    sum += make_auto_spin_without_info(balance, bet, count_spins);
                }
                //теоретическая возврат в сумме
                float teor_ret = (float)sum / n;                                  //в процентах
                Console.WriteLine($"{teor_ret} теоретический возврат в этот раз {(teor_ret / balance) * 100}%");
                teor_roi += (teor_ret / balance) * 100;
            }
            return teor_roi / 10;
        }

        //кол-во спинов, пока balance >= bet 
        public static uint check_count_spins(uint balance, uint bet)
        {
            //полученный выигрыш учитывается
            uint count_spins = 0;
            while (balance >= bet)
            {
                count_spins++;

                Slot.random_fill();
                uint win = Pay.pay_out(bet, Slot.get_win_set());
                balance = Pay.give_win(bet, win, balance);
            }
            return count_spins;
        }

        //авто-запуск слотов для определенного баланса, без вывода инфо
        public static uint make_auto_spin_without_info(uint balance, uint bet, uint count_spins)
        {
            for (uint i = 0; i < count_spins; i++)
            {
                if (balance >= bet)
                {
                    Slot.random_fill();
                    uint win = Pay.pay_out(bet, Slot.get_win_set());
                    balance = Pay.give_win(bet, win, balance);
                }
                else
                {

                    Console.WriteLine("недостаточно средств! игра приостановлена");
                    Console.WriteLine();
                    break;
                }
            }

            return balance;
        }

        //попытка создания графика (пока что не трогать)
        public static void graph()
        {
            for (uint i = 1; i < 100; i++)
            {
                for (uint j = 1; j < 11; j++)
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