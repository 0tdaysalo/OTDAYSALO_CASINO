using System;
using System.Collections.Generic;

namespace SLOT_1
{
    public static class Program
    {
        public static readonly Random rand = new Random();

        //11 символов слота, последовательная значимости
        public static readonly char[] array_symbols = { '1', 'J', 'Q', 'K', 'A', '@', '#', '$', '%', '&', '0' };

        //подсчет выплат по символам
        public static readonly Dictionary<char, int> array_symbols_dic = new Dictionary<char, int>()
        {
            {'1', 1},
            {'J', 1},
            {'Q', 1},
            {'K', 1},
            {'A', 1},
            {'@', 1},
            {'#', 1},
            {'$', 1},
            {'%', 1},
            {'&', 1},
            {'0', 250},
        };

        //объявление слота в виде матрицы равной длины
        public static char[,] slot = new char[Const.length, Const.length];


        //рандомное заполнения слота в виде матрицы char
        public static char[,] random_fill(char[,] slot)
        {
            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    slot[i, j] = array_symbols[rand.Next(0, array_symbols.Length)];
                }
            }
            return slot;
        }

        //красивый вывод (ТОЛЬКО!) слота 
        public static void beaut_print(char[,] slot)
        {
            Console.WriteLine("-----");
            for (int i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Const.length; j++)
                {
                    Console.Write(slot[i, j]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----");
        }

        //задаются все играющие комбинации 
        public static char[] get_win_set(char[,] slot)
        {

            //в этом слоте всего 5 линий, 3 по строкам и 2 по диагоналям
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
                    foreach (var win in array_symbols_dic)
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

        //вывод инфо о совершённом спине
        public static void info_about_spin(int win, char[] arr_of_lines, int balance, char[,] slot, int bet)
        {
            beaut_print(slot);
            if (win != 0)
            {
                Console.WriteLine($"поздравляем: вы выиграли монет {win}, ваш баланс {balance + win}, ставка: {bet}");
                for (int i = 0; i < arr_of_lines.Length; i++)
                {
                    if (arr_of_lines[i] != 0)
                    {
                        Console.WriteLine("сыграла  линия: {0}", i + 1);
                    }
                }
            }
            else
            {
                Console.WriteLine($"сожалеем, ваша ставка не сыграла, попробуйте ещё раз, ваш баланс: {balance - bet}, ставка: {bet}");
            }
        }

        //авто-запуск слота при заданных: баланс, ставка, кол-во спинов, возвращает баланс
        public static int make_full_auto_spin(int balance, int bet, int coint_spins)
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


                info_about_spin(win, get_win_set(slot), balance, slot, bet);

                balance = give_win(bet, win, balance);
                Console.WriteLine($"номер спина: {Hashcode.hash_code(slot)}");
                Console.WriteLine();
            }

            return balance;
        }

        //авто-запуск слота обернутый в несколько сессий, возвращает результат игр
        public static int make_full_auto_spin(int balance, int bet, int coint_spins, int count_session)
        {
            //баланс задается на 1 сессию
            int result = 0;
            int prev_balance = balance;
            for (int i = 0; i < count_session; i++)
            {
                prev_balance = make_full_auto_spin(prev_balance, bet, coint_spins);
                result += prev_balance;
            }
            return result;
        }

        public static void Main()
        {
            Game.game();

            Console.ReadLine();
        }
    }
}