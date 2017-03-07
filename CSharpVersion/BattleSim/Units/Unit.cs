using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSim.Units
{
    abstract class Unit
    {
        public const int MAX_INITIATIVE = 7;
        public const int MIN_INITIATIVE = 1;

        private int health;
        private int initiative;
        public int max_health;
        virtual public int Attack { get; set; }
        public int Health
        {
            get { return health; }
            set
            {
                if (value > max_health)
                {
                    health = max_health;
                }
                else if(value < 0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
            }
        }
        public int Initiative
        {
            get { return initiative; }
            set
            {
                if(MAX_INITIATIVE < value)
                {
                    initiative = MAX_INITIATIVE;
                }
                else if(MIN_INITIATIVE > value)
                {
                    initiative = MIN_INITIATIVE;
                } else
                {
                    initiative = value;
                }
            }
        }

        protected Unit(int health, int attack=0, int initiative=MAX_INITIATIVE) 
        {
            this.max_health = this.health = health;
            this.Attack = attack;
            this.Initiative = initiative;
        }

        public void attackUnit(Unit t)
        {
            t.Health -= this.Attack;
        }
    }
}
