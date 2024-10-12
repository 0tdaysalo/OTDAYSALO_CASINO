using System;
using System.Linq;

namespace SLOT_1
{
    public static class Game
    {
        //измененная функция make_spin для game();
        private static int make_spin_game(int balance, int bet, int n)
        {
            if (balance < bet)
            {
                Console.WriteLine("недостаточно средств! игра приостановлена");
                Console.WriteLine($"баланс: {balance}, ставка: {bet}");
                Console.WriteLine();
            }
            else
            {
                Slot.random_fill();
                int win = Pay.pay_out(bet, Slot.get_win_set());
                Console.WriteLine($"произошёл {n}-ый спин");
                Printer.info_about_spin(win, Slot.get_win_set(), balance, bet);
                balance = Pay.give_win(bet, win, balance);
            }
            return balance;
        }

        //получение итогового выигрыша в game()
        private static int game_total_win(int balance, int bet)
        {
            int tot_win = 0;
            if (balance < bet)
            {

            }
            else
            {
                Slot.random_fill();
                int win = Pay.pay_out(bet, Slot.get_win_set());
                tot_win += win;
            }
            return tot_win;
        }

        //полноценная консоль-игра (релиз)
        public static void game()
        {
            int total_dep = 0;
            int total_bet = 0;
            int total_win = 0;
            int total_bal;

        start: { }
            Console.Clear();
            Console.WriteLine("0TDAYSALO_CASINO, SLOT_1");
            Printer.beaut_print("0");
            Console.WriteLine("!перед началом обязательно ознакомьтесь с правилами!");
            Console.WriteLine("введите СТАРТ, чтобы запустить игру");
            Console.WriteLine("введите ПРАВИЛА, чтобы отобразить их");
            Console.WriteLine("введите ВЫХОД, чтобы покинуть игру");
            Console.WriteLine("введите ПРОСМОТР, чтобы отобразить сыгровку символов");

            string user_input_start = Console.ReadLine();

            if (user_input_start == "СТАРТ")
            {
            input_dep: { }
                Console.Clear();
                Console.WriteLine("введите сумму вашего депозита:");
                if (!Int32.TryParse(Console.ReadLine(), out int dep))
                {
                    Console.WriteLine("некорректный ввод");
                    goto input_dep;
                }
                Console.Clear();


            input_bet: { }
                Console.Clear();
                Console.WriteLine("введите сумму вашей ставки:");
                if (!Int32.TryParse(Console.ReadLine(), out int bet))
                {
                    Console.WriteLine("некорректный ввод");
                    goto input_bet;
                }
                Console.Clear();

                if (bet <= 0 || dep <= 0)
                {
                    Console.WriteLine("некорректный ввод");
                    Console.WriteLine($"с какой целью вы вводите {bet} и {dep}?");
                    return;
                }


                Console.WriteLine($"депозит: {dep}, ставка: {bet}, приятной игры!");
                Console.ReadLine();

                total_bet += bet;
                total_dep += dep;
                int count_spins_game = 0;

                while (true)
                {
                    count_spins_game++;
                    dep = make_spin_game(dep, bet, count_spins_game);

                    string user_input_game = Console.ReadLine();

                    if (user_input_game == "ДЕП")
                    {
                    cont_change_dep: { }
                        Console.WriteLine("введите сумму");
                        if (!Int32.TryParse(Console.ReadLine(), out int new_dep))
                        {
                            Console.WriteLine("некорректный ввод");
                            goto cont_change_dep;
                        }
                        else
                        {
                            dep += new_dep;
                            total_dep += new_dep;
                            Console.WriteLine($"успешное пополнение, теперь ваш баланс {dep}");
                            Console.WriteLine($"суммарно вы пополнили на {total_dep}");
                            Console.WriteLine("нажмите любую клавишу, чтобы продолжить");
                            Console.ReadKey();
                            Console.WriteLine();
                        }
                    }

                    else if (user_input_game == "БЕТ")
                    {
                    cont_change_bet: { }
                        Console.WriteLine("введите новую сумму ставки");
                        if (Int32.TryParse(Console.ReadLine(), out bet))
                        {
                            Console.WriteLine($"успешная смена баланса, теперь ваша ставка {bet}");
                            Console.WriteLine("нажмите любую клавишу, чтобы продолжить");
                            Console.ReadKey();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("некорректный ввод");
                            goto cont_change_bet;
                        }
                    }

                    else if (user_input_game == "ПРОСМОТР")
                    {
                        Console.WriteLine("введите ваш код сыгровки:");
                        Hashcode.uncode(Console.ReadLine());
                        Console.WriteLine("нажмите любую клавишу, чтобы продолжить");
                        Console.ReadKey();
                        Console.WriteLine();
                    }

                    else if (user_input_game == "ВЫХОД")
                    {
                        total_bal = dep;
                        Console.Clear();
                        Console.WriteLine("вы совершили:");
                        Console.WriteLine($"депозитов на сумму: {total_dep}");
                        Console.WriteLine($"{count_spins_game} спин(-ов)(-а) на сумму: {total_bet}");
                        Console.WriteLine($"а также, суммарно выиграли {total_win}");
                        Console.WriteLine($"теперь ваш баланс после выхода составляет {total_bal} это {((float)total_bal / total_dep) * 100}% от вашего(-их) депозита(-ов)");
                        return;
                    }

                    total_bet += bet;
                    total_win += game_total_win(dep, bet);
                }
            }

            else if (user_input_start == "ПРАВИЛА")
            {
                Console.Clear();
                Console.WriteLine("ПРАВИЛА SLOT_1:");

                Console.WriteLine("ставка может быть только целым числом");
                Console.WriteLine("при каждом совершенном спине с вашего баланса снимается сумма вашей ставки");
                Console.WriteLine("размер суммы выигрыша  зависит от размера ставки");
                Console.WriteLine("вы выигрываете не сумму, а увеличение ставки на определенный множитель");
                Console.WriteLine($"в игре {Const.count_lines} играющих линий, 3 горизонтальных и 2 по диагонали");
                Console.WriteLine("линия считается сыгравшей если в ней имеется 3 одинаковых символа");
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
                goto start;
            }

            else if (user_input_start == "ВЫХОД")
            {
                Console.Clear();
                return;
            }

            else if(user_input_start == "ПРОСМОТР")
            {
                Console.WriteLine("введите ваш код сыгровки:");
                Hashcode.uncode(Console.ReadLine());
            }

            else
            {
                Console.Clear();
                goto start;
            }
        }
    }
}