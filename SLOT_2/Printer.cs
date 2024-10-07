using System;

namespace SLOT_2
{
    public static class Printer
    {
        //красивый вывод (ТОЛЬКО!) слота 
        public static void beaut_print(char[,] slot_print)
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
            if (check_scatter >= 3) Console.WriteLine($"поздравляем! {check_scatter} - S, вы выиграли {Const.arr_symb_dic_mlt_S[check_scatter]} бонусных вращений");

        }
    }
}
