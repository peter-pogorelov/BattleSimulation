using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using BattleSim.Units;

namespace BattleSim
{
    using Army = List<Unit>;
    using NamedArmy = Tuple<string, List<Unit>>;

    class Battle
    {
        private static TextWriter oldOut = Console.Out;
        private static FileStream ostrm;
        private static StreamWriter writer;

        public const int ARMY_SIZE = 50;
        public const int ARMY_COUNT = 5;
        public const int MAX_ITERATIONS = 10000;

        public static void AttackArmy(Army a, Army b)
        {
            foreach(var group in a.OrderBy(x => -x.Initiative).GroupBy(x => x.Initiative))
            {
                Console.WriteLine("-Units with initiative " + group.Key.ToString() + " are attacking");
                
                foreach (var unit in group)
                {
                    if(!(unit is HealingUnit))
                    {
                        var victim = Utils.getRandomUnit(b);

                        if(victim == null){
                            // Victim is dead
                            return;
                        }
                        Console.WriteLine("--Unit" + unit.GetType().Name + " is attacking " + victim.GetType().Name);
                        unit.attackUnit(victim);
                    }
                    else
                    {
                        Console.WriteLine("--Unit" + unit.GetType().Name + " is healing friendly units.");
                        (unit as HealingUnit).heal(a);
                    }
                }
            }
        }

        public static void PrintArmy(NamedArmy army)
        {
            Console.WriteLine("-Army name is " + army.Item1);
            Console.WriteLine("-Army content is:");
            int counter = 0;
            foreach(Unit t in army.Item2)
            {
                if(counter % 7 == 0)
                {
                    Console.WriteLine();
                    Console.Write("--");
                }

                Console.Write(t.GetType().Name + " ");
                counter++;
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            SetOutputSettings("battle.log");
            List<NamedArmy> armies = new List<NamedArmy>();

            for(int i = 0; i < ARMY_COUNT; i++)
            {
                armies.Add(new NamedArmy("Army " + i, new Army()));

                for (int j = 0; j < ARMY_SIZE; j++)
                {
                    armies[i].Item2.Add(UnitFactory.instance().createWeightedRandomUnit());
                }
            }

#if (DEBUG)
            Console.WriteLine(armies.Count + " armies has been generated.");
            foreach(var army in armies)
            {
                PrintArmy(army);
            }
#endif

#if (DEBUG)
            Console.WriteLine("#########################################################################");
            Console.WriteLine("############################BATTLE#STARTED###############################");
            Console.WriteLine("#########################################################################");
#endif

            int inter_counter = 0;
            do
            {
                for (int i = 0; i < armies.Count; i++)
                {
                    NamedArmy a = armies[i];
                    NamedArmy b = armies[i + 1 >= armies.Count ? 0 : i + 1];

#if (DEBUG)
                    Console.WriteLine(a.Item1 + " is attacking " + b.Item1);
#endif
                    AttackArmy(a.Item2, b.Item2);

                    if (Utils.isArmyAlive(b.Item2))
                    {
#if (DEBUG)
                        Console.WriteLine(b.Item1 + " is alive and want payback");
#endif
                        AttackArmy(b.Item2, a.Item2);
                    }
                    else
                    {
#if (DEBUG)
                        Console.WriteLine(b.Item1 + " is dead now and removed from pool");
#endif
                        armies.Remove(b);
                    }

                    if(armies.Count == 0)
                    {
                        break;
                    }
                }

                inter_counter++;
            } while (armies.Count != 1 && inter_counter < MAX_ITERATIONS);

#if (DEBUG)
            if (inter_counter < MAX_ITERATIONS)
                Console.WriteLine(armies[0].Item1 + " has won!");
            else
                Console.WriteLine("Battle has been aborted due to iteration limit alert");
#endif
            
            ReleaseOutputSettings();
        }

        public static void SetOutputSettings(string name)
        {
            try
            {
                ostrm = new FileStream("./" + name, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open " + name + " for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
        }

        public static void ReleaseOutputSettings()
        {
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }
    }
}
