using System;

namespace SLOT_2
{
    public static class Auto
    {
        public static char[,] slot_after_one_spin(char[,] slot_main)
        {
            Const.random_fill(slot_main);
            bool check = Program.going_symbols(slot_main);

            Console.WriteLine("просто слот:");
            Printer.beaut_print(slot_main);

            var slot_while = (char[,])slot_main.Clone();

            while (check)
            {
                Console.WriteLine("символы сыграли");
                var slot_not_play = Program.stay_not_play(slot_while);
                Printer.beaut_print(slot_not_play);
                //Thread.Sleep(500);

                Console.WriteLine("символы упали");
                var slot_drop = Program.drop_symbols(slot_not_play);
                Printer.beaut_print(slot_drop);
                //Thread.Sleep(500);

                Console.WriteLine("слот заполнился");
                slot_while = Program.feel_after_drop(slot_drop);
                Printer.beaut_print(slot_while);
                //Thread.Sleep(500);



                check = Program.going_symbols(slot_while);
            }

            return slot_while;
        }
    }
}
