using System;
using System.Collections.Generic;

namespace SLOT_2
{
    public static class Const
    {
        //основная длина слота 7 сивмола(в высоту и длину), 49 возможных значений
        public const int length = 7;

        //сыгровка должна содержать минимум 5 символов
        public const int min_count_symbols = 5;

        //максимальное количество играющих зон, показано в файле "как проходить по клеткам.png"
        public const int max_count_playzone = 12;

        //словарь для подсчета выплаты по символам при минимальной сыгровке
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
        };

        //8 символов игрового автомата, представлены по последовательной значимости
        public static readonly char[] array_symbols = { '0', '1', '2', '3', '4', '5', '6', 'S' };

        //класс для словарей для подсчета выплат по символам при разных сыгровках
        //у каждого словаря есть в конце названия символ который означает их сыгровку
        public static readonly Dictionary<int, int> arr_symb_dic_mlt_S = new Dictionary<int, int>()
        {
            //scatters
            {3, 10},
            {4, 12},
            {5, 15},
            {6, 20},
            {7, 30},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_6 = new Dictionary<int, double>()
        {
            //чупики
            {5, 1.00},
            {6, 1.50},
            {7, 1.75},
            {8, 2.00},
            {9, 2.50},
            {10, 5.00},
            {11, 7.50},
            {12, 15.00},
            {13, 35.00},
            {14, 70.00},
            {15, 150.00},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_5 = new Dictionary<int, double>()
        {
            //сердечки
            {5, 0.75},
            {6, 1.00},
            {7, 1.25},
            {8, 1.50},
            {9, 2.00},
            {10, 4.00},
            {11, 6.00},
            {12, 12.50},
            {13, 30.00},
            {14, 60.00},
            {15, 100.00},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_4 = new Dictionary<int, double>()
        {
            //баклажаны
            {5, 0.5},
            {6, 0.75},
            {7, 1.00},
            {8, 1.25},
            {9, 1.50},
            {10, 3.00},
            {11, 4.50},
            {12, 10.00},
            {13, 20.00},
            {14, 40.00},
            {15, 60.00},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_3 = new Dictionary<int, double>()
        {
            //звёзды
            {5, 0.4},
            {6, 0.5},
            {7, 0.75},
            {8, 1.00},
            {9, 1.25},
            {10, 2.0},
            {11, 3.0},
            {12, 5.0},
            {13, 10.0},
            {14, 20.0},
            {15, 40.0},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_2 = new Dictionary<int, double>()
        {
            //мишки красные
            {5, 0.3},
            {6, 0.4},
            {7, 0.5},
            {8, 0.75},
            {9, 1.00},
            {10, 1.50},
            {11, 2.50},
            {12, 3.50},
            {13, 8.00},
            {14, 15.00},
            {15, 30.00},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_1 = new Dictionary<int, double>()
        {
            //мишки фиол
            {5, 0.25},
            {6, 0.30},
            {7, 0.40},
            {8, 0.50},
            {9, 0.75},
            {10, 1.25},
            {11, 2.00},
            {12, 3.00},
            {13, 6.00},
            {14, 12.00},
            {15, 25.00},
        };

        public static readonly Dictionary<int, double> arr_symb_dic_mlt_0 = new Dictionary<int, double>()
        {
            //мишки желт
            {5, 0.20},
            {6, 0.25},
            {7, 0.30},
            {8, 0.40},
            {9, 0.50},
            {10, 1.00},
            {11, 1.50},
            {12, 2.50},
            {13, 5.00},
            {14, 10.00},
            {15, 20.00},
        };

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

        public static readonly Random rand = new Random();
    }
}