using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    class Warrior : Unit
    {
        protected Warrior(int val, int attack=0, int initiative=6): base(val, attack, initiative) { }
        public Warrior(): base(5, 2, 6){}
    }
}
