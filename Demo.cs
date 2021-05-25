using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftEngine.SoftEngine;
using System.Drawing;

namespace SoftEngine
{
    class Demo : SoftEngine.SoftEngine
    {
        public Demo() : base(new Vector2(615, 515), "Test123") { }
        public Shape sock;
        public override void OnLoad()
        {
            Console.WriteLine("onLoad");
            BackgroundColour = Color.Black;
            List<Vector2> temp = new List<Vector2>();
            temp.Add(new Vector2(100, 100));
            temp.Add(new Vector2(100, 200));
            temp.Add(new Vector2(200, 200));
            temp.Add(new Vector2(200, 100));
            sock = new Shape(temp, Color.Red, 0, new Vector2());
        }

        public override void OnDraw()
        {
            //shape.moveShape(1, 1);
        }

        public override void OnUpdate()
        {
            
            clearPixels();
            addShape(sock);//this is much less stable for some reason//creating a shape in here could be problem?
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 45);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 10);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 20);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 30);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 40);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 50);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 60);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 70);
            //addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 80);
        }
    }
}