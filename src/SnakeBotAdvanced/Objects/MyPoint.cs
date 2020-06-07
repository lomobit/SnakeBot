using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeBotAdvanced.Objects
{
    public class MyPoint : List<int>
    {
        public MyPoint()
        {
        }
        public MyPoint(int x, int y)
        {
            this.Add(x);
            this.Add(y);
        }
        public int X => this[0];
        public int Y => this[1];

        public override bool Equals(object obj)
        {
            if (obj is MyPoint)
            {
                return ((MyPoint)obj).X == this.X && ((MyPoint)obj).Y == this.Y;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
