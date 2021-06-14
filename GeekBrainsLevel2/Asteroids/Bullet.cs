using System;
using System.Drawing;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        internal static Image[] Images { get; set; } = new Image[2]
        {
            Properties.Resources.ray,
            Properties.Resources.redRay
        };
        internal static Image PlayerBulletImg => Images[0];
        internal static Image EnemyBulletImg => Images[1];
        internal int ImageIndex { get; set; }        
        public Bullet(Point pos, Point dir, int power) : base(pos, dir, power)
        {
            ImageIndex = 0;
            Rect = new Rectangle(Pos.X, Pos.Y, Images[ImageIndex].Size.Width, Images[ImageIndex].Size.Height);
        }
        public Bullet(Point pos, Point dir, int power, int imageIndex) : base(pos, dir, power)
        {
            if (imageIndex >= Images.Length) throw new ArgumentOutOfRangeException("Bullet Image index");
            ImageIndex = imageIndex;
            Rect = new Rectangle(Pos.X, Pos.Y, Images[ImageIndex].Size.Width, Images[ImageIndex].Size.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Images[ImageIndex], Pos);
            Rect = new Rectangle(Pos.X, Pos.Y, Images[ImageIndex].Size.Width, Images[ImageIndex].Size.Height);
        }

        public override void Update() => Pos.X += Dir.X;
    }
}
