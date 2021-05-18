using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace SoftEngine.SoftEngine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    public abstract class SoftEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "SoftEngine";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public static List<Pixel> pixels = new List<Pixel>();

        public Color BackgroundColour = Color.Beige;
        public SoftEngine(Vector2 ScreenSize, string Title)
        {
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.x, (int)this.ScreenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch (Exception)
                {
                    Console.WriteLine("Engine loading..");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackgroundColour);

            foreach (var pixel in pixels)
            {
                g.FillRectangle(new SolidBrush(pixel.color),pixel.Position.x,pixel.Position.y,1,1);
            }
        }

        public void addShape()
        {
            Shape newShape = new Shape(new Vector2(10, 10), new Vector2(10,20), new Vector2(20,10), new Vector2(20,20), Color.Green);
            pixels.AddRange(newShape.OutputList());
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
