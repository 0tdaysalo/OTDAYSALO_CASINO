using System;

namespace SLOT_1
{
    public class Printer
    {
        //вывод Slot.slot 
        public static void beaut_print()
        {
            Console.WriteLine("-----");
            for (int i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Const.length; j++)
                {
                    Console.Write(Slot.slot[i, j]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----");
        }

        //вывод слота с переданными символами
        public static void beaut_print(string symbol)
        {
            Console.WriteLine("-----");
            for (int i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Const.length; j++)
                {
                    Console.Write(symbol);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----");
        }

        //вывод переданного слота
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

        //вывод инфо о совершённом спине
        public static void info_about_spin(int win, char[] arr_of_lines, int balance, int bet)
        {
            beaut_print();
            if (win != 0)
            {
                Console.WriteLine($"поздравляем, вы выиграли монет {win}");
                Console.WriteLine($"баланс: {balance + win}, ставка: {bet}");
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
                Console.WriteLine($"сожалеем, ставка не сыграла, попробуйте ещё раз");
                Console.WriteLine($"баланс: {balance - bet}, ставка: {bet}");
            }
            Console.WriteLine($"Hashcode спина: {Hashcode.hash_code(Slot.slot)}");
        }


    }
}