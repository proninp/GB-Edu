using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] asteroids;
        public static BaseObject[] stars;
        static Image Background = Properties.Resources.background;
        static Game()
        {

        }
        public static void Init(Form form)
        {
            context = BufferedGraphicsManager.Current;
            Graphics graphics = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawImage(Background, new Point(0, 0));
            foreach(var el in asteroids) el.Draw();
            foreach (var el in stars) el.Draw();
            Buffer.Render();
        }
        public static void Load()
        {
            asteroids = new Asteroid[Settings.AsteroidsCount];
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i] = new Asteroid();
            }
            stars = new BaseObject[Settings.StarsCount];
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(i);
            }

        }
        public static void Update()
        {
            foreach (var el in asteroids) el.Update();
            foreach (var el in stars) el.Update();
        }

    }
}
