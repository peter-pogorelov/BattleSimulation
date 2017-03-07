using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    interface HealingUnit
    {
        void heal(List<Unit> t);
    }
}
