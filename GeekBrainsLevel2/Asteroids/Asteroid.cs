﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid: BaseObject
    {
        int SpeedX { get; } = Rand.Next(Settings.AsteroidMinSpeedX, Settings.AsteroidMaxSpeedX);
        int SpeedY { get; } = Rand.Next(Settings.AsteroidMinSpeedY, Settings.AsteroidMaxSpeedY);
        public static Image[] Images { get; set; } = new Image[4]
        {
            Properties.Resources.ast_0,
            Properties.Resources.ast_1,
            Properties.Resources.ast_2,
            Properties.Resources.ast_3
        };
        int ImageIndex { get; } = Rand.Next(0, Images.Length);
        public Asteroid(Point pos, Point dir, Size size): base(pos, dir, size) { }
        public Asteroid()
        {
            Pos = new Point(Rand.Next(Game.Width - 100, Game.Width), Rand.Next(0, Game.Height));
            Dir = new Point(SpeedX, SpeedY);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Images[ImageIndex], Pos);
        }
        public override void Update()
        {
            Pos.X -= Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0 - Images[ImageIndex].Size.Width) Pos.X = Game.Width;
            if (Pos.Y < 0 - Images[ImageIndex].Size.Height) Pos.Y = Game.Height;
            if (Pos.Y > Game.Height) Pos.Y = 0;
        }
    }
}
