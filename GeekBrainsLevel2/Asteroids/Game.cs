using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static List<BaseObject> Asteroids { get; set; }
        public static List<Bullet> PlayerBullets { get; set; }
        public static List<Explode> Explodes { get; set; }
        public static BaseObject[] Starts { get; set; }
        static Image Background = Properties.Resources.background;
        static Game()
        {

        }
        public static void Init(Form form)
        {
            if (form.Height > 1000 || form.Width > 1000) throw new ArgumentOutOfRangeException("Form size");
            context = BufferedGraphicsManager.Current;
            Graphics graphics = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = Settings.ScreenUpdateFrequency };
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
            CheckCollisions();
            foreach (var el in Asteroids) el.Draw();
            foreach (var el in Starts) el.Draw();
            foreach (var el in PlayerBullets) el.Draw();
            foreach (var el in Explodes) el.Draw();
            Buffer.Render();
        }
        public static void Load()
        {
            Asteroids = new List<BaseObject>(Settings.AsteroidsCount);
            for (int i = 0; i < Settings.AsteroidsCount; i++)
                Asteroids.Add(new Asteroid());
            Starts = new BaseObject[Settings.StarsCount];
            for (int i = 0; i < Starts.Length; i++)
                Starts[i] = new Star(i);
            PlayerBullets = new List<Bullet>();
            PlayerBullets.Add(new Bullet(new Point(50, 300), new Point(Settings.PlayerBulletSpeed, 0), Settings.PlayerBulletPower));
            Explodes = new List<Explode>();
        }
        public static void Update()
        {
            foreach (var el in Asteroids) el.Update();
            foreach (var el in Starts) el.Update();
            foreach (var el in PlayerBullets) el.Update();
            for (int i = 0; i < Explodes.Count; i++)
            {
                Explodes[i].Update();
                if (Explodes[i].VisabilityTicksCount < 1)
                {
                    Explodes[i].Pos.X = Settings.FieldWidth;
                    Explodes[i] = default(Explode);
                    Explodes.RemoveAt(i);
                }
                
            }
        }
        static void CheckCollisions()
        {
            for (int i = 0; i < Asteroids.Count; i++)
                for (int j = 0; j < PlayerBullets.Count; j++)
                    if (Asteroids[i].Collision(PlayerBullets[j]))
                        Explodes.Add(new Explode(Asteroids[i].Pos));
        }

    }
}
