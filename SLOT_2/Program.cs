using System;
using System.Collections.Generic;

namespace SLOT_2
{
    public static class Program
    {
        public static readonly Random rand = new Random();

        //слот
        public static char[,] slot = new char[Const.length, Const.length];

        //эточе
        public static List<MatrixWalk> list_of_games_symbols = new List<MatrixWalk>();

        //рандомное заполнения слота
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
                        if ((0 <= random) && (random <= 70)) slot_fill[j, i] = '0';
                        if ((70 < random) && (random <= 150)) slot_fill[j, i] = '1';
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
                    //Printer.beaut_print(slot_fill);
                    //Console.WriteLine($"заполнено: {slot_fill[j, i]}");
                }
            }

            return slot_fill;
        }

        //проход по областям играющих символов
        public static bool going_symbols(char[,] slot_going)
        {
            //пример работы - слот sugar rush

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
                            check = true;
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
                        if (j != Const.length - 1)
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
     
        

        public static void Main()
        {
            Auto.slot_after_one_spin(slot);

            Console.WriteLine("просто слот каким был изначально:");
            Printer.beaut_print(slot);

            Console.ReadLine();
        }
    }
}
