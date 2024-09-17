using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SLOT_3
{
    public class UtilitsFunc
    {
        //функция чтобы смотреть сколько процентов в спине встречается scatter  в %
        public static void count_scatter_check()
        {
            int count = 30000;
            int scatter = 0;
            for (int z = 0; z < count; z++)
            {
                int check = 0;
                Program.random_fill(Program.slot);

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
                    Program.good_beaut_print(Program.slot);
                }
             
            }
           Console.WriteLine($"\nитого процентное количество бонусок {((double)scatter / count) * 100.0}%");
        }
    }
}
