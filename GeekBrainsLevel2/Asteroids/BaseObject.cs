using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asteroids
{
    abstract class BaseObject: ICollision
    {
        public static Random Rand { get; } = new Random();

        internal Point Pos;
        internal Point Dir;
        internal Size Size;
        internal int HelthOrPower;
        public Rectangle Rect { get; set; }

        public BaseObject() { }
        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
        }
        public BaseObject(Point pos, Point dir, Size size) : this(pos, dir) => Size = size;
        public BaseObject(Point pos, Point dir, int helthOrPower) : this(pos, dir) => HelthOrPower = helthOrPower;

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public abstract void Update();

        public bool Collision(ICollision obj) => ((BaseObject)obj).Rect.IntersectsWith(Rect);
    }
}
