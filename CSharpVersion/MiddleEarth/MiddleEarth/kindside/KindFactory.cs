using MiddleEarth.metaside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.kindside
{
    class KindFactory : Factory
    {
        public KindFactory()
        {
            builderSingle.Add(() => new Wizard());

            builderFirst.Add(() => new Rohhirim());

            builderSecond.Add(() => new WoodenElf());
            builderSecond.Add(() => new Rohhirim());
            builderSecond.Add(() => new Human());
            builderSecond.Add(() => new Elf());
        }
    }
}
