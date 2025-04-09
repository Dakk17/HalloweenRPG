using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Halloween
{
    internal class Postavy
    {
        private float _hp;
        private float _damage;
        private int _level;
        private float _golds;
        private int _xp;
        private int _currentXP;
        public Inventory Inventory { get; private set;} = new Inventory();
        public int CurrentXP
        {
            get => _currentXP;
            set => _currentXP = value;
        }
        public int XP
        {
            get => _xp;
            set => _xp = value;
        }
        public float HP
        {
            get => _hp;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Default HP nemůže být 0 a méně");
                }
                _hp = value;
            }
        }
        public float Damage
        {
            get => _damage;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Default damage nemůže být 0 a méně");
                }
                _damage = value;
            }
        }
        public int Level
        {
            get => _level;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Default level nemůže být záporný");
                }
                _level = value;
            }
        }
        public float Golds
        {
            get => _golds;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Default golds nemůže být záporný");
                }
                _golds = value;
            }
        }
        protected Postavy(float hp, float _damage, int level, float golds, int xp, int currentXP) 
        {
            HP = hp;
            Damage = _damage;
            Level = level;
            Golds = golds;
            XP = xp;
            CurrentXP = currentXP;
        }
    }

    class Zombie:Postavy
    {
        public Zombie(float _hp, float _damage, int _level, float _golds, int _xp, int _currentXP) : base(_hp, _damage, _level, _golds, _xp, _currentXP) { }
    }
    class Witch:Postavy
    {
        public Witch(float _hp, float _damage, int _level, float _golds, int _xp, int _currentXP) : base(_hp, _damage, _level, _golds, _xp, _currentXP) { }
    }

    class Skeleton:Postavy
    {
        public Skeleton(float _hp, float _damage, int _level, float _golds, int _xp, int _currentXP) : base(_hp, _damage, _level, _golds, _xp, _currentXP) { }
    }

    class Enemy : Postavy
    {
        public Enemy(float hp, float _damage, int _level, float _golds, int _xp, int _currentXP) : base(hp, _damage, _level, _golds, _xp, _currentXP) { }
    }
}
