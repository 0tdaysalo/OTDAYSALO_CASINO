using System;

namespace SLOT_1
{
    public static class Hashcode
    {
        //хэширование данных о спине в виде строки
        public static string code(char[,] slot)
        {
            string code = string.Empty;

            for (uint i = 0; i < Const.length; i++)
            {
                for (uint j = 0; j < Const.length; j++)
                {
                    int symbolIndex = Array.IndexOf(Const.array_symbols, slot[i, j]);

                    if (symbolIndex >= 0)
                    {
                        //если символ находится в массиве, то добавляем его индекс в шестнадцатеричной системе
                        code += symbolIndex.ToString("X");
                    }
                    else
                    {
                        throw new Exception("Символ не найден в массиве допустимых символов.");
                    }
                }
            }

            //переворот строки
            code = reverse_string(code);

            //сдобавление времени
            long time = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            code += "_" + time.ToString();

            return code;
        }

        //воспроизведение спина по хэш-коду
        public static void uncode(string code)
        {
            string[] uncode_string = code.Split('_');
            if (uncode_string.Length != 2)
            {
                throw new Exception("Неверный формат хэш-кода.");
            }

            char[,] slot_func = new char[Const.length, Const.length];

            // Первая часть - раскодируем строку
            string decoded_str = reverse_string(uncode_string[0]);

            // Проверяем длину строки для корректной обработки
            if (decoded_str.Length != Const.length * Const.length)
            {
                throw new Exception("Длина раскодированной строки не соответствует ожидаемой длине.");
            }

            // Преобразуем раскодированную строку в символы
            for (uint i = 0; i < Const.length; i++)
            {
                for (uint j = 0; j < Const.length; j++)
                {
                    uint index = (i * Const.length) + j;
                    string hexValue = decoded_str[(int)index].ToString();

                    // Парсим символ как шестнадцатеричное число
                    int symbolIndex = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

                    // Проверяем допустимость индекса символа
                    if (symbolIndex >= 0 && symbolIndex < Const.array_symbols.Length)
                    {
                        slot_func[i, j] = Const.array_symbols[symbolIndex];
                    }
                    else
                    {
                        throw new Exception("Неверный индекс символа при декодировании.");
                    }
                }
            }

            // Вторая часть - метка времени
            long date = long.Parse(uncode_string[1]);
            DateTime unix_epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long time_stamp_tick = date * TimeSpan.TicksPerMillisecond;
            DateTime date_time = new DateTime(unix_epoch.Ticks + time_stamp_tick, DateTimeKind.Utc);

            // Вывод результата
            Console.WriteLine();
            Console.WriteLine($"По коду вашего спина равного: {code}, найдена комбинация:");
            Printer.slot_print(slot_func);
            Console.WriteLine($"Которая была сделана: {date_time} GMT+0");
        }

        //переворот строки
        private static string reverse_string(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
