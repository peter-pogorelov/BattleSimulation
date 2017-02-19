using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleEarth.metaside;

namespace MiddleEarth
{
    class Battle
    {
        private static TextWriter oldOut = Console.Out;
        private static FileStream ostrm;
        private static StreamWriter writer;

        private Army kindArmy;
        private Army evilArmy;

        public Battle()
        {
            kindArmy = new Army("Kind", new kindside.KindFactory());
            evilArmy = new Army("Evil", new darkside.DarkFactory());
        }

        public static void SetOutputSettings(string name)
        {
            try
            {
                ostrm = new FileStream("./" + name, FileMode.OpenOrCreate, FileAccess.Write);
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

        public static void ReleaseOutputSettings() {
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }

        public void fillKindArmy()
        {
            kindArmy.generateFirstSquad();
            kindArmy.generateSecondSquad();
        }

        public void fillEvilArmy()
        {
            evilArmy.generateFirstSquad();
            evilArmy.generateSecondSquad();
        }

        public Army getKindArmy()
        {
            return kindArmy;
        }

        public Army getEvilArmy()
        {
            return evilArmy;
        }

        public void setEvilArmy(Army evilArmy)
        {
            this.evilArmy = evilArmy;
        }

        public void setKindArmy(Army kindArmy)
        {
            this.kindArmy = kindArmy;
        }

        static void Main(string[] args)
        {
            SetOutputSettings("battle.log");

            Battle battle = new Battle();

            battle.fillEvilArmy();
            battle.fillKindArmy();

            Army attacker;
            Army defender;

            var randValue = new Random(Guid.NewGuid().GetHashCode()).Next(0, 2);

#if (DEBUG)
            Console.WriteLine("Random value seed is " + Guid.NewGuid().GetHashCode());
            Console.WriteLine("Random value is " + randValue);
#endif

            if (randValue == 0) 
            {
                Console.WriteLine("Evil side is attacker!");
                attacker = battle.getEvilArmy();
                defender = battle.getKindArmy();
            }
            else {
                Console.WriteLine("Kind side is attacker!");
                attacker = battle.getKindArmy();
                defender = battle.getEvilArmy();
            }

            Console.WriteLine("The battle is beginig!");
            Console.WriteLine("Attacker is " + attacker.getArmyName());
            Console.WriteLine("Defender is " + defender.getArmyName());
            Console.WriteLine(attacker.getArmyName() + " has " + attacker.getTotalAlives() + " units.");
            Console.WriteLine(defender.getArmyName() + " has " + defender.getTotalAlives() + " units.");

            Console.WriteLine("##########################First round Attack!##########################");
            attacker.firstRoundAttack(defender);
            Console.WriteLine("##########################Second round Attack!##########################");
            attacker.secondRoundAttack(defender);

            if(attacker.getTotalAlives() == 0)
            {
                Console.WriteLine(defender.getArmyName() + " has won epic battle at second round!");
            }
            else if(defender.getTotalAlives() == 0)
            {
                Console.WriteLine(attacker.getArmyName() + " has won epic battle at second round!");
            } else
            {
                Console.WriteLine("##########################Third round Attack!##########################");

                if (attacker.thirdRoundAttack(defender))
                {
                    Console.WriteLine(attacker.getArmyName() + " has won epic battle at third round!");
                }
                else {
                    Console.WriteLine(defender.getArmyName() + " has won epic battle at third round!");
                }
            }

            Console.WriteLine(attacker.getArmyName() + " " + attacker.getTotalAlives());
            Console.WriteLine(defender.getArmyName() + " " + defender.getTotalAlives());

            ReleaseOutputSettings();
        }
    }
}
