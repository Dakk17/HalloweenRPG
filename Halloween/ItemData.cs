using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class ItemData
    {
        public static Helmet HellsCap = new Helmet("Hellscap", hp: 10f, damage: 2f, golds: 20f, upgrade: 0);
        public static Helmet CrownoftheIronSentinel = new Helmet("Crown of the Iron Sentinel", hp: 20f, damage: 2f, golds: 35f, upgrade: 0);
        public static Helmet HelmoftheFallenKnight = new Helmet("Helm of the Fallen Knight", hp: 50f, damage: 5f, golds: 60f, upgrade: 0);

        public static Chestplate BreastplateofUnyieldingWill = new Chestplate("Breastplate of Unyielding Will", hp: 15f, damage: 0f, golds: 20f, upgrade: 0);
        public static Chestplate AegisoftheSteelborn = new Chestplate("Aegis of the Steelborn", hp: 25f, damage: 1f, golds: 35f, upgrade: 0);
        public static Chestplate DragonsHeartplate = new Chestplate("Dragon’s Heartplate", hp: 65f, damage: 0f, golds: 60f, upgrade: 0);

        public static Leggings LegguardsoftheSilentStalker = new Leggings("Legguards of the Silent Stalker", hp: 10f, damage: 1f, golds: 20f, upgrade: 0);
        public static Leggings GreavesoftheLostVanguard = new Leggings("Greaves of the Lost Vanguard", hp: 20f, damage: 1f, golds: 35f, upgrade: 0);
        public static Leggings ShadowstepLegwraps = new Leggings("Shadowstep Legwraps", hp: 30f, damage: 2f, golds: 60f, upgrade: 0);

        public static Weapon BladeoftheEternalHunt = new Weapon("Blade of the Eternal Hunt", hp: 2f, damage: 15f, golds: 20f, upgrade: 0);
        public static Weapon Shadowmourne = new Weapon("Shadowmourne", hp: 2f, damage: 22.5f, golds: 35f, upgrade: 0);
        public static Weapon Soulreaver = new Weapon("Soulreaver", hp: 1f, damage: 35.5f, golds: 60f, upgrade: 0);
    }
}
