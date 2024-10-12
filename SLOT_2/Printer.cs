using System;

namespace SLOT_2
{
    public static class Printer
    {
        private static ConsoleColor symb_color(char symbol)
        {
            switch (symbol)
            {
                case '0':
                    return ConsoleColor.Yellow;
                case '1':
                    return ConsoleColor.Cyan;
                case '2':
                    return ConsoleColor.Green;
                case '3':
                    return ConsoleColor.Blue;
                case '4':
                    return ConsoleColor.Magenta;
                case '5':
                    return ConsoleColor.Red;
                case '6':
                    return ConsoleColor.DarkYellow;
                case 'S':
                    return ConsoleColor.White; // например, Scatter будет белым
                default:
                    return ConsoleColor.Gray;  // на случай неизвестных символов
            }
        }

        //красивый вывод (ТОЛЬКО!) слота 
        public static void beaut_print(char[,] slot_print)
        {
            Console.WriteLine("---------");
            for (int i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Const.length; j++)
                {
                    Console.ForegroundColor = symb_color(slot_print[j, i]);
                    Console.Write(slot_print[j, i]);
                }
                Console.ResetColor();
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
            if (check_scatter >= 3)
            {
                Console.ResetColor();
                Console.WriteLine($"поздравляем! {check_scatter} - S, вы выиграли {Const.arr_symb_dic_mlt_S[check_scatter]} бонусных вращений");
            }      
        }   
    }
}
