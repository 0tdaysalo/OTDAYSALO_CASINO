using System;

namespace SLOT_1
{
    public class Hashcode
    {
        //хэширование данных о спине в виде 
        public static string hash_code(char[,] slot)
        {
            //можно будет воспроизвести спин
            string code = string.Empty;

            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    for (int k = 0; k < Const.array_symbols.Length; k++)
                    {
                        if (slot[i, j] == Const.array_symbols[k])
                        {
                            code += k.ToString("X");
                        }
                    }
                }
            }
            code = add_confus(code);
            Int64 time = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            code += "_" + time.ToString();

            return code;
        }

        //воспроизведение спина по хэш-коду
        public static void hash_uncode(string code)
        {
            string[] uncode_string = code.Split('_');
            char[,] slot_func = new char[Const.length, Const.length];

            //первая часть

            //переворачиваем строку
            string decode_str_rev = add_confus((uncode_string[0]));

            //перевернутая последовательнсть в виде массива(заполненная ниже)
            string[] decode_str_rev_arr = new string[decode_str_rev.Length];
            for (int i = 0; i < decode_str_rev.Length; i++)
            {
                decode_str_rev_arr[i] = decode_str_rev[i].ToString();
                if (decode_str_rev_arr[i] == "A")
                {
                    decode_str_rev_arr[i] = 10.ToString();
                }
            }

            slot_func[0, 0] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[0])];
            slot_func[0, 1] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[1])];
            slot_func[0, 2] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[2])];

            slot_func[1, 0] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[3])];
            slot_func[1, 1] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[4])];
            slot_func[1, 2] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[5])];

            slot_func[2, 0] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[6])];
            slot_func[2, 1] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[7])];
            slot_func[2, 2] = Const.array_symbols[Convert.ToInt32(decode_str_rev_arr[8])];

            //вторая часть

            long date = (long)Convert.ToUInt64((uncode_string[1]));
            DateTime unix_epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long time_stamp_tick = date * TimeSpan.TicksPerMillisecond;
            var date_time = new DateTime(unix_epoch.Ticks + time_stamp_tick, DateTimeKind.Utc);

            //вывод
            Console.WriteLine();
            Console.WriteLine($"по коду вашего спина равного: {code} найдена комбинация");
            Printer.beaut_print(slot_func);
            Console.WriteLine($"которая была сделана: {date_time} GMT+0");

        }

        //переворот строки
        public static string add_confus(string str)
        {
            //вспомогательная функция для hash_code и hash_uncode
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}