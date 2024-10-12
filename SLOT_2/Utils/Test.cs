using System;

namespace SLOT_2
{
    public class Test
    {
        //% scatter в спине встречается scatter
        public static void count_scatter_check()
        {
            int count = 30000;
            int scatter = 0;
            for (int z = 0; z < count; z++)
            {
                int check = 0;
                Const.random_fill(Program.slot);

                for (int i = 0; i < Const.length; i++)
                {
                    for (int j = 0; j < Const.length; j++)
                    {
                        if (Program.slot[i, j] == 'S')
                        {
                            check++;
                        }
                    }
                }
                if (check >= 3)
                {
                    scatter++;
                    Printer.beaut_print(Program.slot);
                }

            }
            Console.WriteLine($"\nитого процентное количество бонусок {((double)scatter / count) * 100.0}%");
        }

        public static void check_scatter_test(char[,] slot_test)
        {
            int count_test = 0;
            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    if (slot_test[i, j] == 'S')
                    {
                        count_test++;
                    }
                }
            }
            //<3 
            if (count_test >= 3)
            {
                Console.WriteLine(">=3,тести");
                Printer.beaut_print(slot_test);
            }

        }

        public static void check_null_location_test(char[,] slot_test)
        {
            int count_test = 0;
            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    if (slot_test[i, j] == ' ' || slot_test[i, j] == '\0')
                    {
                        count_test++;
                    }
                }
            }
            //<3 
            if (count_test >= 1)
            {
                Console.WriteLine("дырка,блять");
                Printer.beaut_print(slot_test);
            }

        }

        public static void is_ready()
        {
            Console.WriteLine("хихи-хаха, поfixено))");
        }
    }
}
