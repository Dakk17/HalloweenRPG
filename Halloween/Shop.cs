using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class Shop
    { 
        private static Random random = new Random();
        public static void GenerateShopItems(Postavy mojePostava)
        {
            var stop = true;
            while (stop)
            {

                var items = typeof(ItemData)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(field => field.FieldType.IsSubclassOf(typeof(Items)))
                    .Select(field => (Items)field.GetValue(null))
                    .ToList();

                var randomItemShop = items
                    .OrderBy(_ => random.Next())
                    .Take(4)
                    .ToArray();

                BigPrints.PrintColored("Vítej v obchodě! \n", ConsoleColor.Cyan);
                BigPrints.PrintColored($"Goldy: {mojePostava.Golds}\n", ConsoleColor.DarkYellow);

                foreach (var item in randomItemShop)
                {
                    Console.Write("\nName: ");
                    BigPrints.PrintColored($"{item.Name}\n", ConsoleColor.Gray);
                    Console.Write("Typ: ");
                    BigPrints.PrintColored($"{item.GetType().Name}\n", ConsoleColor.Gray);
                    Console.Write("HP: ");
                    BigPrints.PrintColored($"{item.HP}\n", ConsoleColor.Green);
                    Console.Write("Damage: ");
                    BigPrints.PrintColored($"{item.Damage} \n", ConsoleColor.Magenta);
                    Console.Write("Golds: ");
                    BigPrints.PrintColored($"{item.Golds} \n", ConsoleColor.DarkYellow);
                }

                Console.WriteLine("\n(K)oupit / (P)rodat / (Z)pět");
                var vyberShop = Console.ReadLine()?.ToUpper();
                switch (vyberShop)
                {
                    case "K":
                        KoupitItem(randomItemShop, mojePostava, items);
                        break;                
                    case "P":
                        break;               
                    case "Z":
                        stop = false;
                        break;
                }
            }
        }
        private static void KoupitItem(Items[] randomItemShop, Postavy mojePostava, List<Items> items)
        {
            Console.WriteLine("Vyberte co chcete koupit 1-4");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Char.IsDigit(input[0]))
            {
                int itemInput = int.Parse(input[0].ToString());
                int? validInput = (itemInput >= 1 && itemInput <= 4) ? itemInput : (int?)null;
                if (validInput.HasValue)
                {
                    int itemIntInput = validInput.Value - 1;
                    var selectedItem = randomItemShop[itemIntInput];

                    if (selectedItem != null)
                    {
                        if(selectedItem.Golds <= mojePostava.Golds)
                        {
                            mojePostava.Golds -= selectedItem.Golds;
                            int freeSlotIndex = Array.FindIndex(Inventory.inventorySlots, slot => slot == null);
                            if (freeSlotIndex != -1)
                            {
                                Inventory.inventorySlots[freeSlotIndex] = selectedItem;

                                randomItemShop[itemIntInput] = items
                                    .OrderBy(_ => random.Next())
                                    .First();
                            }
                        }
                        else
                        {
                            BigPrints.PrintColored("Nedostatek goldů", ConsoleColor.Red);
                            Thread.Sleep(350);
                        }
                    }
                    else
                    {
                        BigPrints.PrintColored("Špatný vstup", ConsoleColor.Red);
                        Thread.Sleep(350);
                    }
                }
            }

        }
    }
}
