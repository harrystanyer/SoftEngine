using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SoftEngine.SoftEngine
{
    public class Pixel
    {
        public Vector2 Position { get; set; }
        public Color color { get; set; }

        public Pixel()
        {
            this.Position = new Vector2(0, 0);
            this.color = Color.White;
        }

        public Pixel(Vector2 Position, Color color)
        {
            this.Position = Position;
            this.color = color;
        }
    }
}
