using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection.Emit;


namespace SLOT_1_optimize_version
{
    //класс где будут описаны констаты для того чтобы потом при малейших изменениях не менять весь код
    public static class Const
    {
        public const int length = 3; //основная длина слота 3 сивмола(в высоту и длину)

        public const int count_lines = 5; //слот содержит 5 играющих линий
    }

    public static class Program
    {

        public static readonly Random rand = new Random(); //рандом задается как глобальная переменная чтобы рандом всегда был разный 

        public static readonly char[] array_symbols = { '1', 'J', 'Q', 'K', 'A', '@', '#', '$', '%', '&', '0' }; //11 символов игрового автомата, представлены по последовательной значимости

        public static readonly int[] arrray_symbols_convert_to_num = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // значения соответствующие array_symbols по индексу, пока что не нужно

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
        }; //словарь для подсчета выплаты по символам

        public static char[,] slot = new char[Const.length, Const.length]; //объявление слота в виде матрицы 3х3, стандартного барабана


        //функция для рандомного заполнения барабана в виде матрицы char
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

        //функция для получения матрицы int перебирая матрицу char
        public static int[,] get_value_of_fill(char[,] slot)
        {
            int[,] slot_int = new int[Const.length, Const.length];

            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    slot_int[i, j] = array_symbols.ToList().IndexOf(slot[i, j]);
                    //switch (slot[i, j])
                    //{
                    //    case '1':
                    //        slot_int[i, j] = 0;

                    //        break;
                    //    case 'J':
                    //        slot_int[i, j] = 1;

                    //        break;
                    //    case 'Q':
                    //        slot_int[i, j] = 2;

                    //        break;
                    //    case 'K':
                    //        slot_int[i, j] = 3;

                    //        break;
                    //    case 'A':
                    //        slot_int[i, j] = 4;

                    //        break;
                    //    case '@':
                    //        slot_int[i, j] = 5;

                    //        break;
                    //    case '#':
                    //        slot_int[i, j] = 6;

                    //        break;
                    //    case '$':
                    //        slot_int[i, j] = 7;

                    //        break;
                    //    case '%':
                    //        slot_int[i, j] = 8;

                    //        break;
                    //    case '&':
                    //        slot_int[i, j] = 9;

                    //        break;
                    //    case '0':
                    //        slot_int[i, j] = 10;

                    //        break;

                    //}
                }
            }

            return slot_int;
        }

        //функция для того чтобы красиво выводить барабан 
        public static void good_beaut_print(char[,] slot)
        {
            //функция для того чтобы красиво выводить барабан 
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

        //функция в которой задаются все играющие комбинации 
        public static char[] check_result_spin_char(char[,] slot)
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
        
        //функция для подсчета выплаты используя конструкцию switch
        public static int pay_out(int bet, char[] arr_of_lines)
        {
            int total_win = 0;
            for (int i = 0; i < arr_of_lines.Length; i++)
            {
                if (arr_of_lines[i] != 0)
                {
                    switch (arr_of_lines[i])
                    {
                        case '1':
                            total_win += bet;
                            break;

                        case 'J':
                            total_win += bet * 1;
                            break;

                        case 'Q':
                            total_win += bet * 1;
                            break;

                        case 'K':
                            total_win += bet * 1;
                            break;

                        case 'A':
                            total_win += bet * 1;
                            break;

                        case '@':
                            total_win += bet * 1;
                            break;

                        case '#':
                            total_win += bet * 1;
                            break;

                        case '$':
                            total_win += bet * 1;
                            break;

                        case '%':
                            total_win += bet * 1;
                            break;

                        case '&':
                            total_win += bet * 1;
                            break;

                        case '0':
                            total_win += bet * 250;
                            break;
                    }
                }
            }

            return total_win;
        }

        //функция для подсчета выплаты используя словарь ключ-значение
        public static int new_pay_out(int bet, char[] arr_of_lines)
        {
            int total_win = 0;
            for (int i = 0; i < arr_of_lines.Length; i++)
            {
                if (arr_of_lines[i] != 0)
                {
                    foreach(var win in array_symbols_dic)
                    {
                        if (arr_of_lines[i] == win.Key)
                            total_win += bet * win.Value;
                    }
                }
            }

            return total_win;
        }

        //функция для взаимодействия с балансом на основе выплаты за спин
        public static int give_money(int bet, int win, int balance)
        {
            balance -= bet;
            if (win != 0) balance += win;           
            return balance;
        }

        //функция для вывода инфомации о совершённом спине в консоль
        public static void info_about_spin(int win, char[] arr_of_lines, int balance, char[,] slot, int bet)
        {
            good_beaut_print(slot);
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

        //функция для автоматического запуска слотов для определенного баланс, ставки, и количество спинов, возвращает баланс
        public static int make_full_auto_spin(int balance, int bet, int n)
        {            
            for (int i = 0; i < n; i++)
            {                
                if (balance < bet)
                {
                    Console.WriteLine("недостаточно средств! игра приостановлена");
                    Console.WriteLine();                   
                    break;
                    
                }              
                random_fill(slot);


                int win = pay_out(bet, check_result_spin_char(slot));
                //вывод в консоль информации о барабане, ставке, победе, проигрыше, играющей линии (в целом о спине)
                Console.WriteLine($"произошёл {i + 1}-ый спин");


                info_about_spin(win, check_result_spin_char(slot), balance, slot, bet);

                balance = give_money(bet, win, balance);
                Console.WriteLine($"номер спина: {Hashcode.hash_code(slot)}");
                Console.WriteLine();
            }

            return balance;
        }

        //та же функция, что и выше, но без вывода инфомации в консоль, возвращает баланс
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
                
                random_fill(slot);

                //int win = pay_out(bet, check_result_spin_char(slot));
                    ////можно использовать и то и то, пока что не знаю что быстрее, new_pay_out короче, запутаннее , а pay_out более понятнее, но говно-код
                int win = new_pay_out(bet, check_result_spin_char(slot)); 
                
                balance = give_money(bet, win, balance);
            }

            return balance;
        }

        //введите количество сессий, и изначальный баланс, ставку и количество спинов,  возвращает результат сессии
        public static int make_full_auto_spin(int balance, int bet, int n, int count_session)
        {
            int result = 0;
            int prev_balance = balance;
            for (int i = 0; i < count_session; i++)
            {
                prev_balance = make_full_auto_spin(prev_balance, bet, n);
                result += prev_balance;
            }
            return result;
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
                    sum += make_full_auto_spin_without_info(balance, bet, count);
                }
                //теоретическая возврат в сумме
                float teor_ret = (float)sum / n;                                  //в процентах
                Console.WriteLine($"{teor_ret} теоретический возврат в этот раз {(teor_ret / balance) * 100}%");
                teor_roi += (teor_ret / balance) * 100;
            }
            return teor_roi / 10;
        }

        //попытка нарисовать график
        public static void test()
        {
            for (int i = 1; i< 100; i++)
            {
                for (int j = 1; j< 11; j++)
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
                random_fill(slot);
                int win = new_pay_out(bet, check_result_spin_char(slot));
                balance = give_money(bet, win, balance);
            }
            return count;
        }
        
        public static void Main()
        {
            make_full_auto_spin(1000, 10, 5);

            //hash_uncode(hash_code(slot));

            //Game.game();

            Console.ReadLine();
        }
    }
}