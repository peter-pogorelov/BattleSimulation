using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.metaside
{
    delegate MiddleEarthCitizen unitBuilder();
    delegate SingleUnit singleBuilder();

    abstract class Factory
    {
        public List<singleBuilder> builderSingle = new List<singleBuilder>();
        public List<unitBuilder> builderFirst = new List<unitBuilder>();
        public List<unitBuilder> builderSecond = new List<unitBuilder>();

        public SingleUnit createSingleUnit()
        {
            if (builderSingle.Count > 0)
            {
                var selected = new Random().Next(0, builderSingle.Count);
                return builderSingle[selected]();
            }

            return null;
        }

        public MiddleEarthCitizen createSecondArmyUnit()
        {
            if (builderSecond.Count > 0)
            {
                var selected = new Random().Next(0, builderSecond.Count);
                return builderSecond[selected]();
            }

            return null;
        }

        public MiddleEarthCitizen createFirstArmyUnit()
        {
            if (builderFirst.Count > 0)
            {
                var selected = new Random().Next(0, builderFirst.Count);
                return builderFirst[selected]();
            }

            return null;
        }
    }
}
