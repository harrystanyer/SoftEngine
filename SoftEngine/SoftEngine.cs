using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace SoftEngine.SoftEngine//look at making another pixels list and compare with the current then only display the pixels that have changed//might be more efficient
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
        public static int screenWidth = 512;
        public static int screenHeight = 512;
        private Vector2 ScreenSize = new Vector2(screenWidth, screenHeight);
        private string Title = "SoftEngine";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public List<Pixel> pixels = new List<Pixel>();

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
                if (pixel.Position.x < screenWidth || pixel.Position.y < screenHeight)//Only draws if in dimensions of the screen
                {
                    g.FillRectangle(new SolidBrush(pixel.color), pixel.Position.x, pixel.Position.y, 1, 1);
                }
            }

            clearPixels();
        }

        public void clearPixels()
        {
            pixels.Clear();
        }

        public void addShape(Shape shape)
        {
            pixels.AddRange(new Shape(shape.points, shape.color, shape.angle, shape.rotationPoint).OutputList());
        }

        public void addRectangle(Vector2 origin, int width, int height, Color color, int angle)//adds rectangle of specific height and width at certain point
        {
            List<Vector2> pointsList = new List<Vector2>();
            pointsList.Add(new Vector2(origin.x, origin.y));
            pointsList.Add(new Vector2(origin.x + width, origin.y));
            pointsList.Add(new Vector2(origin.x + width, origin.y + height));
            pointsList.Add(new Vector2(origin.x, origin.y + height));
            pixels.AddRange(new Shape(pointsList, color, angle, new Vector2()).OutputList());
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
