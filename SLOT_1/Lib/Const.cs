using System.Collections.Generic;

namespace SLOT_1
{
    public static class Const
    {
        //основная длина слота 3 сивмола(вертикально и горизонтально)
        public const int length = 3;

        //5 играющих линий
        public const int count_lines = 5;

        //11 символов слота последовательной значимости
        public static readonly char[] array_symbols = { '1', 'J', 'Q', 'K', 'A', '@', '#', '$', '%', '&', '0' };

        //выплаты по символам 
        public static readonly Dictionary<char, int> array_symbols_dic = new Dictionary<char, int>()
        {
            {array_symbols[0], 1},
            {array_symbols[1], 1},
            {array_symbols[2], 1},
            {array_symbols[3], 1},
            {array_symbols[4], 1},
            {array_symbols[5], 1},
            {array_symbols[6], 1},
            {array_symbols[7], 1},
            {array_symbols[8], 1},
            {array_symbols[9], 1},
            {array_symbols[10], 250},
        };
    }
}