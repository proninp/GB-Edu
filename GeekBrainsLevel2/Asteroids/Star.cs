using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Star: BaseObject
    {
        int StarSize { get; } = Rand.Next(Settings.StarSizeMin, Settings.StarSizeMax);
        public Star(Point pos, Point dir, Size size): base(pos, dir, size) { }
        public Star(int i)
        {
            Pos = //new Point(Game.Width, Rand.Next(Settings.FieldHeight / (i + 1), Settings.FieldHeight));
                new Point(Rand.Next(0, Settings.FieldWidth), Rand.Next(10, Settings.FieldHeight));
            Dir = //new Point(i % Rand.Next(10, 20) + 1, 0);
                new Point(i % 20 - 1, 0);
            Size = new Size(StarSize, StarSize);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y + Size.Height/2, Pos.X + Size.Width, Pos.Y + Size.Height/2);

        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
