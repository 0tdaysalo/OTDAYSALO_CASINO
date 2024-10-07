using System.Collections.Generic;

namespace SLOT_1
{
    public static class Const
    {
        //основная длина слота 3 сивмола(вертикально и горизонтально)
        public static readonly int length = 3;

        //5 играющих линий
        public static readonly int count_lines = 5;

        //11 символов слота последовательной значимости
        public static readonly char[] array_symbols = { '1', 'J', 'Q', 'K', 'A', '@', '#', '$', '%', '&', '0' };

        //подсчет выплат по символам
        public static readonly Dictionary<char, int> array_symbols_dic = new Dictionary<char, int>()
        {
            {'1', 1},
            {'J', 1},
            {'Q', 1},
            {'K', 1},
            {'A', 1},
            {'@', 1},
            {'#', 1},
            {'$', 1},
            {'%', 1},
            {'&', 1},
            {'0', 250},
        };
    }
}