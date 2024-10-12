using System;

namespace SLOT_1
{
    public static class Auto
    {
        //авто-запуски слота при разных аргументах

        public static int make_spin(int balance, int bet)
        {
            if (balance < bet)
            {
                Console.WriteLine("недостаточно средств! игра приостановлена");
                Console.WriteLine();
                return 0;
            }
            Slot.random_fill();
            int win = Pay.pay_out(bet, Slot.get_win_set());
            balance = Pay.give_win(bet, win, balance);
          
            Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
            Console.WriteLine();
            return balance;
        }

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
                balance = Pay.give_win(bet, win, balance);

                //вывод инфо о слоте, ставке, победе, проигрыше, играющей линии
                Console.WriteLine($"произошёл {i + 1}-ый спин");
                Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
                Console.WriteLine();
            }

            return balance;
        }

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

        public static int make_spin_game(int balance, int bet, int number_spin)
        {
            //number_spins это номер спина, а не количество спинов
            //нужно для Game.game()
            if (balance < bet)
            {
                Console.WriteLine("недостаточно средств! игра приостановлена");
                Console.WriteLine($"баланс: {balance}, ставка: {bet}");
                Console.WriteLine();
            }
            else
            {
                Slot.random_fill();
                int win = Pay.pay_out(bet, Slot.get_win_set());
                balance = Pay.give_win(bet, win, balance);

                Console.WriteLine($"произошёл {number_spin}-ый спин");
                Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
            }
            return balance;
        }
    }
}