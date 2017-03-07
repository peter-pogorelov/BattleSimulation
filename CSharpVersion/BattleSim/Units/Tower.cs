using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    abstract class Tower: Unit
    {
        protected Tower(int val, int attack=0): base(val, attack, 1) { }
    }
}
