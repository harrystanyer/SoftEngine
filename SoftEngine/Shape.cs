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
        public Color color { get; set; }
        private List<Pixel> pixels = new List<Pixel>();

        public Shape(List<Vector2> points, Color color)
        {
            this.points = points;
            this.color = color;
        }
        public List<Pixel> OutputList()
        {
            CreateShape();
            return pixels;
        }

        private void CreateShape()
        {
            Line temp = new Line(points[0], points[1]);
            for (int i = 0; i < points.Count - 1; i++)
            {
                temp = new Line(points[i], points[i + 1]);
                pixels.AddRange(vector2ToPixel(temp.OutputList(), color));
            }
            temp = new Line(points[points.Count - 1], points[0]);
            pixels.AddRange(vector2ToPixel(temp.OutputList(), color));
        }

        private List<Pixel> vector2ToPixel(List<Vector2> pointsTemp, Color color)
        {
            List<Pixel> pixel = new List<Pixel>();
            foreach (var point in pointsTemp)
            {
                pixel.Add(new Pixel(point, color));
            }
            return pixel;
        }
    }
}