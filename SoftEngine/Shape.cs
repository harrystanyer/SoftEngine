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
        private List<Pixel> pixelsBuffer = new List<Pixel>();

        public Shape(List<Vector2> points, Color color)
        {
            this.points = points;
            this.color = color;
        }

        public Shape(List<Vector2> points, Color color, int angle, Vector2 rotationPoint)
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
            shapeCentre();
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

        private void shapeCentre()
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
        public void fillShape(Vector2 startPoint)//allows individual points to be morphed
        {
            //would be everything inside of the lines created
            //foreach point on line a draw a line to corrosponding point on line b // do for all lines
            //flood fill // quick fill // check area of shape to make more efficient?
            /*
             find left edge
             add points from start to left de-increment points counter
             find right edge
             add points from start to right de-increment points counter
             start the loop upwards and downwards if not out of the area

             */
            startPoint = new Vector2();//find a starting point that is inside of the shape
            int LeftRemaining = DistanceToLeft(startPoint);
            int rightRemaining = DistanceToRight(startPoint);
            for (int i = 0; i < LeftRemaining; i++)
            {
                pixelsBuffer.Add(new Pixel(new Vector2(startPoint.x - i, startPoint.y), color));
            }
            for (int i = 0; i < rightRemaining; i++)
            {
                pixelsBuffer.Add(new Pixel(new Vector2(startPoint.x + i, startPoint.y), color));
            }
            if (CheckPixel(new Vector2(startPoint.x, startPoint.y + 1)))
            {
                fillShape(new Vector2(startPoint.x, startPoint.y + 1));
            }
            if (CheckPixel(new Vector2(startPoint.x, startPoint.y - 1)))
            {
                fillShape(new Vector2(startPoint.x, startPoint.y - 1));
            }
        }
        private bool CheckPixel(Vector2 point)
        {//ray casting
            int counter = 0;
            foreach (var pixel in pixels)
            {
                if (pixel.Position.x == point.x)
                {
                    counter++;
                }
            }
            if (counter % 2 == 1)
            {
                return true;
            }
            return false;
        }
        private int DistanceToLeft(Vector2 point)
        {
            foreach (var pixel in pixels)
            {
                if (pixel.Position.y == point.y && pixel.Position.x < point.x)
                {
                    return point.x - pixel.Position.x;
                }
            }
            return 0;
        }
        private int DistanceToRight(Vector2 point)
        {
            foreach (var pixel in pixels)
            {
                if (pixel.Position.y == point.y && pixel.Position.x > point.x)
                {
                    return pixel.Position.x - point.x;
                }
            }
            return -1;
        }
        public void scale(double scale)
        {

        }
    }
}

//look at creating more natural generated shapes
//branch off softengine for each of the ideas or create this as package and import into new repos