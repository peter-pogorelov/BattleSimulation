using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.darkside
{
    abstract class AbstractOrc : metaside.MiddleEarthCitizen
    {
        public AbstractOrc(string name) : base(name, 8, 10){}

        protected AbstractOrc(string name, int minPower, int maxPower) : base(name, minPower, maxPower) { }
    }
}
