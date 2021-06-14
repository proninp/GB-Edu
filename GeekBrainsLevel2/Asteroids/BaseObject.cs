using System;
using System.Collections.Generic;
using System.Drawing;


namespace Asteroids
{
    abstract class BaseObject: ICollision
    {
        public static Random Rand { get; } = new Random();
        static readonly int beyoundLim = 101;
        internal Point Pos;
        internal Point Dir;
        internal Size Size;
        public Rectangle Rect { get; set; }
        protected int lifePoints;
        public virtual int LifePoints
        {
            get { return lifePoints; }
            set { lifePoints = value; }
        }

        public BaseObject() { }
        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
        }
        public BaseObject(Point pos, Point dir, Size size) : this(pos, dir) => Size = size;
        public BaseObject(Point pos, Point dir, int lifePonts) : this(pos, dir) => LifePoints = lifePonts;
        public BaseObject(Point pos, Point dir, int lifiPonts, Size size) : this(pos, dir, lifiPonts) => Size = size;

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < -beyoundLim) Pos.X = Game.Width + beyoundLim - 1;
            if (Pos.X > (Game.Width + beyoundLim)) Pos.X = -beyoundLim + 1;
            if (Pos.Y > Game.Height + beyoundLim) Pos.Y = 0;

            if (Pos.Y < -beyoundLim) Pos.Y = Game.Height;
        }

        public bool Collision(ICollision obj) => ((BaseObject)obj).Rect.IntersectsWith(Rect);
        
        public virtual void Del<T>(List<T> list, int i)
        {
            list[i] = default(T);
            if (i >= 0 && i < list.Count) list.RemoveAt(i);
        }
    }
}
