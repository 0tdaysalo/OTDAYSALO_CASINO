using System;
using System.Linq;

namespace SLOT_1
{
    public static class Game
    {
        //измененная функция make_full_auto_spin для game();
        public static int make_full_auto_spin_game(int balance, int bet, int n)
        {
            if (balance < bet)
            {
                Console.WriteLine("недостаточно средств! игра приостановлена");
                Console.WriteLine();
            }
            else
            {
                Program.random_fill(Program.slot);
                int win = Program.pay_out(bet, Program.get_win_set(Program.slot));
                Console.WriteLine($"произошёл {n}-ый спин");
                Printer.info_about_spin(win, Program.get_win_set(Program.slot), balance, Program.slot, bet);
                balance = Program.give_win(bet, win, balance);
            }
            return balance;
        }

        //получение итогового выигрыша в game()
        public static int game_total_win(int balance, int bet)
        {
            int tot_win = 0;
            if (balance < bet)
            {

            }
            else
            {
                Program.random_fill(Program.slot);
                int win = Program.pay_out(bet,  Program.get_win_set(Program. slot));
                tot_win += win;
            }
            return tot_win;
        }

        //полноценная консоль-игра (релиз)
        public static int game()
        {
            int total_dep = 0;
            int total_bet = 0;
            int total_win = 0;
            int total_bal;
        start: { }
            Console.Clear();
            Console.WriteLine("вас приветствует 0TDAYSALO_CASINO, SLOT_1");
            Console.WriteLine("-----");
            for (int i = 0; i < Const.length; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Const.length; j++)
                {
                    Console.Write("0");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----");
            Console.WriteLine("введите НАЧАТЬ, чтобы начать играть");
            Console.WriteLine("перед тем как начать играть, обязательно обратитесь к правилам");
            Console.WriteLine("для того чтобы обратиться к правилам введите rule");


            string user_input_1 = Console.ReadLine();
            if (user_input_1 == "НАЧАТЬ")
            {
            cont_1: { }
                Console.Clear();
                Console.WriteLine("введите сумму вашего депозита:");
                if (!Int32.TryParse(Console.ReadLine(), out int dep))
                {
                    Console.WriteLine("некорректный ввод");
                    goto cont_1;
                }
                Console.Clear();


            cont_2: { }
                Console.Clear();
                Console.WriteLine("введите сумму вашей ставки:");
                if (!Int32.TryParse(Console.ReadLine(), out int bet))
                {
                    Console.WriteLine("некорректный ввод");
                    goto cont_2;
                }
                Console.Clear();

                if (bet <= 0 || dep <= 0)
                {
                    Console.WriteLine("неверно");
                    Console.WriteLine($"с какой целью вы вводите {bet} и {dep}?");
                    return 0;
                }


                Console.WriteLine($"ваш депозит равен {dep}, ваша ставка равна {bet}");
                total_bet += bet;
                total_dep += dep;



                int count_spins_game = 0;
                while (true)
                {
                    count_spins_game++;
                    dep = make_full_auto_spin_game(dep, bet, count_spins_game);

                    string user_input_2 = Console.ReadLine();
                    if (user_input_2 == "ДЕП")
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
                            Console.WriteLine("успешно!");
                            dep += new_dep;
                            total_dep += new_dep;
                        }
                    }
                    else if (user_input_2 == "БЕТ")
                    {
                    cont_change_bet: { }
                        Console.WriteLine("введите новую сумму ставки");
                        if (!Int32.TryParse(Console.ReadLine(), out bet))
                        {
                            Console.WriteLine("некорректный ввод");
                            goto cont_change_bet;
                        }
                        else
                        {
                            Console.WriteLine("успешно!");
                        }
                    }
                    else if (user_input_2 == "ВЫХОД")
                    {
                        total_bal = dep;
                        Console.Clear();
                        Console.WriteLine($"вы совершили депозитов на сумму: {total_dep}");
                        Console.WriteLine($"{count_spins_game} спин(-ов)(-а) на сумму: {total_bet}");
                        Console.WriteLine($"суммарно выиграли {total_win}");
                        Console.WriteLine($"ваш баланс после выхода составляет {total_bal} это {((float)total_bal / total_dep) * 100}% от вашего(-их) депозита(-ов)");
                        return 0;
                    }
                    total_bet += bet;
                    total_win += game_total_win(dep, bet);
                }
            }

            else if (user_input_1 == "rule")
            {
                Console.Clear();
                Console.WriteLine($"ПРАВИЛА SLOT_1:");
                Console.WriteLine($"ставка может быть только целым числом");
                Console.WriteLine($"если вы желаете внести депзоит, введите ДЕП после начала игры");
                Console.WriteLine($"если вы желаете изменить ставку, введите БЕТ после начала игры");
                Console.WriteLine($"если вы желаете завершить игру введите ВЫХОД после начала игры");
                Console.WriteLine($"при завершении игры вам будет видна ваша статистика");
                Console.WriteLine($"при каждом совершенном спине с вашего баланса снимается сумма вашей ставки");
                Console.WriteLine($"размер суммы выигрыша  зависит от размера ставки");
                Console.WriteLine($"вы выигрываете не сумму, а увеличение ставки на определенный множитель");
                Console.WriteLine($"в игре {Const.count_lines} играющих линий, 3 горизонтальных и 2 по диагонали");
                Console.WriteLine($"линия считается сыгравшей если в ней имеется 3 одинаковых символа");
                foreach (var win in Program.array_symbols_dic)
                {
                    Console.WriteLine($"символ {win.Key} увеличивает ставку в {win.Value} раз");
                }
                Console.WriteLine($"максимальный выигрыш составляет X{Program.array_symbols_dic.Values.Max() * Const.count_lines} от ставки");
                Console.WriteLine($"теоретический процент возврата вашего баланса составляет 97,7%");
                Console.WriteLine($"НИКОГДА НЕ ИГРАЙТЕ В АЗАРТНЫЕ ИГРЫ, А ТЕМ БОЛЕЕ В КАЗИНО, ИНАЧЕ ВЫ ОСТАНЕТЕСЬ В МИНУСЕ");
                Console.WriteLine($"\nнажмите Enter, чтобы выйти");
                Console.ReadLine();
                goto start;
            }
            else
            {
                Console.Clear();
                goto start;
            }
        }
    }
}
