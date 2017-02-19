using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.kindside
{
    class Elf : metaside.MiddleEarthCitizen
    {
        public Elf() : base("Elf", 4, 7) {}

        protected Elf(int val) : base("Elf", val) { }
    }
}
