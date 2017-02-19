using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleEarth.metaside;

namespace MiddleEarth.kindside
{
    class Wizard : MiddleEarthCitizen, HasBeast, FirstStrikeable, SingleUnit
    {
        private Horse horse;
        private bool firstStriked = false;

        public Wizard() : base("Wizard", 20)
        {
            horse = new Horse();
        }

        public void setBeast(Beast beast)
        {
            if (beast is Horse)
        {
                this.horse = (Horse)beast;
            }
        }

        public Beast getBeast()
        {
            return this.horse;
        }

        public int getBeastPower()
        {
            return this.horse.getPower();
        }

        public new void applyAttack(MiddleEarthCitizen enemy)
        {
            enemy.setPower(enemy.getPower() - (!firstStriked ? getBeastPower() : 0) - getPower());
            firstStriked = true;
        }
    }
}
