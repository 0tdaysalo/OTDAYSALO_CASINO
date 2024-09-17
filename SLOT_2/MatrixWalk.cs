using System;
namespace SLOT_3
{
    public class MatrixWalk
    {
        public int position_i = 0;

        public int position_j = 0;

        public char symbol = ' ';

        public MatrixWalk(int i, int j, char s)
        {
            this.position_i = i;
            this.position_j = j;
            this.symbol = s;
        }

        public static void good_print()
        {
            for (int q=0; q<Program.list_of_games_symbols.Count; q++)
            {
                    Console.WriteLine($"элемент {q}," +
                        $" поля:{Program.list_of_games_symbols[q].position_i}," +
                        $"{Program.list_of_games_symbols[q].position_j}," +
                        $"{Program.list_of_games_symbols[q].symbol} ");
            }
        }
    }
}