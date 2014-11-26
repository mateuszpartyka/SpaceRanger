using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRanger
{
    class SpaceRanger
    {
        public int spaceRangerLifes;
        
        public SpaceRanger()
        {
            spaceRangerLifes = 3;
        }

        public void OnDeath()
        {
            spaceRangerLifes--;
        }

        public void fieldPromotion()
        {
            spaceRangerLifes++;
        }
    }
}
