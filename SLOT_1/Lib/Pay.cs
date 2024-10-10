namespace SLOT_1
{
    public static class Pay
    {
        //подсчет выплат
        public static int pay_out(int bet, char[] arr_of_lines)
        {
            //используется словарь: ключ - значение

            int total_win = 0;
            for (int i = 0; i < arr_of_lines.Length; i++)
            {
                if (arr_of_lines[i] != 0)
                {
                    foreach (var win in Const.array_symbols_dic)
                    {
                        if (arr_of_lines[i] == win.Key)
                            total_win += bet * win.Value;
                    }
                }
            }

            return total_win;
        }

        //взаимодействие с балансом на основе выплаты за спин
        public static int give_win(int bet, int win, int balance)
        {
            balance -= bet;
            if (win != 0) balance += win;
            return balance;
        }

    }
}