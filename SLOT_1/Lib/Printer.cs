using System;
using System.Linq;

namespace SLOT_1
{
    public static class Printer
    {
        private static ConsoleColor symb_color(char symbol)
        {
            switch (symbol)
            {
                case '1':
                    return ConsoleColor.Yellow;
                case 'J':
                    return ConsoleColor.Cyan;
                case 'Q':
                    return ConsoleColor.Green;
                case 'K':
                    return ConsoleColor.Blue;
                case 'A':
                    return ConsoleColor.Magenta;
                case '@':
                    return ConsoleColor.Red;
                case '#':
                    return ConsoleColor.DarkCyan;
                case '$':
                    return ConsoleColor.White;
                case '%':
                    return ConsoleColor.DarkBlue;
                case '&':
                    return ConsoleColor.DarkMagenta;
                case '0':
                    return ConsoleColor.DarkMagenta;
                default:
                    return ConsoleColor.DarkYellow;
            }
        }

        //вывод Slot.slot 
        public static void slot_print()
        {
            Console.WriteLine("-----");
            for (uint i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (uint j = 0; j < Const.length; j++)
                {
                    Console.ForegroundColor = symb_color(Slot.slot[i, j]);
                    Console.Write(Slot.slot[i, j]);
                }
                Console.ResetColor();
                Console.WriteLine("|");
            }
            Console.WriteLine("-----");
        }

        //вывод слота с переданным символом
        public static void slot_print(char symbol)
        {
            Console.WriteLine("-----");
            for (uint i = 0; i < Const.length; i++)
            {


                Console.Write("|");
                for (uint j = 0; j < Const.length; j++)
                {
                    Console.ForegroundColor = symb_color(symbol);
                    Console.Write(symbol);
                }
                Console.ResetColor();
                Console.WriteLine("|");


            }
            Console.WriteLine("-----");
        }

        //вывод переданного слота
        public static void slot_print(char[,] slot)
        {
            Console.WriteLine("-----");
            for (uint i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (uint j = 0; j < Const.length; j++)
                {
                    Console.Write(slot[i, j]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----");
        }

        //вывод инфо о совершённом спине
        public static void info_about_spin(uint win, char[] arr_of_lines, uint balance_after_spin, uint bet)
        {
            slot_print();
            Console.WriteLine($"баланс: {balance_after_spin}, ставка: {bet}");
            if (win == 0)
            {
                Console.WriteLine("сожалеем, ставка не сыграла, попробуйте ещё раз");
            }
            else
            {
                Console.WriteLine($"поздравляем, вы выиграли монет {win}");
                for (uint i = 0; i < arr_of_lines.Length; i++)
                {
                    if (arr_of_lines[i] != 0)
                    {
                        Console.WriteLine($"сыграла  линия: {i + 1}");
                    }
                }
            }
            Console.WriteLine($"Hashcode спина: {Hashcode.code(Slot.slot)}");
        }

        public static void slot_start()
        {
            Console.WriteLine("0TDAYSALO_CASINO, SLOT_1");
            Printer.slot_print('0');
            Console.WriteLine("!перед началом обязательно ознакомьтесь с правилами!");
            Console.WriteLine("введите СТАРТ, чтобы запустить игру");
            Console.WriteLine("введите ПРАВИЛА, чтобы отобразить их");
            Console.WriteLine("введите ВЫХОД, чтобы покинуть игру");
            Console.WriteLine("введите ПРОСМОТР, чтобы отобразить сыгровку символов");
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
                Console.WriteLine($"символ {symbol.Key} увеличивает ставку в {symbol.Value} раз(а)");
            }
            Console.WriteLine($"максимальный выигрыш составляет X{Const.array_symbols_dic.Values.Max() * Const.count_lines} от ставки");
            Console.WriteLine("теоретический процент возврата вашего баланса составляет 97,7%");

            Console.WriteLine();

            Console.WriteLine("чтобы играть жмите Enter");
            Console.WriteLine("если вы желаете внести депзоит, введите ДЕП после начала игры");
            Console.WriteLine("если вы желаете изменить ставку, введите БЕТ после начала игры");
            Console.WriteLine("если вы желаете завершить игру введите ВЫХОД после начала игры");
            Console.WriteLine("если вы желаете выйти в начальное меню введите МЕНЮ после начала игры");
            Console.WriteLine("при завершении игры вам будет видна ваша статистика");

            Console.WriteLine();

            Console.WriteLine("НИКОГДА НЕ ИГРАЙТЕ В АЗАРТНЫЕ ИГРЫ, А ТЕМ БОЛЕЕ В КАЗИНО, ИНАЧЕ ВЫ ОСТАНЕТЕСЬ В МИНУСЕ");
            Console.WriteLine();
            Console.WriteLine("нажмите на любую клавишу, чтобы выйти покинуть раздел правил");
        }
    }
}