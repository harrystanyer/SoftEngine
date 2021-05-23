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
            clearPixels();
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 0);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 10);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 20);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 30);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 40);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 50);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 60);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 70);
            addRectangle(new Vector2(100, 100), 150, 150, Color.Green, 80);
        }
    }
}