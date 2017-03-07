using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSim.Units;

namespace BattleSim
{
    using Pair = Tuple<unitBuilder, int>;
    delegate Unit unitBuilder();

    class UnitFactory
    {
        public List<Pair> builder = new List<Pair>();
        private static UnitFactory inst;
        private int fullweight;

        private UnitFactory()
        {
            //Second argument of pair is weight in distribution

            builder.Add(new Pair(() => new Warrior(),           10));
            builder.Add(new Pair(() => new Archer(),            10));
            builder.Add(new Pair(() => new AttackingTower(),    1));
            builder.Add(new Pair(() => new HealingTower(),      1));
            builder.Add(new Pair(() => new Cavalry(),           4));
            builder.Add(new Pair(() => new CavalryArcher(),     4));
            builder.Add(new Pair(() => new Healer(),            5));

            fullweight = builder.Select(x => x.Item2).Aggregate((a, b) => a + b);
        }

        public static UnitFactory instance()
        {
            if(inst == null){
                inst = new UnitFactory();
            }

            return inst;
        }

        public Unit createUniformRandomUnit()
        {
            if (builder.Count > 0)
            {
                var selected = Utils.getRandBetween(0, builder.Count);
                return builder[selected].Item1();
            }

            return null;
        }

        public Unit createWeightedRandomUnit()
        {
            if (builder.Count > 0)
            {
                var selected = Utils.getRandBetween(1, fullweight);
                double cumsum = 0;
                int counter = 0;

                foreach(var val in builder.Select(x => x.Item2))
                {
                    if(cumsum <= selected && selected <= cumsum + val)
                    {
                        return builder[counter].Item1();
                    }

                    cumsum += val;
                    counter++;
                }
            }

            return null;
        }
    }
}