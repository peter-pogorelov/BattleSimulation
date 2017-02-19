using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleEarth.metaside
{
    interface HasBeast
    {
        void setBeast(Beast beast);
        Beast getBeast();
        int getBeastPower();
    }
}
