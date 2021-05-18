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

        public override void OnLoad()
        {
            BackgroundColour = Color.Black;
        }

        public override void OnDraw()
        {

        }

        public override void OnUpdate()
        {
            addShape();
            addRectangle(new Vector2(100, 100), 100, 100, Color.Green, 0.8);
            addRectangle(new Vector2(100, 100), 100, 100, Color.Green, -0.8);
            addRectangle(new Vector2(100, 100), 100, 100, Color.Green, 2.4);
            addRectangle(new Vector2(100, 100), 100, 100, Color.Green, -2.4);
        }
    }
}