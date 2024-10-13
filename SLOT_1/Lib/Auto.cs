using System;

namespace SLOT_1
{
    public static class Auto
    {
        //авто-запуски слота при разных аргументах

        public static uint make_spin(uint balance, uint bet)
        {
            if (balance < bet)
            {
                Console.WriteLine("недостаточно средств! игра приостановлена");
                Console.WriteLine();
                return 0;
            }
            Slot.random_fill();
            uint win = Pay.pay_out(bet, Slot.get_win_set());
            balance = Pay.give_win(bet, win, balance);

            Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
            Console.WriteLine();
            return balance;
        }

        public static uint make_spin(uint balance, uint bet, uint count_spins)
        {
            for (uint i = 0; i < count_spins; i++)
            {
                if (balance < bet)
                {
                    Console.WriteLine("недостаточно средств! игра приостановлена");
                    Console.WriteLine();
                    break;

                }
                Slot.random_fill();
                uint win = Pay.pay_out(bet, Slot.get_win_set());
                balance = Pay.give_win(bet, win, balance);

                //вывод инфо о слоте, ставке, победе, проигрыше, играющей линии
                Console.WriteLine($"произошёл {i + 1}-ый спин");
                Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
                Console.WriteLine();
            }

            return balance;
        }

        public static uint make_spin(uint balance, uint bet, uint count_spins, uint count_sessions)
        {
            //баланс задаётся на каждую сессию и суммируется
            uint result = 0;
            uint prev_balance = balance;
            for (uint i = 0; i < count_sessions; i++)
            {
                prev_balance = make_spin(prev_balance, bet, count_spins);
                result += prev_balance;
            }

            return result;
        }

        public static uint make_spin_game(uint balance, uint bet, uint number_spin)
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
                uint win = Pay.pay_out(bet, Slot.get_win_set());
                balance = Pay.give_win(bet, win, balance);

                Console.WriteLine($"произошёл {number_spin}-ый спин");
                Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
            }
            return balance;
        }
    }
}