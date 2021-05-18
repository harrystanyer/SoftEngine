using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftEngine.SoftEngine
{
    public class Vector2
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2()
        {
            x = Zero().x;
            y = Zero().y;
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }
    }
}