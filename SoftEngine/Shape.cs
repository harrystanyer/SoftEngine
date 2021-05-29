using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SoftEngine.SoftEngine
{
    public class Shape//Make sure input is in clockwise or anti clockwise
    {
        public List<Vector2> points = new List<Vector2>();
        public Color color { get; set; }
        public int angle = 0;
        public Vector2 rotationPoint = new Vector2();

        private List<Pixel> pixels = new List<Pixel>();

        public Shape(List<Vector2> points, Color color)
        {
            this.points = points;
            this.color = color;
        }

        public Shape(List<Vector2> points, Color color, int angle, Vector2 rotationPoint)//if rotationPoint is null then rotate around centre of shape
        {
            this.points = points;
            this.color = color;
            this.angle = angle;
            this.rotationPoint = rotationPoint;
        }

        public List<Pixel> OutputList()
        {
            CreateShape();
            return pixels;
        }

        private void CreateShape()
        {
            if (angle != 0)
            {
                points = rotateShape();
            }
            Line temp = new Line(null, null);
            for (int i = 0; i < points.Count - 1; i++)//for each of the points added create a line
            {
                temp = new Line(points[i], points[i + 1]);
                pixels.AddRange(vector2ToPixel(temp.OutputList(), color));//adds line pixels to pixels list
            }
            temp = new Line(points[points.Count - 1], points[0]);//last point and first point
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

        public List<Vector2> rotateShape()
        {
            if (rotationPoint.x == 0 && rotationPoint.y == 0)//if empty rotationPoint then gets the centre of the shape
            {
                foreach (var point in points)
                {
                    rotationPoint.x += point.x;
                    rotationPoint.y += point.y;
                }
                rotationPoint.x /= points.Count;
                rotationPoint.y /= points.Count;
            }
            List<Vector2> outputList = new List<Vector2>();
            double s = Math.Sin(ConvertToRadians(angle));
            double c = Math.Cos(ConvertToRadians(angle));
            double xNew = 0;
            double yNew = 0;
            foreach (var point in points)
            {
                point.x -= rotationPoint.x;
                point.y -= rotationPoint.y;
                xNew = point.x * c - point.y * s;
                yNew = point.x * s + point.y * c;
                outputList.Add(new Vector2((int)Math.Round(xNew + rotationPoint.x, 0), (int)Math.Round(yNew + rotationPoint.y, 0)));
                point.x += rotationPoint.x;
                point.y += rotationPoint.y;
            }
            rotationPoint.x = 0;
            rotationPoint.y = 0;
            return outputList;
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public void moveShape(int x, int y)
        {
            foreach (var point in points)
            {
                point.x += x;
                point.y += y;
            }
        }
        public void skewShape()//allows individual points to be morphed
        {

        }
    }
}