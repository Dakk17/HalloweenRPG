using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class BigPrints
    {
        public static void PrintColored(string text, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }
        public static void ArenaPrint(Enemy enemy, Postavy mojePostava, float currentHP, float enemyCurrentHP)
        {
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("            | ");
            PrintColored("     Já", ConsoleColor.Green);
            Console.Write("      | ");
            PrintColored("             Nepřítel\n", ConsoleColor.Red);
            Console.WriteLine("----------------------------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"HP          | ");
            PrintColored($"{currentHP,-10}", ConsoleColor.Green);
            Console.Write($"   | ");
            PrintColored($"{enemyCurrentHP}\n", ConsoleColor.Green);

            Console.Write($"AVG. DAMAGE | ");
            PrintColored($"{mojePostava.Damage,-10}", ConsoleColor.Magenta);
            Console.Write($"   | ");
            PrintColored($"{enemy.Damage}\n", ConsoleColor.Magenta);

            Console.Write($"LEVEL       | ");
            PrintColored($"{mojePostava.Level,-10}", ConsoleColor.Yellow);
            Console.Write($"   | ");
            PrintColored($"{enemy.Level}\n", ConsoleColor.Yellow);

            Console.Write($"GOLDS       | ");
            PrintColored($"{mojePostava.Golds,-10}", ConsoleColor.DarkYellow);
            Console.Write($"   | ");
            PrintColored($"{enemy.Golds}\n", ConsoleColor.DarkYellow);

            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ResetColor();
        }
    }
}
