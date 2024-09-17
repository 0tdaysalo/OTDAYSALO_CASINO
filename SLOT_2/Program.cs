using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SLOT_3
{
    public class Const
    {
        public const int length = 7; //основная длина слота 7 сивмола(в высоту и длину), 49 возможных значений

        public const int min_count_symbols = 5; //сыгровка должна содержать минимум 5 символов

        public const int max_count_playzone = 12; //максимальное количество играющих зон, показано в файле "как проходить по клеткам.png"
    }
    public static class Program
    {

        public static char[,] slot = new char[Const.length, Const.length];

        public static List<MatrixWalk> list_of_games_symbols = new List<MatrixWalk>();

        public static List<List<int[,]>> array_of_matrix_slot_list = new List<List<int[,]>>();

        public static readonly Random rand = new Random(); //рандом задается как глобальная переменная чтобы рандом всегда был разный 

        public static readonly char[] array_symbols = { '0', '1', '2', '3', '4', '5', '6', 'S' }; //8 символов игрового автомата, представлены по последовательной значимости

        public static readonly Dictionary<char, double> array_symbols_dic = new Dictionary<char, double>()
        {
            {'0', 0.2},
            {'1', 0.25},
            {'2', 0.3},
            {'3', 0.4},
            {'4', 0.5},
            {'5', 0.75},
            {'6', 1},
            {'S', 100},
        }; //словарь для подсчета выплаты по символам при минимальной сыгровке


        //функция для того чтобы красиво выводить слот
        public static void good_beaut_print(char[,] slot_print)
        {
            Console.WriteLine("---------");
            for (int i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Const.length; j++)
                {
                    Console.Write(slot_print[j, i]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("---------");


            int check_scatter = 0;
            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    if (slot_print[i, j] == 'S')
                    {
                        check_scatter++;
                    }
                }
            }
            if (check_scatter >= 3) Console.WriteLine($"поздравляем! {check_scatter} - S, вы выиграли {DictionariesOfPlayingSymbols.array_symbols_dic_multiplucation_S[check_scatter]} бонусных вращений");

        }

        //функция для рандомного заполнения барабана в виде матрицы char
        public static char[,] random_fill(char[,] slot_fill)
        {
            //основной цикл заполнения
            for (int j = 0; j < Const.length; j++)
            {
                int scatter_check = 0;
                bool scatter_check_bool = true;

                for (int i = 0; i < Const.length; i++)
                {
                    int random = rand.Next(0, 700);
                    if ((690 < random) && (random <= 700) && scatter_check_bool)
                    {
                        scatter_check++;
                        scatter_check_bool = false;
                        slot_fill[j, i] = 'S';
                    }

                    if (scatter_check <= 1)
                    {
                        if ((0  <= random) && (random <=  70)) slot_fill[j, i] = '0';
                        if ((70  < random) && (random <= 150)) slot_fill[j, i] = '1';
                        if ((150 < random) && (random <= 250)) slot_fill[j, i] = '2';
                        if ((250 < random) && (random <= 350)) slot_fill[j, i] = '3';
                        if ((350 < random) && (random <= 450)) slot_fill[j, i] = '4';
                        if ((450 < random) && (random <= 595)) slot_fill[j, i] = '5';
                        if ((595 < random) && (random <= 690)) slot_fill[j, i] = '6';
                    }

                    else if ((690 < random) && (random <= 700) && scatter_check_bool)
                    {
                        scatter_check++;
                        scatter_check_bool = false;
                        slot_fill[j, i] = 'S';
                    }

                    ////штука чтобы посмотреть как поэтапно заполняется матрица
                    //good_beaut_print(slot_fill);
                    //Console.WriteLine($"заполнено: {slot_fill[j, i]}");
                }
            }

            return slot_fill;
        }

        //функция которая будет проходиться по областям играющих символов и все такое (как в sugar rush)
        public static bool going_symbols(char[,] slot_going)
        {
            //Dictionary<char, int> array_of_playzone = new Dictionary<char, int>();  //словарь символ - количество играющих символом

            bool check = false;

            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {

                    //Console.WriteLine($"работает находимся на символе {slot_going[i, j]}");
                    
                    if (slot_going[i, j] == 'S') break; 

                    if (i >= 1)
                    {
                        if (slot_going[i, j] == slot_going[i - 1, j])
                        {
                            //Console.WriteLine(" - играет лево");
                            check=true;
                            list_of_games_symbols.Add(new MatrixWalk(i, j, slot_going[i, j]));

                        }
                    }

                    if (j >= 1)
                    {
                        if (slot_going[i, j] == slot_going[i, j - 1])
                        {
                            //Console.WriteLine(" - играет верх");
                            check = true;
                            list_of_games_symbols.Add(new MatrixWalk(i, j, slot_going[i, j]));

                        }
                    }

                    if (i <= 5)
                    {
                        if (slot_going[i, j] == slot_going[i + 1, j])
                        {
                            //Console.WriteLine(" - играет право");
                            check = true;
                            list_of_games_symbols.Add(new MatrixWalk(i, j, slot_going[i, j]));

                        }
                    }

                    if (j <= 5)
                    {
                        if (slot_going[i, j] == slot_going[i, j + 1])
                        {
                            //Console.WriteLine(" - играет низ");
                            check = true;
                            list_of_games_symbols.Add(new MatrixWalk(i, j, slot_going[i, j]));

                        }
                    }

                    //Console.WriteLine();
                }
            }

            return check;
        }


        //public static char[,] stay_only_play()
        //{
        //    char[,] slot_only_play = new char[Const.length, Const.length];
        //    int count = 0;
        //    for (int i = 0; i < Const.length * 2; i++)
        //    {
        //        if (count == list_of_games_symbols.Count) { break; }
        //        slot_only_play[list_of_games_symbols[count].position_i, list_of_games_symbols[count].position_j] = list_of_games_symbols[count].symbol;
        //        count++;
        //    }
        //    //MatrixWalk.good_print();
        //    list_of_games_symbols.Clear();
        //    list_of_games_symbols.TrimExcess();

        //    return slot_only_play;
        //}

        //public static char[,] stay_not_play(char[,] slot_only_play, char[,] slot_now)
        //{
        //    char[,] slot_new = (char[,])slot_now.Clone();

        //    for (int j = 0; j < Const.length; j++)
        //    {
        //        for (int k = 0; k < Const.length; k++)
        //        {
        //            if (slot_only_play[j, k] != '\0')
        //            {
        //                slot_new[j, k] = ' ';
        //            }
        //        }
        //    }
        //    return slot_new;
        //}



        public static char[,] stay_not_play(char[,] slot_now)
        {
            char[,] slot_only_play = new char[Const.length, Const.length];
            int count = 0;

            for (int i = 0; i < Const.length * 2; i++)
            {
                if (count == list_of_games_symbols.Count) { break; }
                slot_only_play[list_of_games_symbols[count].position_i, list_of_games_symbols[count].position_j] = list_of_games_symbols[count].symbol;
                count++;
            }

            //MatrixWalk.good_print();
            count = 0;
            list_of_games_symbols.Clear();
            list_of_games_symbols.TrimExcess();

            char[,] slot_new = (char[,])slot_now.Clone();

            for (int j = 0; j < Const.length; j++)
            {
                for (int k = 0; k < Const.length; k++)
                {
                    if (slot_only_play[j, k] != '\0')
                    {
                        slot_new[j, k] = ' ';
                    }
                }
            }
            slot_only_play = null;

            return slot_new;
        }

        public static char[,] drop_symbols(char[,] slot_not_play)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    for (int k = 0; k < Const.length; k++)
                    {
                        if (j != Const.length-1)
                        {
                            //Console.WriteLine($"на элементе [{j},{k}]={slot_not_play[k, j]}" +
                            //$" а внизу, на [{j + 1},{k}]={slot_not_play[k, j + 1]}");

                            if (slot_not_play[k, j + 1] == '\0' || slot_not_play[k, j + 1] == ' ')
                            {
                                slot_not_play[k, j + 1] = slot_not_play[k, j];
                                slot_not_play[k, j] = ' ';
                            }
                        }

                    }
                }
            }
            return slot_not_play;
        }

        public static char[,] feel_after_drop(char[,] slot_not_play_droped)
        {   

            for (int j = 0; j < Const.length; j++)
            {
                for (int k = 0; k < Const.length; k++)
                {
                    if (slot_not_play_droped[k, j] == '\0' || slot_not_play_droped[k, j] == ' ')
                    {
                        bool checker = false;
                        for (int i = 0; i < Const.length; i++)
                        {
                            if (slot_not_play_droped[i, k] == 'S')
                            {
                                checker = true;
                                break;
                            }
                        }

                        int random = rand.Next(0, 700);
                        if (checker)
                        {
                            if ((0 <= random) && (random <= 70)) slot_not_play_droped[k, j] = '0';
                            if ((70 < random) && (random <= 150)) slot_not_play_droped[k, j] = '1';
                            if ((150 < random) && (random <= 250)) slot_not_play_droped[k, j] = '2';
                            if ((250 < random) && (random <= 350)) slot_not_play_droped[k, j] = '3';
                            if ((350 < random) && (random <= 450)) slot_not_play_droped[k, j] = '4';
                            if ((450 < random) && (random <= 595)) slot_not_play_droped[k, j] = '5';
                            if ((595 < random) && (random <= 700)) slot_not_play_droped[k, j] = '6';

                        }
                        else
                        {
                            if ((0 <= random) && (random <= 70)) slot_not_play_droped[k, j] = '0';
                            if ((70 < random) && (random <= 150)) slot_not_play_droped[k, j] = '1';
                            if ((150 < random) && (random <= 250)) slot_not_play_droped[k, j] = '2';
                            if ((250 < random) && (random <= 350)) slot_not_play_droped[k, j] = '3';
                            if ((350 < random) && (random <= 450)) slot_not_play_droped[k, j] = '4';
                            if ((450 < random) && (random <= 595)) slot_not_play_droped[k, j] = '5';
                            if ((595 < random) && (random <= 690)) slot_not_play_droped[k, j] = '6';
                            if ((690 < random) && (random <= 700)) slot_not_play_droped[k, j] = 'S';
                        }
                    }
                }
            }     

            return slot_not_play_droped;
        }

        //public static char[,] slot_after_one_spin(char[,] slot_main)
        //{
        //    random_fill(slot_main);
        //    bool check = going_symbols(slot_main);

        //    Console.WriteLine("просто слот:");
        //    good_beaut_print(slot_main);

        //    var slot_while = (char[,])slot_main.Clone();

        //    while (check)
        //    {
        //        Console.WriteLine("вывод только неиграющих:");
        //        var slot_not_play = stay_not_play(stay_only_play(), slot_while);
        //        good_beaut_print(slot_not_play);
        //        //Thread.Sleep(500);

        //        Console.WriteLine("символы упали]:");
        //        var slot_drop = drop_symbols(slot_not_play);
        //        good_beaut_print(slot_drop);
        //        //Thread.Sleep(500);

        //        Console.WriteLine("пустые ячейки заполнились после падения)))):");
        //        slot_while = feel_after_drop(slot_drop);
        //        good_beaut_print(slot_while);
        //        //Thread.Sleep(500);



        //        check = going_symbols(slot_while);
        //    }

        //    return slot_while;
        //}

        public static char[,] slot_after_one_spin(char[,] slot_main)
        {
            random_fill(slot_main);
            bool check = going_symbols(slot_main);

            Console.WriteLine("просто слот:");
            good_beaut_print(slot_main);

            var slot_while = (char[,])slot_main.Clone();

            while (check)
            {
                Console.WriteLine("символы сыграли");
                var slot_not_play = stay_not_play(slot_while);
                good_beaut_print(slot_not_play);
                //Thread.Sleep(500);

                Console.WriteLine("символы упали");
                var slot_drop = drop_symbols(slot_not_play);
                good_beaut_print(slot_drop);
                //Thread.Sleep(500);

                Console.WriteLine("слот заполнился");
                slot_while = feel_after_drop(slot_drop);
                good_beaut_print(slot_while);
                //Thread.Sleep(500);



                check = going_symbols(slot_while);
            }

            return slot_while;
        }

        public static void Main()
        {
            slot_after_one_spin(slot);

            Console.WriteLine("просто слот каким был изначально:");
            good_beaut_print(slot);

            Console.ReadLine();
        }
    }
}
