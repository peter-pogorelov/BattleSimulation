using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    class HealingTower: Tower, HealingUnit
    {
        protected int healing = 15;
        override public int Attack
        {
            get
            {
                throw new Exception("Healing units cannot attack");
            }
        }

        public HealingTower(int healing=15) : base(20) { this.healing = healing; }

        public void heal(List<Unit> t)
        {
            foreach(int i in Enumerable.Range(1, 3))
            {
                Unit unt = BattleSim.Utils.getDamagedAliveUnit(t);

                if (unt != null)
                {
                    unt.Health += healing;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
