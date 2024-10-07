﻿using System;


namespace SLOT_1
{
    public static class Program
    {
        public static readonly Random rand = new Random();

        //объявление singleton слота
        public static char[,] slot = new char[Const.length, Const.length];

        //рандомное заполнения слота
        public static char[,] random_fill(char[,] slot)
        {
            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    slot[i, j] = Const.array_symbols[rand.Next(0, Const.array_symbols.Length)];
                }
            }
            return slot;
        }

        //все играющие комбинации 
        public static char[] get_win_set(char[,] slot)
        {
            //в слоте 5 линий: 3 по строкам, 2 по диагоналям
            char[] arr_of_lines = new char[Const.count_lines];
            arr_of_lines[0] = ((slot[1, 0] == slot[1, 1]) && (slot[1, 1] == slot[1, 2])) ? slot[1, 0] : (char)0;

            arr_of_lines[1] = ((slot[0, 0] == slot[0, 1]) && (slot[0, 1] == slot[0, 2])) ? slot[0, 0] : (char)0;

            arr_of_lines[2] = ((slot[2, 0] == slot[2, 1]) && (slot[2, 1] == slot[2, 2])) ? slot[2, 0] : (char)0;

            arr_of_lines[3] = ((slot[0, 0] == slot[1, 1]) && (slot[1, 1] == slot[2, 2])) ? slot[0, 0] : (char)0;

            arr_of_lines[4] = ((slot[0, 2] == slot[1, 1]) && (slot[1, 1] == slot[2, 0])) ? slot[0, 2] : (char)0;

            return arr_of_lines;
        }

        //подсчет выплат
        public static int pay_out(int bet, char[] arr_of_lines)
        {
            //используется словарь: ключ - значение

            int total_win = 0;
            for (int i = 0; i < arr_of_lines.Length; i++)
            {
                if (arr_of_lines[i] != 0)
                {
                    foreach (var win in Const.array_symbols_dic)
                    {
                        if (arr_of_lines[i] == win.Key)
                            total_win += bet * win.Value;
                    }
                }
            }

            return total_win;
        }

        //взаимодействие с балансом на основе выплаты за спин
        public static int give_win(int bet, int win, int balance)
        {
            balance -= bet;
            if (win != 0) balance += win;
            return balance;
        }

        //авто-запуск слота при заданных: баланс, ставка, кол-во спинов, возвращает баланс
        public static int make_spin(int balance, int bet, int coint_spins)
        {
            for (int i = 0; i < coint_spins; i++)
            {
                if (balance < bet)
                {
                    Console.WriteLine("недостаточно средств! игра приостановлена");
                    Console.WriteLine();
                    break;

                }
                random_fill(slot);


                int win = pay_out(bet, get_win_set(slot));
                //вывод инфо о слоте, ставке, победе, проигрыше, играющей линии
                Console.WriteLine($"произошёл {i + 1}-ый спин");


                Printer.info_about_spin(win, get_win_set(slot), balance, slot, bet);

                balance = give_win(bet, win, balance);
                Console.WriteLine($"уникальный код спина: {Hashcode.hash_code(slot)}");
                Console.WriteLine();
            }

            return balance;
        }

        //авто-запуск слота обернутый в несколько сессий, возвращает результат игр
        public static int make_spin(int balance, int bet, int coint_spins, int count_session)
        {
            //баланс задаётся на каждую сессию и суммируется
            int result = 0;
            int prev_balance = balance;
            for (int i = 0; i < count_session; i++)
            {
                prev_balance = make_spin(prev_balance, bet, coint_spins);
                result += prev_balance;
            }
            return result;
        }

        public static void Main()
        {
            Game.game();

            Console.ReadKey();
        }
    }
}