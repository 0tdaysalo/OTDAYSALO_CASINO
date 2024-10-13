using System;

namespace SLOT_1
{
    public static class Game
    {

        //получение итогового выигрыша в game()
        private static uint total_win_game(uint balance, uint bet)
        {
            uint tot_win = 0;
            if (!(balance < bet))
            {
                Slot.random_fill();
                uint win = Pay.pay_out(bet, Slot.get_win_set());
                tot_win += win;
            }
            return tot_win;
        }

        //полноценная консоль-игра (релиз)
        public static void game()
        {
            uint total_dep = 0;
            uint total_bet = 0;
            uint total_win = 0;
            uint total_bal;

        start: { }
            Console.Clear();
            Printer.slot_start();

            string user_input_start = Console.ReadLine();

            switch (user_input_start)
            {
                case "СТАРТ":
                input_dep: { }
                    Console.Clear();
                    Console.WriteLine("введите сумму вашего депозита:");
                    if (!UInt32.TryParse(Console.ReadLine(), out uint dep))
                    {
                        Console.WriteLine("некорректный ввод");
                        goto input_dep;
                    }
                    Console.Clear();


                input_bet: { }
                    Console.Clear();
                    Console.WriteLine("введите сумму вашей ставки:");
                    if (!UInt32.TryParse(Console.ReadLine(), out uint bet))
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
                    uint count_spins_game = 0;

                    while (true)
                    {
                        count_spins_game++;
                        dep = Auto.make_spin_game(dep, bet, count_spins_game);

                        string user_input_game = Console.ReadLine();
                        switch (user_input_game)
                        {
                            case ("ДЕП"):
                            cont_change_dep: { }
                                Console.WriteLine();
                                Console.WriteLine("введите сумму:");
                                if (UInt32.TryParse(Console.ReadLine(), out uint new_dep))
                                {
                                    dep += new_dep;
                                    total_dep += new_dep;
                                    Console.WriteLine($"успешное пополнение, теперь ваш баланс: {dep}");
                                    Console.WriteLine($"суммарно вы пополнили на {total_dep}");
                                    Console.WriteLine("нажмите любую клавишу, чтобы продолжить");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("некорректный ввод");
                                    goto cont_change_dep;
                                }
                                Console.WriteLine();
                                break;
                            case ("БЕТ"):
                            cont_change_bet: { }
                                Console.WriteLine();
                                Console.WriteLine("введите новую сумму ставки:");
                                if (UInt32.TryParse(Console.ReadLine(), out bet))
                                {
                                    Console.WriteLine($"успешная смена, теперь ваша ставка: {bet}");
                                    Console.WriteLine("нажмите любую клавишу, чтобы продолжить");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("некорректный ввод");
                                    goto cont_change_bet;
                                }
                                Console.WriteLine();
                                break;
                            case ("ПРОСМОТР"):
                                Console.WriteLine("введите ваш код сыгровки:");
                                Hashcode.uncode(Console.ReadLine());
                                Console.WriteLine("нажмите любую клавишу, чтобы продолжить");
                                Console.ReadKey();
                                Console.WriteLine();
                                break;
                            case ("ВЫХОД"):
                                total_bal = dep;
                                Console.Clear();
                                Console.WriteLine("вы совершили:");
                                Console.WriteLine($"депозитов на сумму: {total_dep}");
                                Console.WriteLine($"{count_spins_game} спин(-ов)(-а) на сумму: {total_bet}");
                                Console.WriteLine($"а также, суммарно выиграли {total_win}");
                                Console.WriteLine($"ваш баланс составляет {total_bal} это {((float)total_bal / total_dep) * 100}% от вашего(-их) депозита(-ов)");
                                return;
                            case ("МЕНЮ"):
                                goto start;
                        }

                        total_bet += bet;
                        total_win += total_win_game(dep, bet);
                    }
                case "ПРАВИЛА":
                    Console.Clear();
                    Printer.slot_rule();
                    Console.ReadKey();
                    goto start;
                case "ВЫХОД":
                    Console.Clear();
                    return;
                case "ПРОСМОТР":
                    Console.WriteLine("введите ваш код сыгровки:");
                    Hashcode.uncode(Console.ReadLine());
                    break;
                default:
                    Console.Clear();
                    goto start;
            }
        }
    }
}