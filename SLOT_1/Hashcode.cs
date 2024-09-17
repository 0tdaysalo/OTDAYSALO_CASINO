using System;


namespace SLOT_1_optimize_version
{
    public class Hashcode
    {
        //функция которая будет хэшировать данные о спине в виде небольшого кусочка кода по которому можно будет воспроизвести спин
        public static string hash_code(char[,] slot)
        {
            string code = string.Empty;

            for (int i = 0; i < Const.length; i++)
            {
                for (int j = 0; j < Const.length; j++)
                {
                    for (int k = 0; k < Program.array_symbols.Length; k++)
                    {
                        if (slot[i, j] == Program.array_symbols[k])
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

        //функция воспроизведения спина по хэш коду
        public static void hash_uncode(string code)
        {
            //общее
            string[] uncode_string = code.Split('_');
            char[,] slot_func = new char[Const.length, Const.length];

            //первая часть
            string decode_str_rev = add_confus((uncode_string[0]));//переворачиваем строку
            string[] decode_str_rev_arr = new string[decode_str_rev.Length];//перевернутая последовательнсть в виде массива(заполненная ниже)
            for (int i = 0; i < decode_str_rev.Length; i++)
            {
                decode_str_rev_arr[i] = decode_str_rev[i].ToString();
                if (decode_str_rev_arr[i] == "A")
                {
                    decode_str_rev_arr[i] = 10.ToString();
                }
            }



            slot_func[0, 0] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[0])];
            slot_func[0, 1] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[1])];
            slot_func[0, 2] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[2])];

            slot_func[1, 0] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[3])];
            slot_func[1, 1] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[4])];
            slot_func[1, 2] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[5])];

            slot_func[2, 0] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[6])];
            slot_func[2, 1] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[7])];
            slot_func[2, 2] = Program.array_symbols[Convert.ToInt32(decode_str_rev_arr[8])];
            Console.WriteLine();
            //вторая часть
            long date = (long)Convert.ToUInt64((uncode_string[1]));
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long unixTimeStampInTicks = date * TimeSpan.TicksPerMillisecond;
            var qwerty = new DateTime(unixEpoch.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
            Console.WriteLine($"по коду вашего спина равного: {code} найдена комбинация");
            Program.good_beaut_print(slot_func);

            Console.WriteLine($"которая была сделана: {qwerty} GMT+0");

        }

        //вспомогающая в кодировке функция которая 
        public static string add_confus(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
