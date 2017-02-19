using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.metaside
{
    class Beast
    {
        private int power;

        public Beast(int minPower, int maxPower)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            this.power = rand.Next(minPower, maxPower);
        }

        public int getPower()
        {
            return power;
        }
    }
}
