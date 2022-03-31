using System;
using System.Collections.Generic;
using System.Text;

namespace Reversi
{
    class Pair
    {
        public int x;  //Either x position of a chip, or dx for a direction
        public int y;  //Either y position of a chip, or dy for a direction

        public Pair(int dx, int dy)
        {
            this.x = dx;
            this.y = dy;
        }
    }
}
