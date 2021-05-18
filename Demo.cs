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
            addShape();
            BackgroundColour = Color.Black;
        }

        public override void OnDraw()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
