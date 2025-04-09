using System;

namespace Halloween
{
    internal class Program
    {
        private static Postavy mojePostava;
        static void Main(string[] args)
        {
            BigPrints.PrintColored("Vítej v Gorobornu\n\n", ConsoleColor.Cyan);
            mojePostava = VyberPostavy();    
            while (mojePostava == null)
            {
                mojePostava = VyberPostavy();                
            }
            Console.Clear();
            while (true)
            {
                BigPrints.PrintColored("Menu:\n", ConsoleColor.Cyan);
                BigPrints.PrintColored("\n(1) Arena\n(2) Inventory\n(3) Stats\n(4) Shop\n(5) Exit\n", ConsoleColor.White);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Arena.Arenas(mojePostava,time: 1500,roundTime: 200);
                        break;
                    case "2":
                        while (true)
                        {
                            Inventory.InventoryChoice(mojePostava);
                            break;
                        }
                        break;
                    case "3":
                        StatCheck();
                        break;
                    case "4":
                        while (true)
                        {
                            Console.Clear();
                            Shop.GenerateShopItems(mojePostava);
                            //Inventory.inventorySlots[0] = ItemData.CrownoftheIronSentinel;
                            break;
                        }
                        break;
                    case "5":
                        BigPrints.PrintColored("\nHra se ukončila", ConsoleColor.Red);
                        Thread.Sleep(300);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        BigPrints.PrintColored("Neplatná volba.\n\n",ConsoleColor.Red);
                        break;
                }
                Console.Clear();
            }
        }

        private static Postavy VyberPostavy()
        {
            Console.WriteLine("Vyber si postavu:");
            BigPrints.PrintColored("(Z)ombie, ", ConsoleColor.Green);
            BigPrints.PrintColored("(W)itch, ", ConsoleColor.DarkMagenta);
            BigPrints.PrintColored("(S)keleton", ConsoleColor.Gray);
            Console.WriteLine();
            string vyber = Console.ReadLine()?.ToUpper();

            switch (vyber)
            {
                case "Z":
                    return new Zombie(_hp: 150f, _damage: 10, _level: 1, _golds: 0f, _xp: 100, _currentXP: 0);
                case "W":
                    return new Witch(_hp: 95f, _damage: 20f, _level: 1, _golds: 0f, _xp: 100, _currentXP: 0);
                case "S":
                    return new Skeleton(_hp: 120f, _damage: 15f, _level: 1, _golds: 0f, _xp: 100, _currentXP: 0);
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Neplatná volba.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
            }
        }     
        
        private static void StatCheck()
        {
            Console.Clear();
            BigPrints.PrintColored($"HP: {mojePostava.HP}\n", ConsoleColor.Green);
            BigPrints.PrintColored($"Damage: {mojePostava.Damage}\n", ConsoleColor.Magenta);
            BigPrints.PrintColored($"Max XP: {mojePostava.XP}\n", ConsoleColor.Red);
            BigPrints.PrintColored($"Level: {mojePostava.Level}\n", ConsoleColor.Yellow);
            BigPrints.PrintColored($"Aktuální XP: {mojePostava.CurrentXP}\n", ConsoleColor.Gray);
            BigPrints.PrintColored($"Golds: {mojePostava.Golds} \n", ConsoleColor.DarkYellow);
            Console.ReadLine();
        }
    }
}
