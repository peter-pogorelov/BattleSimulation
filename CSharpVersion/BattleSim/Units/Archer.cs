using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    class Archer: Unit
    {
        protected Archer(int val, int attack=0, int initiative=4): base(val, attack, initiative) { }
        public Archer() : base(3, 3, 4) { }
    }
}
