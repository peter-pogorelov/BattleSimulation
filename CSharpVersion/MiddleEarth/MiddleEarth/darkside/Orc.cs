using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleEarth.metaside;

namespace MiddleEarth.darkside
{
    class Orc : AbstractOrc, FirstStrikeable, HasBeast
    {
        private Wolf wolf;
        private bool firstStriked = false;

        public Orc() : base("Orc")
        {
            this.wolf = new Wolf();
        }

        public void setBeast(Beast beast)
        {
            if (beast is Wolf) {
                this.wolf = (Wolf)beast;
            }
        }

        public Beast getBeast()
        {
            return this.wolf;
        }

        public int getBeastPower()
        {
            return this.wolf.getPower();
        }

        public new void applyAttack(MiddleEarthCitizen enemy)
        {
            enemy.setPower(enemy.getPower() - (!firstStriked ? getBeastPower() : 0) - getPower());
            firstStriked = true;
        }
    }
}
