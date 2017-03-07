using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    class Healer : Unit, HealingUnit
    {
        protected int healing = 2;
        override public int Attack
        {
            get
            {
                throw new Exception("Healing units cannot attack");
            }
        }

        public Healer(int healing=15) : base(3, initiative:5) { this.healing = healing; }

        public void heal(List<Unit> t)
        {
            Unit unt = BattleSim.Utils.getDamagedAliveUnit(t);
            if (unt != null)
            {
                unt.Health += healing;
            }
        }
    }
}
