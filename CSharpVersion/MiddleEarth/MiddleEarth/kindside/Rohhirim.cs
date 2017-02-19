using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiddleEarth.metaside;

namespace MiddleEarth.kindside
{
    class Rohhirim : Human, HasBeast, FirstStrikeable
    {
        private Horse horse;
        private bool firstStriked = false;

        public Rohhirim()
        {
            this.horse = new Horse();
        }

        public void applyAttach(MiddleEarthCitizen enemy)
        {
            enemy.setPower(enemy.getPower() - (!firstStriked ? getBeastPower() : 0) - getPower());
            firstStriked = true;
        }

        public void setBeast(Beast beast)
        {
            if (beast is Horse) {
                this.horse = (Horse)beast;
            }
        }

        public Beast getBeast()
        {
            return horse;
        }

        public int getBeastPower()
        {
            return horse.getPower();
        }
    }
}
