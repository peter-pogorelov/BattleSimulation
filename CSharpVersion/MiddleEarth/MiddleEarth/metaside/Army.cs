using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.metaside
{
    class Army
    {
        public const int UNITNUMBER_MAX = 2000;
        public const int UNITNUMBER_MIN = 1500;

        private string armyName;
        private Factory factory;

        private List<MiddleEarthCitizen> firstSquad = new List<MiddleEarthCitizen>();
        private List<MiddleEarthCitizen> secondSquad = new List<MiddleEarthCitizen>();

        public Army(string name, Factory fact)
        {
            this.armyName = name;
            this.factory = fact;
        }

        public void generateFirstSquad()
        {
            var armySize = new Random(Guid.NewGuid().GetHashCode()).Next(UNITNUMBER_MIN, UNITNUMBER_MAX);

            bool singleUnitDeployed = false;

            for(int i = 0; i < armySize; i++)
            {
                if(!singleUnitDeployed)
                {
                    var unit = factory.createSingleUnit();

                    if (unit == null)
                    {
                        firstSquad.Add(factory.createFirstArmyUnit());
                    }
                    else {
                        firstSquad.Add(unit as MiddleEarthCitizen);
                    }

                    singleUnitDeployed = true;
                } else
                {
                    firstSquad.Add(factory.createFirstArmyUnit());
                }
            }
        }

        public void generateSecondSquad()
        {
            var armySize = new Random(Guid.NewGuid().GetHashCode()).Next(UNITNUMBER_MIN, UNITNUMBER_MAX);

            for (int i = 0; i < armySize; i++)
            {
                secondSquad.Add(factory.createSecondArmyUnit());
            }
        }

        public bool firstRoundAttack(Army oppo)
        { //Loop through alive units
            int k = -1, e = -1;

            while (getFirstArmyAlives() > 0 && oppo.getFirstArmyAlives() > 0)
            {
                int kw = getNextFAAlive(k);
                if (kw == -1)
                {
                    return false; //defender won
                }

                int ew = oppo.getNextFAAlive(e);
                if (ew == -1)
                {
                    return true; //attacker won
                }

                MiddleEarthCitizen warrior = (MiddleEarthCitizen)getFirstArmy()[kw];
                MiddleEarthCitizen enemy = (MiddleEarthCitizen)oppo.getFirstArmy()[ew];

#if (DEBUG)
                Console.WriteLine(warrior.getName() + " attacked " + enemy.getName() + " with " + Convert.ToString(warrior.getPower()) + " to " + Convert.ToString((enemy.getPower() - warrior.getPower())) + "hp");
#endif
                warrior.applyAttack(enemy);
                if (!enemy.isDead())
                {
#if (DEBUG)
                    Console.WriteLine(enemy.getName() + " attacked " + warrior.getName() + " with " + Convert.ToString(enemy.getPower()) + " to " + Convert.ToString((warrior.getPower() - enemy.getPower())) + "hp");
#endif
                    enemy.applyAttack(warrior);
                }
                else
                {
                    Console.WriteLine(">>>>>>>>>> [" + enemy.getName() + " is not survived!] <<<<<<<<<<");
                }

                k = kw;
                e = ew;
            }

            return true; //will not even be called
        }

        public bool secondRoundAttack(Army oppo)
        {
            int k = -1, e = -1;
            while (getSecondArmyAlives() > 0 && oppo.getSecondArmyAlives() > 0)
            {
                int kw = getNextSAAlive(k);
                if (kw == -1)
                {
                    return false; //defender won
                }

                int ew = oppo.getNextSAAlive(e);
                if (ew == -1)
                {
                    return true; //attacker won
                }

                MiddleEarthCitizen warrior = (MiddleEarthCitizen)getSecondArmy()[kw];
                MiddleEarthCitizen enemy = (MiddleEarthCitizen)oppo.getSecondArmy()[ew];

                warrior.applyAttack(enemy);
                if (!enemy.isDead())
                {
                    enemy.applyAttack(warrior);
                }

                k = kw;
                e = ew;
            }

            return true; //will not even be called
        }

        public bool thirdRoundAttack(Army oppo)
        {
            int fk = -1, fe = -1;
            int sk = -1, se = -1;

            while (getTotalAlives() > 0 && oppo.getTotalAlives() > 0)
            {
                MiddleEarthCitizen warrior;
                MiddleEarthCitizen enemy;

                int fkw = getNextFAAlive(fk);
                if (fkw == -1)
                {
                    int skw = getNextSAAlive(sk);
                    if (skw == -1)
                    {
                        return false;
                    }
                    else {
                        sk = skw;
                        warrior = (MiddleEarthCitizen)getSecondArmy()[sk];
                    }
                }
                else {
                    fk = fkw;
                    warrior = (MiddleEarthCitizen)getFirstArmy()[fk];
                }

                int few = oppo.getNextFAAlive(fe);
                if (few == -1)
                {
                    int sew = oppo.getNextSAAlive(se);
                    if (sew == -1)
                    {
                        return true;
                    }
                    else {
                        se = sew;
                        enemy = (MiddleEarthCitizen)oppo.getSecondArmy()[se];
                    }
                }
                else {
                    fe = few;
                    enemy = (MiddleEarthCitizen)oppo.getFirstArmy()[fe];
                }

                if (enemy is HasBeast) {
                    enemy.applyAttack(warrior);
                    if (!warrior.isDead())
                    {
                        warrior.applyAttack(enemy);
                    }
                } else {
                    warrior.applyAttack(enemy);
                    if (!enemy.isDead())
                    {
                        enemy.applyAttack(warrior);
                    }
                }
            }

            return true; //will not even be called
        }

        public int getNextFAAlive(int cur)
        {
            int total = firstSquad.Count;
            int i = cur + 1;
            while (i < total)
            {
                MiddleEarthCitizen unit = (MiddleEarthCitizen)firstSquad[i];
                if (!unit.isDead())
                    return i;

                ++i;
            }

            i = 0;

            while (i <= cur)
            {
                MiddleEarthCitizen unit = (MiddleEarthCitizen)firstSquad[i];
                if (!unit.isDead())
                    return i;

                ++i;
            }

            return cur;
        }

        public int getNextSAAlive(int cur)
        {
            int total = secondSquad.Count;
            int i = cur + 1;
            while (i < total)
            {
                MiddleEarthCitizen unit = (MiddleEarthCitizen)secondSquad[i];
                if (!unit.isDead())
                    return i;

                ++i;
            }

            i = 0;

            while (i <= cur)
            {
                MiddleEarthCitizen unit = (MiddleEarthCitizen)secondSquad[i];
                if (!unit.isDead())
                    return i;

                ++i;
            }

            return cur;
        }

        public int getFirstArmyAlives()
        {
            int j = 0;
            for (int i = 0; i < firstSquad.Count; i++)
            {
                if (!((MiddleEarthCitizen)firstSquad[i]).isDead())
                {
                    j++;
                }
            }
            return j;
        }

        public int getSecondArmyAlives()
        {
            int j = 0;
            for (int i = 0; i < firstSquad.Count; i++)
            {
                if (!((MiddleEarthCitizen)firstSquad[i]).isDead())
                {
                    j++;
                }
            }
            return j;
        }

        public int getTotalAlives()
        {
            return getFirstArmyAlives() + getSecondArmyAlives();
        }

        public String getArmyName()
        {
            return armyName;
        }

        public List<MiddleEarthCitizen> getFirstArmy()
        {
            return firstSquad;
        }

        public List<MiddleEarthCitizen> getSecondArmy()
        {
            return secondSquad;
        }

        public void setArmyName(String armyName)
        {
            this.armyName = armyName;
        }
    }
}
