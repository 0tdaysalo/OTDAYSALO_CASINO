using System;
using System.Linq;

namespace SLOT_1
{
    public static class Printer
    {
        //вывод Slot.slot 
        public static void slot_print()
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
        public static void slot_print(string symbol)
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
        public static void slot_print(char[,] slot)
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
        public static void info_about_spin(int win, char[] arr_of_lines, int balance_after_spin, int bet)
        {
            slot_print();
            if (win != 0)
            {
                Console.WriteLine($"поздравляем, вы выиграли монет {win}");
                Console.WriteLine($"баланс: {balance_after_spin}, ставка: {bet}");
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
                Console.WriteLine($"баланс: {balance_after_spin}, ставка: {bet}");
            }
            Console.WriteLine($"Hashcode спина: {Hashcode.code(Slot.slot)}");
        }

        public static void slot_rule()
        {
            Console.WriteLine("ПРАВИЛА SLOT_1:");

            Console.WriteLine("ставка может быть только целым числом");
            Console.WriteLine("при каждом совершенном спине с вашего баланса снимается сумма вашей ставки");
            Console.WriteLine("размер суммы выигрыша  зависит от размера ставки");
            Console.WriteLine("вы выигрываете увеличение ставки на определенный множитель");
            Console.WriteLine($"в игре {Const.count_lines} играющих линий, 3 горизонтальных и 2 по диагонали");
            Console.WriteLine("линия считается сыгравшей, если в ней имеется 3 одинаковых символа");
            foreach (var symbol in Const.array_symbols_dic)
            {
                Console.WriteLine($"символ {symbol.Key} увеличивает ставку в {symbol.Value} раз");
            }
            Console.WriteLine($"максимальный выигрыш составляет X{Const.array_symbols_dic.Values.Max() * Const.count_lines} от ставки");
            Console.WriteLine("теоретический процент возврата вашего баланса составляет 97,7%");

            Console.WriteLine();

            Console.WriteLine("чтобы играть жмите Enter");
            Console.WriteLine("если вы желаете внести депзоит, введите ДЕП после начала игры");
            Console.WriteLine("если вы желаете изменить ставку, введите БЕТ после начала игры");
            Console.WriteLine("если вы желаете завершить игру введите ВЫХОД после начала игры");
            Console.WriteLine("при завершении игры вам будет видна ваша статистика");

            Console.WriteLine();

            Console.WriteLine("НИКОГДА НЕ ИГРАЙТЕ В АЗАРТНЫЕ ИГРЫ, А ТЕМ БОЛЕЕ В КАЗИНО, ИНАЧЕ ВЫ ОСТАНЕТЕСЬ В МИНУСЕ");
            Console.WriteLine("\nнажмите на любую клавишу, чтобы выйти покинуть раздел правил");
            Console.ReadKey();
        }
    }
}