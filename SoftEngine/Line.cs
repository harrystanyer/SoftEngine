using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SoftEngine.SoftEngine
{
    class Line
    {
        public Vector2 point1 { get; set; }
        public Vector2 point2 { get; set; }
        private List<Vector2> points = new List<Vector2>();
        public Line(Vector2 point1, Vector2 point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        public List<Vector2> OutputList()
        {
            CreateLine();
            return points;
        }

        public void CreateLine()//Bresenham algorithm
        {
            int x = point1.x;
            int y = point1.y;
            int w = point2.x - point1.x;
            int h = point2.y - point1.y;
            int dx1 = 0;
            int dy1 = 0;
            int dx2 = 0;
            int dy2 = 0;
            if (w < 0)
            {
                dx1 = -1;
            }
            else if (w > 0)
            {
                dx1 = 1;
            }
            if (h < 0)
            {
                dy1 = -1;
            }
            else if (h > 0)
            {
                dy1 = 1;
            }
            if (w < 0)
            {
                dx2 = -1;
            }
            else if (w > 0)
            {
                dx2 = 1;
            }
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0)
                {
                    dy2 = -1;
                }
                else if (h > 0)
                {
                    dy2 = 1;
                }
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                points.Add(new Vector2(x, y));
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}