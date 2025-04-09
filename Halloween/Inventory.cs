using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class Inventory
    {
        public Helmet HelmetSlot { get; private set; }
        public Chestplate ChestplateSlot { get; private set; }
        public Leggings LeggingsSlot { get; private set; }
        public Weapon WeaponSlot { get; private set; }
        public static Items[] inventorySlots = new Items[8];

        public void EquipHelmet(Helmet helmet) => HelmetSlot = helmet;
        public void EquipChestplate(Chestplate chestplate) => ChestplateSlot = chestplate;
        public void EquipLeggings(Leggings leggins) => LeggingsSlot = leggins;
        public void EquipWeaon(Weapon weapon) => WeaponSlot = weapon;

        public void UnEquipHelmet(Helmet helmet) => HelmetSlot = null;
        public void UnEquipChestplate(Chestplate chestplate) => ChestplateSlot = null;
        public void UnEquipLeggings(Leggings leggins) => LeggingsSlot = null;
        public void UnEquipWeaon(Weapon weapon) => WeaponSlot = null;

        private static Dictionary<Type, MethodInfo> equipMethods = new Dictionary<Type, MethodInfo>()
        {
            { typeof(Helmet), typeof(Inventory).GetMethod("EquipHelmet") },
            { typeof(Chestplate), typeof(Inventory).GetMethod("EquipChestplate") },
            { typeof(Leggings), typeof(Inventory).GetMethod("EquipLeggings") },
            { typeof(Weapon), typeof(Inventory).GetMethod("EquipWeapon") }
        };
        private static Dictionary<Type, MethodInfo> unEquipMethods = new Dictionary<Type, MethodInfo>()
        {
            { typeof(Helmet), typeof(Inventory).GetMethod("UnEquipHelmet") },
            { typeof(Chestplate), typeof(Inventory).GetMethod("UnEquipChestplate") },
            { typeof(Leggings), typeof(Inventory).GetMethod("UnEquipLeggings") },
            { typeof(Weapon), typeof(Inventory).GetMethod("UnEquipWeapon") }
        };

        public static void InventorySlotsShow()
        {
            BigPrints.PrintColored("Inventory:\n\n", ConsoleColor.Cyan);
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                Console.WriteLine("Slot" +(i+1) + ": " + (inventorySlots[i] == null ? "Prázdný slot" : inventorySlots[i].Name));
            }
        }

        public static void EquipmentShow(Postavy mojePostava)
        {
            BigPrints.PrintColored("\nEquipment:\n\n", ConsoleColor.Cyan);
            var slotNames = new Dictionary<string, Items>()
            {
                { "Helmet Slot", mojePostava.Inventory.HelmetSlot },
                { "Chestplate Slot", mojePostava.Inventory.ChestplateSlot },
                { "Leggings Slot", mojePostava.Inventory.LeggingsSlot },
                { "Weapon Slot", mojePostava.Inventory.WeaponSlot }
            };
            foreach (var slot in slotNames)
            {
                string slotStatus = slot.Value == null ? "Prázdný slot" : slot.Value.Name;
                Console.WriteLine($"{slot.Key}: {slotStatus}");
            }
        }

        public static void Equip(Postavy mojePostava)
        {
            Console.WriteLine("\nVyberte item z inventáře 1-8: \n");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Char.IsDigit(input[0]))
            {
                int itemInput = int.Parse(input[0].ToString());
                int? validInput = (itemInput >= 1 && itemInput <= 8) ? itemInput : (int?)null;
                if (validInput.HasValue)
                {
                    int itemIntInput = validInput.Value - 1;
                    var selectedItem = inventorySlots[itemIntInput];

                    if (selectedItem != null)
                    {
                        var itemType = selectedItem.GetType();
                        if (equipMethods.TryGetValue(itemType, out MethodInfo equipMethod))
                        {
                            equipMethod.Invoke(mojePostava.Inventory, new[] { selectedItem });
                            inventorySlots[itemIntInput] = null;
                            mojePostava.Damage += selectedItem.Damage;
                            mojePostava.HP += selectedItem.HP;
                        }
                    }
                    else
                    {
                        BigPrints.PrintColored("Inventory slot je prázdný", ConsoleColor.Red);
                        Thread.Sleep(350);
                    }           
                }
            }
        }

        public static void Unequip(Postavy mojePostava)
        {
            Console.WriteLine("\nVyberte co chcete sundat 1-4: \n");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Char.IsDigit(input[0]))
            {
                int itemInput = int.Parse(input[0].ToString());
                int? validInput = (itemInput >= 1 && itemInput <= 8) ? itemInput : (int?)null;
                if (validInput.HasValue)
                {
                    int itemIntInput = Char.IsDigit((char)validInput.Value) ? (int.Parse(validInput.Value.ToString()) - 1) : 0;
                    Items[] slots =
                    [
                        mojePostava.Inventory.HelmetSlot,
                        mojePostava.Inventory.ChestplateSlot,
                        mojePostava.Inventory.LeggingsSlot,
                        mojePostava.Inventory.WeaponSlot
                    ];
                    var selectedItem = slots[itemIntInput];
                    if (selectedItem != null)
                    {
                        var itemType = selectedItem.GetType();
                        if (unEquipMethods.TryGetValue(itemType, out MethodInfo unEquipMethod))
                        {
                            unEquipMethod.Invoke(mojePostava.Inventory, new[] { selectedItem });
                        }
                        if (inventorySlots.All(slot => slot != null))
                        {
                            BigPrints.PrintColored("\nVšechny inventory sloty jsou plné\n", ConsoleColor.Red);
                            return;
                        }

                        int freeSlotIndex = Array.FindIndex(inventorySlots, slot => slot == null);

                        if (freeSlotIndex != -1)
                        {
                            inventorySlots[freeSlotIndex] = selectedItem;
                            mojePostava.Damage -= selectedItem.Damage;
                            mojePostava.HP -= selectedItem.HP;
                        }
                    }
                    else
                    {
                        BigPrints.PrintColored("Inventory slot je prázdný", ConsoleColor.Red);
                        Thread.Sleep(350);
                    }
                }
            }
        }
        public static void InventoryChoice(Postavy mojePostava)
        {
            var invPlay = true;
            while (invPlay)
            {
                Console.Clear();
                InventorySlotsShow();
                EquipmentShow(mojePostava);
                BigPrints.PrintColored("\n(E)quip / (U)nequip / (B)ack\n", ConsoleColor.Red);
                var choiceInput = Console.ReadLine()?.ToUpper();
                switch (choiceInput)
                {
                    case "E":
                        Console.Clear();
                        InventorySlotsShow();
                        EquipmentShow(mojePostava);
                        BigPrints.PrintColored("\n(E)quip / (U)nequip / (B)ack\n", ConsoleColor.Red);
                        Equip(mojePostava);
                        break;
                    case "U":
                        Console.Clear();
                        InventorySlotsShow();
                        EquipmentShow(mojePostava);
                        BigPrints.PrintColored("\n(E)quip / (U)nequip / (B)ack\n", ConsoleColor.Red);
                        Unequip(mojePostava);
                        break;
                    case "B":
                        invPlay = false;
                        break;
                }                
            }
        }
    }
    internal class Helmet : Items
    {
        public Helmet(string jmeno, float hp, float damage, float golds, int upgrade) : base(jmeno, hp, damage, golds, upgrade) { }
    }
    internal class Chestplate : Items
    {
        public Chestplate(string jmeno, float hp, float damage, float golds, int upgrade) : base(jmeno, hp, damage, golds, upgrade) { }
    }
    internal class Leggings : Items
    {
        public Leggings(string jmeno, float hp, float damage, float golds, int upgrade) : base(jmeno, hp, damage, golds, upgrade) { }
    }
    internal class Weapon : Items
    {
        public Weapon(string jmeno, float hp, float damage, float golds, int upgrade) : base(jmeno, hp, damage, golds, upgrade) { }
    }
}
