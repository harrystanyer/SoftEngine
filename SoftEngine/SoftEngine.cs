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
            Console.WriteLine(pixels.Count);
            Console.WriteLine(pixels.ToList().Count);
            Graphics g = e.Graphics;

            g.Clear(BackgroundColour);

            foreach (var pixel in pixels.ToList())
            {
                if (pixel.Position.x < 512 && pixel.Position.y < 512)
                {
                    g.FillRectangle(new SolidBrush(pixel.color), pixel.Position.x, pixel.Position.y, 1, 1);
                }
            }

            pixels.Clear();
        }

        public void addShape()
        {
            pixels.Clear();
            List<Vector2> temp = new List<Vector2>();
            temp.Add(new Vector2(110, 110));
            temp.Add(new Vector2(110, 120));
            temp.Add(new Vector2(120, 130));
            temp.Add(new Vector2(130, 120));
            temp.Add(new Vector2(130, 110));
            temp.Add(new Vector2(120, 90));

            pixels.AddRange(new Shape(temp, Color.Green).OutputList());

            pixels.AddRange(new Shape(rotateShape(temp, new Vector2(100, 100), 3.14 / 2), Color.Red).OutputList());
            pixels.AddRange(new Shape(rotateShape(temp, new Vector2(100, 300), 3.14), Color.Blue).OutputList());
            pixels.AddRange(new Shape(rotateShape(temp, new Vector2(200, 300), -3.14 / 2), Color.Yellow).OutputList());
        }

        public List<Vector2> rotateShape(List<Vector2> points, Vector2 rotationPoint, double angle)
        {
            List<Vector2> outputList = new List<Vector2>();
            double s = Math.Sin(angle);
            double c = Math.Cos(angle);
            double xNew = 0;
            double yNew = 0;
            foreach (var point in points)
            {
                point.x -= rotationPoint.x;
                point.y -= rotationPoint.y;
                xNew = point.x * c - point.y * s;
                yNew = point.x * s + point.y * c;
                outputList.Add(new Vector2((int)Math.Round(xNew + rotationPoint.x, 0), (int)Math.Round(yNew + rotationPoint.y, 0)));
            }
            return outputList;
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
