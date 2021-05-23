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

            foreach (var pixel in pixels.ToList())
            {
                if (pixel.Position.x < 512 && pixel.Position.y < 512)//Only draws if in dimensions of the screen
                {
                    g.FillRectangle(new SolidBrush(pixel.color), pixel.Position.x, pixel.Position.y, 1, 1);
                }
            }

            pixels.Clear();
        }

        public void clearPixels()
        {
            pixels.Clear();
        }

        public void addShape()//create a base method for creaing shapes with switch for other shapes
        {
            
        }

        public void addRectangle(Vector2 origin, int width, int height, Color color, double rotationRad)//check where the width and heights need to go on this.
        {
            List<Vector2> temp = new List<Vector2>();
            temp.Add(new Vector2(origin.x, origin.y));
            temp.Add(new Vector2(origin.x + width, origin.y));
            temp.Add(new Vector2(origin.x + width, origin.y + height));
            temp.Add(new Vector2(origin.x, origin.y + height));
            pixels.AddRange(new Shape(rotateShape(temp, new Vector2(origin.x + width/2, origin.y + height/2), rotationRad), color).OutputList());
        }

        public List<Vector2> rotateShape(List<Vector2> points, Vector2 rotationPoint, double angle)
        {
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
            }
            return outputList;
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
