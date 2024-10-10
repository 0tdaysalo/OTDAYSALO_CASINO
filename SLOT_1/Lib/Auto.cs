using System;

namespace SLOT_1
{
    public static class Auto
    {
        //авто-запуск слота при заданных: баланс, ставка, кол-во спинов, возвращает баланс
        public static int make_spin(int balance, int bet, int count_spins)
        {
            for (int i = 0; i < count_spins; i++)
            {
                if (balance < bet)
                {
                    Console.WriteLine("недостаточно средств! игра приостановлена");
                    Console.WriteLine();
                    break;

                }
                Slot.random_fill();


                int win = Pay.pay_out(bet, Slot.get_win_set());
                //вывод инфо о слоте, ставке, победе, проигрыше, играющей линии
                Console.WriteLine($"произошёл {i + 1}-ый спин");


                Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);

                balance = Pay.give_win(bet, win, balance);
                Console.WriteLine();
            }

            return balance;
        }

        //авто-запуск слота обернутый в несколько сессий, возвращает результат игр
        public static int make_spin(int balance, int bet, int count_spins, int count_sessions)
        {
            //баланс задаётся на каждую сессию и суммируется
            int result = 0;
            int prev_balance = balance;
            for (int i = 0; i < count_sessions; i++)
            {
                prev_balance = make_spin(prev_balance, bet, count_spins);
                result += prev_balance;
            }

            return result;
        }
    }
}