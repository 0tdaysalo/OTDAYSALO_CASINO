namespace SLOT_1
{
    public static class Slot
    {
        //слот
        public static char[,] slot = new char[Const.length, Const.length];

        //рандомное заполнения слота
        public static char[,] random_fill()
        {
            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    slot[i, j] = Const.array_symbols[Program.rand.Next(0, Const.array_symbols.Length)];
                }
            }
            return slot;
        }

        //все играющие комбинации 
        public static char[] get_win_set()
        {
            //в слоте 5 линий: 3 по строкам, 2 по диагоналям
            char[] arr_of_lines = new char[Const.count_lines];
            arr_of_lines[0] = ((slot[1, 0] == slot[1, 1]) && (slot[1, 1] == slot[1, 2])) ? slot[1, 0] : (char)0;

            arr_of_lines[1] = ((slot[0, 0] == slot[0, 1]) && (slot[0, 1] == slot[0, 2])) ? slot[0, 0] : (char)0;

            arr_of_lines[2] = ((slot[2, 0] == slot[2, 1]) && (slot[2, 1] == slot[2, 2])) ? slot[2, 0] : (char)0;

            arr_of_lines[3] = ((slot[0, 0] == slot[1, 1]) && (slot[1, 1] == slot[2, 2])) ? slot[0, 0] : (char)0;

            arr_of_lines[4] = ((slot[0, 2] == slot[1, 1]) && (slot[1, 1] == slot[2, 0])) ? slot[0, 2] : (char)0;

            return arr_of_lines;
        }
    }
}