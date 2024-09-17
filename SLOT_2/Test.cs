using System;


namespace SLOT_3
{
    public class Test
    {
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
                Program.good_beaut_print(slot_test);
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
                Program.good_beaut_print(slot_test);
            }

        }
        
        public static void is_ready()
        {
            Console.WriteLine("хихи-хаха, поfixено))");
        }
    }
}
