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
        public Shape shape1;
        public int counter = 0;
        public override void OnLoad()
        {
            BackgroundColour = Color.Black;
            List<Vector2> temp = new List<Vector2>();
            temp.Add(new Vector2(100, 100));
            temp.Add(new Vector2(100, 200));
            temp.Add(new Vector2(200, 200));
            temp.Add(new Vector2(200, 100));
            shape1 = new Shape(temp, Color.Red, 0, new Vector2());
        }

        public override void OnDraw()
        {
            shape1.moveShape(1, 0);
            shape1.angle = counter;
            counter++;
        }

        public override void OnUpdate()
        {
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, counter);
            AddShape(shape1);
        }

        private void AddShape(Shape shape)
        {
            addRectangle(shape1.points[0], 100, 100, shape.color, shape.angle);
        }
    }
}