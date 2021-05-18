using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SoftEngine.SoftEngine
{
    class Shape//Make sure input is in clockwise or anti clockwise
    {
        public List<Vector2> points = new List<Vector2>();
        public Vector2 point1 { get; set; }
        public Vector2 point2 { get; set; }
        public Vector2 point3 { get; set; }
        public Vector2 point4 { get; set; }
        public Color color { get; set; }
        private List<Pixel> pixels = new List<Pixel>();
        public Shape(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4, Color color)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.point4 = point4;
            this.color = color;
        }

        public Shape(List<Vector2> points)
        {
            this.points = points;
        }
        public List<Pixel> OutputList()
        {
            CreateShape();
            return pixels;
        }

        void CreateShape()
        {
            Line temp = new Line(points[0], points[1]);
            for (int i = 0; i < points.Count - 1; i++)
            {
                temp = new Line(points[i], points[i + 1]);
                pixels.AddRange(temp.OutputList());
            }
            temp = new Line(points[points.Count - 1], points[0]);
            pixels.AddRange(temp.OutputList());
        }
    }
}