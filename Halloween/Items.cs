using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class Items
    {
        public string Name { get; }
        public float HP { get; }
        public float Damage { get; }
        public float Golds {  get; }
        public int Upgrade { get; }

        protected Items(string name, float hp, float damage, float golds, int upgrade)
        {
            Name = name;
            HP = hp;
            Damage = damage;
            Golds = golds;
            Upgrade = upgrade;
        }
    }
}

