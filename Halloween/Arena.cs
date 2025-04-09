using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class Arena
    {
        public static void Arenas(Postavy mojePostava,int time, int roundTime)
        {
            BigPrints.PrintColored("Vítej v aréně!\n", ConsoleColor.Cyan);
            Random random = new Random();

            int enemyLevel = random.Next(mojePostava.Level >= 3 ? mojePostava.Level - 2 : 1, mojePostava.Level + 2);
            float enemyGold = random.Next(2*(int)(Math.Pow(1+enemyLevel,1.5)), 3 * (int)(Math.Pow(1 + enemyLevel, 1.5)));
            float enemyHP = random.Next(80 * enemyLevel / 2, 150 * enemyLevel * 10 / 15);
            float enemyDamage = random.Next(5 * enemyLevel, 20 * enemyLevel);
            int enemyXP = random.Next(enemyLevel * mojePostava.XP / 100 * 20, enemyLevel * mojePostava.XP / 100 * 30);

            Enemy enemy = new Enemy(enemyHP, enemyDamage, enemyLevel, enemyGold, enemyXP, 0);

            float currentHP = mojePostava.HP;
            float enemyCurrentHP = enemy.HP;
            bool playerTurn = true;

            while (currentHP >= 0 && enemyCurrentHP >= 0)
            {
                Console.Clear();
                if (playerTurn)
                {
                    int dodgeChance = random.Next(1, 10);
                    float flatDamage = random.Next((int)mojePostava.Damage - 2, (int)mojePostava.Damage + 2);
                    if (dodgeChance == 1)
                    {
                        BigPrints.ArenaPrint(enemy, mojePostava, currentHP, enemyCurrentHP);
                        BigPrints.PrintColored("Nepřítel uhnul.", ConsoleColor.Red);
                    }
                    else
                    {
                        enemyCurrentHP -= flatDamage;
                        BigPrints.ArenaPrint(enemy, mojePostava, currentHP, enemyCurrentHP);
                        BigPrints.PrintColored($"Zasahuješ nepřítele za {flatDamage}", ConsoleColor.Green);
                    }
                }
                else
                {
                    int dodgeChance = random.Next(1, 10);
                    float flatDamage = random.Next((int)enemy.Damage - 2, (int)enemy.Damage + 2);
                    if (dodgeChance == 1)
                    {
                        BigPrints.ArenaPrint(enemy, mojePostava, currentHP, enemyCurrentHP);
                        BigPrints.PrintColored("Uhnul jsi", ConsoleColor.Green);
                        ;
                    }
                    else
                    {
                        currentHP -= flatDamage;
                        BigPrints.ArenaPrint(enemy, mojePostava, currentHP, enemyCurrentHP);
                        BigPrints.PrintColored($"Nepřítel tě zasahuje za {flatDamage}", ConsoleColor.Red);
                    }
                }
                playerTurn = !playerTurn;

                if (time - roundTime > 0)
                    Thread.Sleep(time - roundTime);
                roundTime += 75;
            }
            if (currentHP <= 0)
            {
                BigPrints.PrintColored("\nProhrál jsi", ConsoleColor.Red);
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                BigPrints.PrintColored("\nVyhrál jsi", ConsoleColor.Green);
                mojePostava.CurrentXP += enemyXP;
                mojePostava.Golds += enemy.Golds;
                if (mojePostava.XP - mojePostava.CurrentXP <= 0)
                {
                    mojePostava.CurrentXP -= mojePostava.XP;
                    mojePostava.Level++;
                    mojePostava.XP = 100 * (int)Math.Pow(mojePostava.Level, 2) + 20 * mojePostava.Level;
                    mojePostava.Damage += (int)Math.Pow(1 + mojePostava.Level, 1.5);
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
