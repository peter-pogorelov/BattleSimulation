using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSim.Units;

namespace BattleSim
{
    class Utils
    {
        public static Unit getRandomUnit(List<Unit> units)
        {
            var slice = units.Where(x => x.Health > 0).ToList();
            if (slice.Count > 0)
            {
                int index = getRandBetween(0, slice.Count);

                return slice[index];
            }

            return null;
        }

        public static Unit getDamagedAliveUnit(List<Unit> units)
        {
            var slice = units.Where(x => x.Health > 0 && x.Health < x.max_health).ToList();
            if (slice.Count > 0)
            {
                int index = getRandBetween(0, slice.Count);

                return slice[index];
            }

            return null;
        }

        public static bool getRandomBool()
        {
            return Convert.ToBoolean(new Random(Guid.NewGuid().GetHashCode()).Next(2));
        }

        public static int getRandBetween(int min, int max)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(min, max);
        }

        public static bool isArmyAlive(List<Unit> army)
        {
            return army.Count(x => x.Health > 0) != 0;
        }
    }
}
