using System;

namespace SLOT_1
{
    public static class Game
    {

        //получение итогового выигрыша в game()
        private static int total_win_game(int balance, int bet)
        {
            int tot_win = 0;
            if (!(balance < bet))
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
            Printer.slot_print("0");
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

                if (Console.ReadLine() == "ВЫХОД")
                {
                    goto start;
                }

                total_bet += bet;
                total_dep += dep;
                int count_spins_game = 0;

                while (true)
                {
                    count_spins_game++;
                    dep = Auto.make_spin_game(dep, bet, count_spins_game);

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
                        Console.WriteLine($"ваш баланс составляет {total_bal} это {((float)total_bal / total_dep) * 100}% от вашего(-их) депозита(-ов)");
                        return;
                    }

                    total_bet += bet;
                    total_win += total_win_game(dep, bet);
                }
            }

            else if (user_input_start == "ПРАВИЛА")
            {
                Console.Clear();
                Printer.slot_rule();
                goto start;
            }

            else if (user_input_start == "ВЫХОД")
            {
                Console.Clear();
                return;
            }

            else if (user_input_start == "ПРОСМОТР")
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