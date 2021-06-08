using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        private static Image[] Images { get; set; } = new Image[2]
        {
            Properties.Resources.ray,
            Properties.Resources.redRay
        };
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

        public override void Update()
        {
            if (Pos.X > Settings.FieldWidth)
                Pos.X = 0;
            Pos.X += Dir.X;
            
        }
    }
}
