using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SoftEngine.SoftEngine
{
    class Shape
    {
        public Vector2 point1 { get; set; }
        public Vector2 point2 { get; set; }
        public Vector2 point3 { get; set; }
        public Vector2 point4 { get; set; }
        public Color color { get; set; }
        private List<Pixel> pixels = new List<Pixel>();
        public Shape(Vector2 point1,Vector2 point2,Vector2 point3, Vector2 point4, Color color)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.point4 = point4;
            this.color = color;
        }
        public List<Pixel> OutputList()
        {
            CreateShape();
            return pixels;
        }
        void CreateShape()
        {
            Line l1 = new Line(point1, point2);
            Line l2 = new Line(point2, point3);
            Line l3 = new Line(point3, point4);
            Line l4 = new Line(point4, point1);
            pixels.AddRange(l1.OutputList());
            pixels.AddRange(l2.OutputList());
            pixels.AddRange(l3.OutputList());
            pixels.AddRange(l4.OutputList());
        }
    }
}