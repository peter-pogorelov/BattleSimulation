using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleEarth.metaside;

namespace MiddleEarth.darkside
{
    class DarkFactory : Factory
    {
        public DarkFactory()
        {
            builderFirst.Add(() => new Orc());

            builderSecond.Add(() => new UrukHai());
            builderSecond.Add(() => new Troll());
            builderSecond.Add(() => new Goblin());
        }
    }
}
