using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.metaside
{
    class MiddleEarthCitizen
    {
        private int power;
        private bool dead;
        private string name;

        public MiddleEarthCitizen(string name, int minPower, int maxPower)
        {
            this.name = name;

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            this.power = rand.Next(minPower, maxPower);
            this.dead = false;
        }

        public MiddleEarthCitizen(string name, int power)
        {
            this.name = name;
            this.power = power;
        }

        public void applyAttack(MiddleEarthCitizen enemy)
        {
            enemy.setPower(enemy.getPower() - getPower());
        }

        public string getName()
        {
            return this.name;
        }

        public int getPower()
        {
            return power;
        }

        public void setPower(int power)
        {
            this.power = power;
            this.setDead(this.power <= 0);
        }

        public bool isDead()
        {
            return dead;
        }

        public void setDead(bool dead)
        {
            this.dead = dead;
        }
    }
}
