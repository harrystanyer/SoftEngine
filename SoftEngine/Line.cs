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
        private List<Pixel> points = new List<Pixel>();
        public Line(Vector2 point1, Vector2 point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        public List<Pixel> OutputList()
        {
            CreateLine();
            return points;
        }
        void CreateLine()
        {
            //Find line equation
            //Find all whole number coordiantes in that range // round to nearest
            //y = mx+c
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!grad needs to be float/decimal but broken atm
            int tempX = 0;
            int tempY = 0;
            float c = ((float)point2.y - (float)point1.y) / ((float)point2.x - (float)point1.x);
            Console.WriteLine($"c = {point2.y} - {point1.y} / {point2.x} - {point1.x} ");
            float m = ((float)point1.y - c) / (float)point1.x;
            int lowestY = point1.y;
            int lowestX = point1.x;
            if (point2.y < point1.y)
            {
                lowestY = point2.y;
            }
            if (point2.x < point1.x)
            {
                lowestX = point2.x; 
            }
            Console.WriteLine($"point1:{point1.x},{point1.y}");
            Console.WriteLine($"point2:{point2.x},{point2.y}");
            Console.WriteLine($"grad:{c}");
            for (int i = lowestY; i < Math.Abs(point2.y-point1.y); i++)
            {
                Console.WriteLine("happening");
                tempY = i;
                tempX = (tempY - (int)Math.Round(c,0)) / (int)Math.Round(m,0);
                points.Add(new Pixel(new Vector2(tempX,tempY), Color.White));
                
            }
        }
    }
}